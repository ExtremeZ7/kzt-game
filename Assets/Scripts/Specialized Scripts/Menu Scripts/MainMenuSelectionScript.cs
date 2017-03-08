using UnityEngine;
using AssemblyCSharp;

public class MainMenuSelectionScript : MonoBehaviour
{

	private enum SelectionState
	{
		StartingTheGame,
		SelectingGameMode,
		SelectingControls,
		SelectingLoadGame,
		MovingToAnotherScene}

	;

	private enum GameSelection
	{
		NewGame,
		LoadGame}

	;

	private enum ControlSelection
	{
		KBRight,
		KBLeft,
		Gamepad}

	;

	private int loadGameSelection;

	private SelectionState selectionState = SelectionState.SelectingGameMode;
	private GameSelection gameSelection;
	private ControlSelection controlSelection;

	private KeyCode upKey;
	private KeyCode downKey;
	private KeyCode selectKey;
	private KeyCode cancelKey;

	[Space (10)]
	public GUIStyle textStyle;

	private GUIStyle newTextStyle;
	private GUIStyle loadTextStyle;
	private GUIStyle kbRightTextStyle;
	private GUIStyle kbLeftTextStyle;
	private GUIStyle gamepadTextStyle;
	private GUIStyle titleTextStyle;

	void Start ()
	{
		GameControl.control.barrierIsOpen = true;

		upKey = GameControl.control.settings.upKey;
		downKey = GameControl.control.settings.downKey;
		selectKey = GameControl.control.settings.selectKey;
		cancelKey = GameControl.control.settings.cancelKey;

		newTextStyle = new GUIStyle (textStyle);
		loadTextStyle = new GUIStyle (textStyle);
		kbRightTextStyle = new GUIStyle (textStyle);
		kbLeftTextStyle = new GUIStyle (textStyle);
		gamepadTextStyle = new GUIStyle (textStyle);

		kbRightTextStyle.alignment = TextAnchor.UpperLeft;
		kbLeftTextStyle.alignment = TextAnchor.UpperLeft;
		gamepadTextStyle.alignment = TextAnchor.UpperLeft;

		titleTextStyle = new GUIStyle (textStyle);

		titleTextStyle.fontSize = 60;
		titleTextStyle.normal.textColor = Color.yellow;
	}

	void Update ()
	{
		switch (selectionState) {
		case SelectionState.SelectingGameMode:

			switch (gameSelection) {

			case GameSelection.NewGame:
				if (Input.GetKeyDown (downKey))
					gameSelection = GameSelection.LoadGame;
				if (Input.GetKeyDown (selectKey)) {
					selectionState = SelectionState.SelectingControls;
					controlSelection = ControlSelection.KBRight;
				}
				break;

			case GameSelection.LoadGame:
				if (Input.GetKeyDown (upKey))
					gameSelection = GameSelection.NewGame;
				if (Input.GetKeyDown (selectKey)) {
					selectionState = SelectionState.SelectingLoadGame;
					loadGameSelection = 0;
				}
				break;
			}
			break;

		case SelectionState.SelectingControls:

			switch (controlSelection) {
			case ControlSelection.KBRight:
				GameControl.control.settings.leftKey = KeyCode.A;
				GameControl.control.settings.rightKey = KeyCode.D;
				GameControl.control.settings.upKey = KeyCode.W;
				GameControl.control.settings.downKey = KeyCode.S;

				GameControl.control.settings.usingGamepad = false;

				if (Input.GetKeyDown (downKey))
					controlSelection = ControlSelection.KBLeft;
				break;
			case ControlSelection.KBLeft:
				GameControl.control.settings.leftKey = KeyCode.LeftArrow;
				GameControl.control.settings.rightKey = KeyCode.RightArrow;
				GameControl.control.settings.upKey = KeyCode.UpArrow;
				GameControl.control.settings.downKey = KeyCode.DownArrow;

				GameControl.control.settings.usingGamepad = false;

				if (Input.GetKeyDown (upKey))
					controlSelection = ControlSelection.KBRight;
				if (Input.GetKeyDown (downKey))
					controlSelection = ControlSelection.Gamepad;
				break;
			case ControlSelection.Gamepad:
				GameControl.control.settings.usingGamepad = true;

				if (Input.GetKeyDown (upKey))
					controlSelection = ControlSelection.KBLeft;
				break;
			}

			if (Input.GetKeyDown (selectKey)) {
				GameControl.control.SaveSettings ();
				GameControl.control.progress.InitNewGameProgress ();
				GameControl.control.MoveToOtherScene ("World Map");
				selectionState = SelectionState.MovingToAnotherScene;
			}

			if (Input.GetKeyDown (cancelKey)) {
				selectionState = SelectionState.SelectingGameMode;
				gameSelection = GameSelection.NewGame;
			}

			break;
		
		case SelectionState.SelectingLoadGame:
			if (Input.GetKeyDown (cancelKey)) {
				selectionState = SelectionState.SelectingGameMode;
				gameSelection = GameSelection.LoadGame;
			}

			if (Input.GetKeyDown (selectKey)) {
				if (GameControl.control.LoadProgress (loadGameSelection)) {
					GameControl.control.MoveToOtherScene ("World Map");
					selectionState = SelectionState.MovingToAnotherScene;
				} else {
					//Create The Reject Sound Effect Here Too (Maybe Use a Prefab This Time)
				}
			}

			if (Input.GetKeyDown (upKey) && loadGameSelection > 0)
				loadGameSelection--;

			if (Input.GetKeyDown (downKey) && loadGameSelection < 3)
				loadGameSelection++;
			break;
		}
	}

