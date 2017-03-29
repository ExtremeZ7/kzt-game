using UnityEngine;
using Helper;
using Controllers;

public class MainMenuSelectionScript : MonoBehaviour
{

    enum SelectionState
    {
        StartingTheGame,
        SelectingGameMode,
        SelectingControls,
        SelectingLoadGame,
        MovingToAnotherScene}

    ;

    enum GameSelection
    {
        NewGame,
        LoadGame}

    ;

    enum ControlSelection
    {
        KBRight,
        KBLeft,
        Gamepad}

    ;

    int loadGameSelection;

    SelectionState selectionState = SelectionState.SelectingGameMode;
    GameSelection gameSelection;
    ControlSelection controlSelection;

    KeyCode upKey;
    KeyCode downKey;
    KeyCode selectKey;
    KeyCode cancelKey;

    [Space(10)]
    public GUIStyle textStyle;

    GUIStyle newTextStyle;
    GUIStyle loadTextStyle;
    GUIStyle kbRightTextStyle;
    GUIStyle kbLeftTextStyle;
    GUIStyle gamepadTextStyle;
    GUIStyle titleTextStyle;

    void Start()
    {
        GameController.Instance.barrierIsOpen = true;

        upKey = GameController.Instance.settings.upKey;
        downKey = GameController.Instance.settings.downKey;
        selectKey = GameController.Instance.settings.selectKey;
        cancelKey = GameController.Instance.settings.cancelKey;

        newTextStyle = new GUIStyle(textStyle);
        loadTextStyle = new GUIStyle(textStyle);
        kbRightTextStyle = new GUIStyle(textStyle);
        kbLeftTextStyle = new GUIStyle(textStyle);
        gamepadTextStyle = new GUIStyle(textStyle);

        kbRightTextStyle.alignment = TextAnchor.UpperLeft;
        kbLeftTextStyle.alignment = TextAnchor.UpperLeft;
        gamepadTextStyle.alignment = TextAnchor.UpperLeft;

        titleTextStyle = new GUIStyle(textStyle);

        titleTextStyle.fontSize = 60;
        titleTextStyle.normal.textColor = Color.yellow;
    }

    void Update()
    {
        switch (selectionState)
        {
            case SelectionState.SelectingGameMode:

                switch (gameSelection)
                {

                    case GameSelection.NewGame:
                        if (Input.GetKeyDown(downKey))
                            gameSelection = GameSelection.LoadGame;
                        if (Input.GetKeyDown(selectKey))
                        {
                            selectionState = SelectionState.SelectingControls;
                            controlSelection = ControlSelection.KBRight;
                        }
                        break;

                    case GameSelection.LoadGame:
                        if (Input.GetKeyDown(upKey))
                            gameSelection = GameSelection.NewGame;
                        if (Input.GetKeyDown(selectKey))
                        {
                            selectionState = SelectionState.SelectingLoadGame;
                            loadGameSelection = 0;
                        }
                        break;
                }
                break;

            case SelectionState.SelectingControls:

                switch (controlSelection)
                {
                    case ControlSelection.KBRight:
                        GameController.Instance.settings.leftKey = KeyCode.A;
                        GameController.Instance.settings.rightKey = KeyCode.D;
                        GameController.Instance.settings.upKey = KeyCode.W;
                        GameController.Instance.settings.downKey = KeyCode.S;

                        GameController.Instance.settings.usingGamepad = false;

                        if (Input.GetKeyDown(downKey))
                            controlSelection = ControlSelection.KBLeft;
                        break;
                    case ControlSelection.KBLeft:
                        GameController.Instance.settings.leftKey = KeyCode.LeftArrow;
                        GameController.Instance.settings.rightKey = KeyCode.RightArrow;
                        GameController.Instance.settings.upKey = KeyCode.UpArrow;
                        GameController.Instance.settings.downKey = KeyCode.DownArrow;

                        GameController.Instance.settings.usingGamepad = false;

                        if (Input.GetKeyDown(upKey))
                            controlSelection = ControlSelection.KBRight;
                        if (Input.GetKeyDown(downKey))
                            controlSelection = ControlSelection.Gamepad;
                        break;
                    case ControlSelection.Gamepad:
                        GameController.Instance.settings.usingGamepad = true;

                        if (Input.GetKeyDown(upKey))
                            controlSelection = ControlSelection.KBLeft;
                        break;
                }

                if (Input.GetKeyDown(selectKey))
                {
                    GameController.Instance.SaveSettings();
                    GameController.Instance.progress.InitNewGameProgress();
                    GameController.Instance.MoveToOtherScene("World Map");
                    selectionState = SelectionState.MovingToAnotherScene;
                }

                if (Input.GetKeyDown(cancelKey))
                {
                    selectionState = SelectionState.SelectingGameMode;
                    gameSelection = GameSelection.NewGame;
                }

                break;
		
            case SelectionState.SelectingLoadGame:
                if (Input.GetKeyDown(cancelKey))
                {
                    selectionState = SelectionState.SelectingGameMode;
                    gameSelection = GameSelection.LoadGame;
                }

                if (Input.GetKeyDown(selectKey))
                {
                    if (GameController.Instance.LoadProgress(loadGameSelection))
                    {
                        GameController.Instance.MoveToOtherScene("World Map");
                        selectionState = SelectionState.MovingToAnotherScene;
                    }
                    else
                    {
                        //Create The Reject Sound Effect Here Too (Maybe Use a Prefab This Time)
                    }
                }

                if (Input.GetKeyDown(upKey) && loadGameSelection > 0)
                    loadGameSelection--;

                if (Input.GetKeyDown(downKey) && loadGameSelection < 3)
                    loadGameSelection++;
                break;
        }
    }

