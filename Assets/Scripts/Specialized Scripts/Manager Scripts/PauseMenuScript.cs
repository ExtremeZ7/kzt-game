using UnityEngine;
using UnityEngine.SceneManagement;
using Common.Extensions;
using Controllers;

public class PauseMenuScript : MonoBehaviour
{

    public enum CurrentMenu
    {
        MainPauseMenu,
        VolumeSettings,
        SaveGame

    }

    public enum MainSelection
    {
        ChangeVolume,
        SaveGame,
        ReturnToWorldMap,
        ReturnToMainMenu,
        ExitGame,
        ReturningToWorldMap,
        ReturningToMainMenu,
        ExitingGame

    }

    public enum VolumeSelection
    {
        MusicVolume,
        EffectsVolume,
        ApplyChanges

    }

    private int saveFileSelection = 1;

    private CurrentMenu currentMenu;
    private MainSelection mainSelection;
    private VolumeSelection volumeSelection;

    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode leftKey;
    private KeyCode rightKey;
    private KeyCode selectKey;
    private KeyCode cancelKey;

    public string[] disablePauseInTheseScenes;

    [Space(10)]
    public GUIStyle textStyle;

    private GUIStyle[] selectionStyles = new GUIStyle[5];
    private GUIStyle titleTextStyle;

    private int originalMusicVolume;
    private int originalEffectsVolume;

    void Start()
    {
        upKey = GameController.Instance.settings.upKey;
        downKey = GameController.Instance.settings.downKey;
        leftKey = GameController.Instance.settings.leftKey;
        rightKey = GameController.Instance.settings.rightKey;
        selectKey = GameController.Instance.settings.selectKey;
        cancelKey = GameController.Instance.settings.cancelKey;

        originalMusicVolume = GameController.Instance.settings.musicVolume;
        originalEffectsVolume = GameController.Instance.settings.effectsVolume;
    }

    void Update()
    {
        if (Input.GetKeyDown(GameController.Instance.settings.pauseKey) && GameController.Instance.barrierIsOpen
            && !disablePauseInTheseScenes.Contains(SceneManager.GetActiveScene().name) && GameController.Instance.allowPause)
        {
            TogglePause();
        }

        if (GameController.Instance.paused)
        {
            switch (currentMenu)
            {

                case CurrentMenu.MainPauseMenu:
                    if (Input.GetKeyDown(upKey) && (int)mainSelection > 0)
                        mainSelection = (MainSelection)((int)mainSelection) - 1;
                    if (Input.GetKeyDown(downKey) && (int)mainSelection < 4)
                        mainSelection = (MainSelection)((int)mainSelection) + 1;

                    switch (mainSelection)
                    {
                        case MainSelection.ChangeVolume:
                            if (Input.GetKeyDown(selectKey))
                                currentMenu = CurrentMenu.VolumeSettings;
                            volumeSelection = VolumeSelection.MusicVolume;
                            break;
				
                        case MainSelection.SaveGame:
                            if (Input.GetKeyDown(selectKey))
                            {
                                saveFileSelection = 1;
                                currentMenu = CurrentMenu.SaveGame;
                            }
                            break;
				
                        case MainSelection.ReturnToWorldMap:
                            if (Input.GetKeyDown(selectKey))
                            {
                                if (SceneManager.GetActiveScene().name != "World Map")
                                {
                                    TogglePause();
                                    GameController.Instance.MoveToOtherScene("World Map");
                                }
                                else
                                {
                                    //Create The Rejection Sound Effect Here
                                }
                            }
                            break;
				
                        case MainSelection.ReturnToMainMenu:
                            if (Input.GetKeyDown(selectKey))
                            {
                                TogglePause();
                                GameController.Instance.MoveToOtherScene("Main Menu");
                            }
                            break;
				
                        case MainSelection.ExitGame:
                            if (Input.GetKeyDown(selectKey))
                            {	
                                TogglePause();
                                GameController.Instance.QuitGame();
                            }
                            break;
                    }

                    if (Input.GetKeyDown(cancelKey))
                    {
                        TogglePause();
                    }
                    break;
			
                case CurrentMenu.VolumeSettings:

                    if (Input.GetKeyDown(upKey) && (int)volumeSelection > 0)
                        volumeSelection = (VolumeSelection)((int)volumeSelection) - 1;
                    if (Input.GetKeyDown(downKey) && (int)volumeSelection < 2)
                        volumeSelection = (VolumeSelection)((int)volumeSelection) + 1;

                    switch (volumeSelection)
                    {
                        case VolumeSelection.MusicVolume:
                            if (Input.GetKey(leftKey))
                                GameController.Instance.settings.musicVolume = Help.IntMoveTowards(GameController.Instance.settings.musicVolume, 0, 1);
                            if (Input.GetKey(rightKey))
                                GameController.Instance.settings.musicVolume = Help.IntMoveTowards(GameController.Instance.settings.musicVolume, 100, 1);

                            GameController.Instance.UpdateMusicVolume();

                            break;

                        case VolumeSelection.EffectsVolume:
                            if (Input.GetKey(leftKey))
                                GameController.Instance.settings.effectsVolume = Help.IntMoveTowards(GameController.Instance.settings.effectsVolume, 0, 1);
                            if (Input.GetKey(rightKey))
                                GameController.Instance.settings.effectsVolume = Help.IntMoveTowards(GameController.Instance.settings.effectsVolume, 100, 1);

                            GameController.Instance.UpdateEffectsVolume();
                            break;

                        case VolumeSelection.ApplyChanges:
                            if (Input.GetKeyDown(selectKey))
                            {
                                originalMusicVolume = GameController.Instance.settings.musicVolume;
                                originalEffectsVolume = GameController.Instance.settings.effectsVolume;

                                GameController.Instance.SaveSettings();

                                currentMenu = CurrentMenu.MainPauseMenu;
                                mainSelection = MainSelection.ChangeVolume;
                            }
                            break;
                    }

                    if (Input.GetKeyDown(cancelKey))
                    {
                        GameController.Instance.settings.musicVolume = originalMusicVolume;
                        GameController.Instance.settings.effectsVolume = originalEffectsVolume;

                        GameController.Instance.UpdateEffectsVolume();
                        GameController.Instance.UpdateMusicVolume();

                        currentMenu = CurrentMenu.MainPauseMenu;
                        mainSelection = MainSelection.ChangeVolume;
                    }
                    break;
			
                case CurrentMenu.SaveGame:
                    if (Input.GetKeyDown(upKey) && saveFileSelection > 1)
                        saveFileSelection--;
                    if (Input.GetKeyDown(downKey) && saveFileSelection < 3)
                        saveFileSelection++;

                    if (Debug.isDebugBuild)
                    {	
                        if (Input.GetKeyDown(KeyCode.Delete))
                        {
                            GameController.Instance.EraseProgress(saveFileSelection);
                        }
                    }

                    if (Input.GetKeyDown(selectKey))
                    {
                        GameController.Instance.SaveProgress(saveFileSelection);
                        TogglePause();
                        //Help.GenerateHintBox("Game Saved");
                    }
                    if (Input.GetKeyDown(cancelKey))
                    {
                        currentMenu = CurrentMenu.MainPauseMenu;
                        mainSelection = MainSelection.SaveGame;
                    }
                    break;
            }
        }
    }