	void OnGUI ()
	{
		float standardScreenWidth = GameControl.control.standardScreenWidth;
		float standardScreenHeight = GameControl.control.standardScreenHeight;

		float xScale = Screen.width / standardScreenWidth;
		float yScale = Screen.height / standardScreenHeight;
		GUI.matrix = Matrix4x4.TRS (new Vector3 (0, 0, 0), Quaternion.identity, new Vector3 (xScale, yScale, 1));

		int scaleSpeed = 2;

		switch (selectionState) {
		case SelectionState.SelectingGameMode:

			Rect newRect = new Rect (250, 350, 300, 100);
			Rect loadRect = new Rect (250, 475, 300, 100);

			newTextStyle.normal.textColor = textStyle.normal.textColor;
			loadTextStyle.normal.textColor = textStyle.normal.textColor;

			switch (gameSelection) {

			case GameSelection.NewGame:
				newTextStyle.fontSize = Helper.IntMoveTowards (newTextStyle.fontSize, textStyle.fontSize + 20, scaleSpeed);
				loadTextStyle.fontSize = Helper.IntMoveTowards (loadTextStyle.fontSize, textStyle.fontSize, scaleSpeed);

				newTextStyle.normal.textColor = new Color (Random.value, Random.value, Random.value);
				break;

			case GameSelection.LoadGame:
				newTextStyle.fontSize = Helper.IntMoveTowards (newTextStyle.fontSize, textStyle.fontSize, scaleSpeed);
				loadTextStyle.fontSize = Helper.IntMoveTowards (loadTextStyle.fontSize, textStyle.fontSize + 20, scaleSpeed);

				loadTextStyle.normal.textColor = new Color (Random.value, Random.value, Random.value);
				break;

			}
				

			GUI.Label (newRect, "New Game", newTextStyle);
			GUI.Label (loadRect, "Load Game", loadTextStyle);

			break;
		
		case SelectionState.SelectingControls:

			Rect menuTitleRect = new Rect (200, 50, 400, 200);
			Rect kbRightRect = new Rect (20, 250, 300, 100);
			Rect kbLeftRect = new Rect (20, 375, 300, 100);
			Rect gamepadRect = new Rect (20, 500, 300, 100);

			kbRightTextStyle.normal.textColor = textStyle.normal.textColor;
			kbLeftTextStyle.normal.textColor = textStyle.normal.textColor;
			gamepadTextStyle.normal.textColor = textStyle.normal.textColor;

			switch (controlSelection) {
			case ControlSelection.KBRight:
				kbRightTextStyle.fontSize = Helper.IntMoveTowards (kbRightTextStyle.fontSize, textStyle.fontSize + 10, scaleSpeed);
				kbLeftTextStyle.fontSize = Helper.IntMoveTowards (kbLeftTextStyle.fontSize, textStyle.fontSize, scaleSpeed);
				gamepadTextStyle.fontSize = Helper.IntMoveTowards (gamepadTextStyle.fontSize, textStyle.fontSize, scaleSpeed);

				kbRightTextStyle.normal.textColor = new Color (Random.value, Random.value, Random.value);

				break;
			case ControlSelection.KBLeft:
				kbRightTextStyle.fontSize = Helper.IntMoveTowards (kbRightTextStyle.fontSize, textStyle.fontSize, scaleSpeed);
				kbLeftTextStyle.fontSize = Helper.IntMoveTowards (kbLeftTextStyle.fontSize, textStyle.fontSize + 10, scaleSpeed);
				gamepadTextStyle.fontSize = Helper.IntMoveTowards (gamepadTextStyle.fontSize, textStyle.fontSize, scaleSpeed);

				kbLeftTextStyle.normal.textColor = new Color (Random.value, Random.value, Random.value);

				break;
			case ControlSelection.Gamepad:
				kbRightTextStyle.fontSize = Helper.IntMoveTowards (kbRightTextStyle.fontSize, textStyle.fontSize, scaleSpeed);
				kbLeftTextStyle.fontSize = Helper.IntMoveTowards (kbLeftTextStyle.fontSize, textStyle.fontSize, scaleSpeed);
				gamepadTextStyle.fontSize = Helper.IntMoveTowards (gamepadTextStyle.fontSize, textStyle.fontSize + 10, scaleSpeed);

				gamepadTextStyle.normal.textColor = new Color (Random.value, Random.value, Random.value);
				break;
			}

			GUI.Label (menuTitleRect, "Select Control Scheme", titleTextStyle);
			GUI.Label (kbRightRect, "Keyboard\n(Right-Handed)", kbRightTextStyle);
			GUI.Label (kbLeftRect, "Keyboard\n(Left-Handed)", kbLeftTextStyle);
			GUI.Label (gamepadRect, "Gamepad", gamepadTextStyle);
			break;

		case SelectionState.SelectingLoadGame:

			GUIStyle nameStyle = new GUIStyle (textStyle);
			nameStyle.alignment = TextAnchor.MiddleRight;

			GUIStyle progressStyle = new GUIStyle (textStyle);
			progressStyle.alignment = TextAnchor.MiddleLeft;

			GUIStyle completionStyle = new GUIStyle (textStyle);
			completionStyle.alignment = TextAnchor.MiddleCenter;
			completionStyle.normal.textColor = new Color (1.0f, 0.2f, 0.2f);

			GUI.Label (new Rect (200, 10, 400, 100), "Load Game", titleTextStyle);

			GameControl.Progress[] saveFiles = GameControl.control.saveFiles;

			for (int i = 0; i < 4; i++) {

				bool saveFileExists = saveFiles [i] != null;

				int currentWorld = saveFileExists ? saveFiles [i].currentWorld : 0;
				int currentLevel = saveFileExists ? saveFiles [i].currentLevel : 0;


				//Draw Select Bar
				GUI.DrawTexture (new Rect (20, 120 + (130 * i), 760, 120),	Res.LoadTexture (loadGameSelection == i) as Texture);

				//Draw Save Icon
				GUI.DrawTexture (new Rect (30, 130 + (130 * i), 100, 100), Res.SaveIcon (currentWorld, currentLevel) as Texture);

				//Draw Collectible Item Progress
				for (int j = 0; j < 5; j++) {
					for (int k = 0; k < 3; k++) {
						GUI.DrawTexture (new Rect (140 + (38 * j), 125 + (36 * k) + (130 * i), 18, 18), 
							Res.RedGemIcon (saveFileExists && saveFiles [i].gemsCollected [j, k]) as Texture);
						GUI.DrawTexture (new Rect (158 + (38 * j), 125 + (36 * k) + (130 * i), 18, 18), 
							Res.KIcon (saveFileExists && saveFiles [i].lettersCollected [j, k, 0]) as Texture);
						GUI.DrawTexture (new Rect (140 + (38 * j), 143 + (36 * k) + (130 * i), 18, 18), 
							Res.KIcon (saveFileExists && saveFiles [i].lettersCollected [j, k, 1]) as Texture);
						GUI.DrawTexture (new Rect (158 + (38 * j), 143 + (36 * k) + (130 * i), 18, 18), 
							Res.KIcon (saveFileExists && saveFiles [i].lettersCollected [j, k, 2]) as Texture);
					}
				}

				/*GUI.DrawTexture(new Rect(140, 130 + (130 * i), 48,48), Resources.Load("Crystal Icons/krazy_krystal", typeof(Texture)) as Texture);
				GUI.Label(new Rect(188, 130 + (130 * i),48,48),"" + (saveFileExists ? saveFiles[i].GetCrystalPercentage() : 0) +"%",progressStyle);
				GUI.Label(new Rect(188, 183 + (130 * i),48,48),"" + (saveFileExists ? saveFiles[i].GetRedGemCount() : 0)+ "/15",progressStyle);*/

				//Draw Save File Identity
				GUI.Label (new Rect (640, 120 + (130 * i), 120, 120), (i == 0 ? "Autosave" : "Save File " + i), nameStyle);

				//Draw Completion Percentage
				GUI.Label (new Rect (450, 120 + (130 * i), 120, 120), "0%\nComplete", completionStyle);
			}
			break;
		}
	}
}