    void OnGUI()
    {
        float standardScreenWidth = GameController.Instance.standardScreenWidth;
        float standardScreenHeight = GameController.Instance.standardScreenHeight;

        float xScale = Screen.width / standardScreenWidth;
        float yScale = Screen.height / standardScreenHeight;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(xScale, yScale, 1));

        int scaleSpeed = 2;

        switch (selectionState)
        {
            case SelectionState.SelectingGameMode:

                var newRect = new Rect(250, 350, 300, 100);
                var loadRect = new Rect(250, 475, 300, 100);

                newTextStyle.normal.textColor = textStyle.normal.textColor;
                loadTextStyle.normal.textColor = textStyle.normal.textColor;

                switch (gameSelection)
                {

                    case GameSelection.NewGame:
                        newTextStyle.fontSize = Help.IntMoveTowards(newTextStyle.fontSize, textStyle.fontSize + 20, scaleSpeed);
                        loadTextStyle.fontSize = Help.IntMoveTowards(loadTextStyle.fontSize, textStyle.fontSize, scaleSpeed);

                        newTextStyle.normal.textColor = new Color(Random.value, Random.value, Random.value);
                        break;

                    case GameSelection.LoadGame:
                        newTextStyle.fontSize = Help.IntMoveTowards(newTextStyle.fontSize, textStyle.fontSize, scaleSpeed);
                        loadTextStyle.fontSize = Help.IntMoveTowards(loadTextStyle.fontSize, textStyle.fontSize + 20, scaleSpeed);

                        loadTextStyle.normal.textColor = new Color(Random.value, Random.value, Random.value);
                        break;

                }
				

                GUI.Label(newRect, "New Game", newTextStyle);
                GUI.Label(loadRect, "Load Game", loadTextStyle);

                break;
		
            case SelectionState.SelectingControls:

                var menuTitleRect = new Rect(200, 50, 400, 200);
                var kbRightRect = new Rect(20, 250, 300, 100);
                var kbLeftRect = new Rect(20, 375, 300, 100);
                var gamepadRect = new Rect(20, 500, 300, 100);

                kbRightTextStyle.normal.textColor = textStyle.normal.textColor;
                kbLeftTextStyle.normal.textColor = textStyle.normal.textColor;
                gamepadTextStyle.normal.textColor = textStyle.normal.textColor;

                switch (controlSelection)
                {
                    case ControlSelection.KBRight:
                        kbRightTextStyle.fontSize = Help.IntMoveTowards(kbRightTextStyle.fontSize, textStyle.fontSize + 10, scaleSpeed);
                        kbLeftTextStyle.fontSize = Help.IntMoveTowards(kbLeftTextStyle.fontSize, textStyle.fontSize, scaleSpeed);
                        gamepadTextStyle.fontSize = Help.IntMoveTowards(gamepadTextStyle.fontSize, textStyle.fontSize, scaleSpeed);

                        kbRightTextStyle.normal.textColor = new Color(Random.value, Random.value, Random.value);

                        break;
                    case ControlSelection.KBLeft:
                        kbRightTextStyle.fontSize = Help.IntMoveTowards(kbRightTextStyle.fontSize, textStyle.fontSize, scaleSpeed);
                        kbLeftTextStyle.fontSize = Help.IntMoveTowards(kbLeftTextStyle.fontSize, textStyle.fontSize + 10, scaleSpeed);
                        gamepadTextStyle.fontSize = Help.IntMoveTowards(gamepadTextStyle.fontSize, textStyle.fontSize, scaleSpeed);

                        kbLeftTextStyle.normal.textColor = new Color(Random.value, Random.value, Random.value);

                        break;
                    case ControlSelection.Gamepad:
                        kbRightTextStyle.fontSize = Help.IntMoveTowards(kbRightTextStyle.fontSize, textStyle.fontSize, scaleSpeed);
                        kbLeftTextStyle.fontSize = Help.IntMoveTowards(kbLeftTextStyle.fontSize, textStyle.fontSize, scaleSpeed);
                        gamepadTextStyle.fontSize = Help.IntMoveTowards(gamepadTextStyle.fontSize, textStyle.fontSize + 10, scaleSpeed);

                        gamepadTextStyle.normal.textColor = new Color(Random.value, Random.value, Random.value);
                        break;
                }

                GUI.Label(menuTitleRect, "Select Control Scheme", titleTextStyle);
                GUI.Label(kbRightRect, "Keyboard\n(Right-Handed)", kbRightTextStyle);
                GUI.Label(kbLeftRect, "Keyboard\n(Left-Handed)", kbLeftTextStyle);
                GUI.Label(gamepadRect, "Gamepad", gamepadTextStyle);
                break;

            case SelectionState.SelectingLoadGame:

                GUIStyle nameStyle = new GUIStyle(textStyle);
                nameStyle.alignment = TextAnchor.MiddleRight;

                GUIStyle progressStyle = new GUIStyle(textStyle);
                progressStyle.alignment = TextAnchor.MiddleLeft;

                GUIStyle completionStyle = new GUIStyle(textStyle);
                completionStyle.alignment = TextAnchor.MiddleCenter;
                completionStyle.normal.textColor = new Color(1.0f, 0.2f, 0.2f);

                GUI.Label(new Rect(200, 10, 400, 100), "Load Game", titleTextStyle);

                GameController.Progress[] saveFiles = GameController.Instance.saveFiles;

                for (int i = 0; i < 4; i++)
                {

                    bool saveFileExists = saveFiles[i] != null;

                    int currentWorld = saveFileExists ? saveFiles[i].currentWorld : 0;
                    int currentLevel = saveFileExists ? saveFiles[i].currentLevel : 0;


                    //Draw Select Bar
                    GUI.DrawTexture(new Rect(20, 120 + (130 * i), 760, 120),	Res.GetSaveFileTexture(loadGameSelection == i) as Texture);

                    //Draw Save Icon
                    if (saveFileExists)
                    {
                        GUI.DrawTexture(new Rect(30, 130 + (130 * i), 100, 100), Res.GetSaveThumbnail(currentWorld - 1, currentLevel - 1) as Texture);
                    }
                    else
                    {
                        GUI.DrawTexture(new Rect(30, 130 + (130 * i), 100, 100), Res.GetNoSaveThumbnail() as Texture);
                    }
                    //Draw Collectible Item Progress
                    for (int j = 0; j < 5; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            GUI.DrawTexture(new Rect(140 + (38 * j), 125 + (36 * k) + (130 * i), 18, 18), 
                                Res.GetRedGemSprite(saveFileExists && saveFiles[i].gemsCollected[j, k]) as Texture);
                            GUI.DrawTexture(new Rect(158 + (38 * j), 125 + (36 * k) + (130 * i), 18, 18), 
                                Res.GetLetterKSprite(saveFileExists && saveFiles[i].lettersCollected[j, k, 0]) as Texture);
                            GUI.DrawTexture(new Rect(140 + (38 * j), 143 + (36 * k) + (130 * i), 18, 18), 
                                Res.GetLetterKSprite(saveFileExists && saveFiles[i].lettersCollected[j, k, 1]) as Texture);
                            GUI.DrawTexture(new Rect(158 + (38 * j), 143 + (36 * k) + (130 * i), 18, 18), 
                                Res.GetLetterKSprite(saveFileExists && saveFiles[i].lettersCollected[j, k, 2]) as Texture);
                        }
                    }

                    /*GUI.DrawTexture(new Rect(140, 130 + (130 * i), 48,48), Resources.Load("Crystal Icons/krazy_krystal", typeof(Texture)) as Texture);
				GUI.Label(new Rect(188, 130 + (130 * i),48,48),"" + (saveFileExists ? saveFiles[i].GetCrystalPercentage() : 0) +"%",progressStyle);
				GUI.Label(new Rect(188, 183 + (130 * i),48,48),"" + (saveFileExists ? saveFiles[i].GetRedGemCount() : 0)+ "/15",progressStyle);*/

                    //Draw Save File Identity
                    GUI.Label(new Rect(640, 120 + (130 * i), 120, 120), (i == 0 ? "Autosave" : "Save File " + i), nameStyle);

                    //Draw Completion Percentage
                    GUI.Label(new Rect(450, 120 + (130 * i), 120, 120), "0%\nComplete", completionStyle);
                }
                break;
        }
    }
}