    void OnGUI()
    {
        if (GameController.Instance.paused)
        {
            float standardScreenWidth = GameController.Instance.standardScreenWidth;
            float standardScreenHeight = GameController.Instance.standardScreenHeight;

            float xScale = Screen.width / standardScreenWidth;
            float yScale = Screen.height / standardScreenHeight;
            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(xScale, yScale, 1));

            int scaleSpeed = 2;
            string selectionName = "";

            GUI.DrawTexture(new Rect(0, 0, 800, 640), Resources.Load("Other Textures/pause_bg_texture", typeof(Texture)) as Texture);


            switch (currentMenu)
            {
                case CurrentMenu.MainPauseMenu:
                    GUI.Label(new Rect(300, 10, 200, 100), "Game Paused", titleTextStyle);

                    for (int i = 0; i < 5; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                selectionName = "Set Volume";
                                break;
                            case 1:
                                selectionName = "Save Game";
                                break;
                            case 2:
                                selectionName = "Return To World Map";
                                break;
                            case 3:
                                selectionName = "Return To Main Menu";
                                break;
                            case 4:
                                selectionName = "Exit Game";
                                break;
                        }

                        if (i == (int)mainSelection)
                        {
                            selectionStyles[i].fontSize = Help.IntMoveTowards(selectionStyles[i].fontSize, textStyle.fontSize + 10, scaleSpeed);
                            selectionStyles[i].normal.textColor = new Color(Random.value, Random.value, Random.value);
                        }
                        else
                        {
                            selectionStyles[i].fontSize = Help.IntMoveTowards(selectionStyles[i].fontSize, textStyle.fontSize, scaleSpeed);
                            selectionStyles[i].normal.textColor = textStyle.normal.textColor;
                        }
						
                        GUI.Label(new Rect(300, 120 + (90 * i), 200, 100), selectionName, selectionStyles[i]);
                    }
                    break;

                case CurrentMenu.VolumeSettings:
                    GUI.Label(new Rect(300, 10, 200, 100), "Volume Settings", titleTextStyle);

                    for (int i = 0; i < 3; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                selectionName = "Music Volume\n" + GameController.Instance.settings.musicVolume + "%";
                                break;
                            case 1:
                                selectionName = "Effects Volume\n" + GameController.Instance.settings.effectsVolume + "%";
                                break;
                            case 2:
                                selectionName = "Apply Changes";
                                break;
                        }

                        if (i == (int)volumeSelection)
                        {
                            selectionStyles[i].fontSize = Help.IntMoveTowards(selectionStyles[i].fontSize, textStyle.fontSize + 10, scaleSpeed);
                            selectionStyles[i].normal.textColor = new Color(Random.value, Random.value, Random.value);
                        }
                        else
                        {
                            selectionStyles[i].fontSize = Help.IntMoveTowards(selectionStyles[i].fontSize, textStyle.fontSize, scaleSpeed);
                            selectionStyles[i].normal.textColor = textStyle.normal.textColor;
                        }

                        GUI.Label(new Rect(300, 140 + (120 * i), 200, 100), selectionName, selectionStyles[i]);
                    }
                    break;

                case CurrentMenu.SaveGame:
                    GUIStyle nameStyle = new GUIStyle(textStyle);
                    nameStyle.alignment = TextAnchor.MiddleRight;

                    GUIStyle progressStyle = new GUIStyle(textStyle);
                    progressStyle.alignment = TextAnchor.MiddleLeft;

                    GUIStyle completionStyle = new GUIStyle(textStyle);
                    completionStyle.alignment = TextAnchor.MiddleCenter;
                    completionStyle.normal.textColor = new Color(1.0f, 0.2f, 0.2f);

                    GUI.Label(new Rect(200, 10, 400, 100), "Save Game", titleTextStyle);

                    GameController.Progress[] saveFiles = GameController.Instance.saveFiles;

                    for (int i = 0; i < 4; i++)
                    {

                        bool saveFileExists = saveFiles[i] != null;

                        Texture saveIcon = Resources.Load("Save File Icons/" + (saveFileExists ? "save_icon_w" +
                                               saveFiles[i].currentWorld + "_l" + saveFiles[i].currentLevel : "no_save_icon"), typeof(Texture)) as Texture;

                        GUI.DrawTexture(new Rect(20, 120 + (130 * i), 760, 120), Resources.Load("Other Textures/" +
                                (saveFileSelection == i ? "selected_load_texture" : "unselected_load_texture"), typeof(Texture)) as Texture);
                        GUI.DrawTexture(new Rect(30, 130 + (130 * i), 100, 100), saveIcon);
                        GUI.DrawTexture(new Rect(140, 130 + (130 * i), 48, 48), Resources.Load("Crystal Icons/krazy_krystal", typeof(Texture)) as Texture);
                        GUI.Label(new Rect(188, 130 + (130 * i), 48, 48), "" + (saveFileExists ? saveFiles[i].GetCrystalPercentage() : 0) + "%", progressStyle);
                        GUI.Label(new Rect(188, 183 + (130 * i), 48, 48), "" + (saveFileExists ? saveFiles[i].GetRedGemCount() : 0) + "/15", progressStyle);
                        GUI.Label(new Rect(640, 120 + (130 * i), 120, 120), (i == 0 ? "Autosave" : "Save File " + i), nameStyle);
                        GUI.Label(new Rect(450, 120 + (130 * i), 120, 120), "0%\nComplete", completionStyle);
                    }

                    GUI.DrawTexture(new Rect(20, 120, 760, 120), Resources.Load("Other Textures/pause_bg_texture", typeof(Texture)) as Texture);
                    break;
            }
        }
    }

    private void TogglePause()
    {
        GameController.Instance.paused = !GameController.Instance.paused;
        Time.timeScale = 1.0f - Time.timeScale;

        Input.ResetInputAxes();

        if (GameController.Instance.paused)
        {
            GameController.Instance.masterMixer.SetFloat("Background Music Lowpass Cut", 22000f / 8f);

            //Help.RemoveAnnoyingMessageBox();

            currentMenu = CurrentMenu.MainPauseMenu;
            mainSelection = MainSelection.ChangeVolume;

            for (int i = 0; i < selectionStyles.Length; i++)
            {
                selectionStyles[i] = new GUIStyle(textStyle);
            }

            titleTextStyle = new GUIStyle(textStyle);

            titleTextStyle.fontSize = 60;
            titleTextStyle.normal.textColor = Color.yellow;
        }
        else
        {
            GameController.Instance.masterMixer.SetFloat("Background Music Lowpass Cut", 22000f);

            GameController.Instance.settings.musicVolume = originalMusicVolume;
            GameController.Instance.settings.effectsVolume = originalEffectsVolume;

            GameController.Instance.UpdateMusicVolume();
            GameController.Instance.UpdateEffectsVolume();
        }
    }
}
