using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using AssemblyCSharp;

public class GameControl : MonoBehaviour
{

    public static GameControl control;

    public Items items;
    public Items itemsFromLastCheckpoint;
    public Progress progress;
    public Progress[] saveFiles = new Progress[4];
    public Settings settings;

    public bool restrictLocked;
    public bool clearAllSaveFiles;
    public bool barrierIsOpen;
    public bool paused;
    public  bool allowPause;

    [Header("Settings")]
    public float standardScreenWidth = 800.0f;
    public float standardScreenHeight = 640.0f;

    private string targetSceneName = "";
    private float transitionTimer;

    private Animator loadingBarrier;
    public AudioMixer masterMixer;

    private bool quitGame;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
            control.items = new Items();
            control.itemsFromLastCheckpoint = new Items();
            control.progress = new Progress();
            control.settings = new Settings();
            control.allowPause = true;
            if (!RetrieveSettings())
                SaveSettings();
            if (clearAllSaveFiles)
            {
                for (int i = 0; i < 4; i++)
                {
                    EraseProgress(i);
                }
            }
            GetSaveFiles();

            masterMixer = Resources.Load("Master Mixer", typeof(AudioMixer)) as AudioMixer;

            if (Debug.isDebugBuild)
                control.progress.InitNewGameProgress();
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateEffectsVolume();
        UpdateMusicVolume();
    }

    void Update()
    {
        control.loadingBarrier = GameObject.FindGameObjectWithTag("Loading Screen Barrier").GetComponent<Animator>();

        if (control.loadingBarrier != null)
            control.loadingBarrier.SetBool("Open", control.barrierIsOpen);
		
        if (control.targetSceneName != "")
        {
            //This is the script used for transfering to another scene
            if (Helper.UseAsTimer(ref control.transitionTimer))
            {
                if (!quitGame)
                {
                    SceneManager.LoadScene(control.targetSceneName);
                    control.barrierIsOpen = true;
                    control.allowPause = true;
                    control.targetSceneName = "";
                }
                else
                {
                    Application.Quit();
                }
            }
        }
    }

    public void UpdateEffectsVolume()
    {
        UpdateEffectsVolume((float)control.settings.effectsVolume);
    }

    public void UpdateMusicVolume()
    {
        UpdateMusicVolume((float)control.settings.musicVolume);
    }

    public void UpdateEffectsVolume(float value)
    {
        masterMixer.SetFloat("Sound Effects Volume", Mathf.Round((float)(Math.Log(1 + (value * Math.Pow(2f, 8f) - 1) / 100f, 2) * 10f) - 80f));
    }

    public void UpdateMusicVolume(float value)
    {
        masterMixer.SetFloat("Background Music Volume", Mathf.Round((float)(Math.Log(1 + (value * Math.Pow(2f, 8f) - 1) / 100f, 2) * 10f) - 80f));
    }

    public void MoveToOtherScene(string targetSceneName, float delay)
    {
        control.barrierIsOpen = false;
        control.transitionTimer = control.loadingBarrier.GetCurrentAnimatorStateInfo(0).length + delay;
        control.targetSceneName = targetSceneName;
        control.allowPause = false;
    }

    public void MoveToOtherScene(string targetSceneName)
    {
        control.barrierIsOpen = false;
        control.transitionTimer = control.loadingBarrier.GetCurrentAnimatorStateInfo(0).length;
        control.targetSceneName = targetSceneName;
        control.allowPause = false;
    }

    public void QuitGame()
    {
        control.barrierIsOpen = false;
        control.transitionTimer = control.loadingBarrier.GetCurrentAnimatorStateInfo(0).length + 1f;
        control.quitGame = true;
        control.allowPause = false;
    }

    public void SaveProgress(int fileNumber)
    {
        BinaryFormatter bf = new BinaryFormatter();

        string saveFilePath;

        if (fileNumber > 0)
            saveFilePath = Application.persistentDataPath + "/saveFile" + fileNumber + ".kzt";
        else
            saveFilePath = Application.persistentDataPath + "/autosave.kzt";

        FileStream file = File.Create(saveFilePath);

        Progress progress = control.progress;

        bf.Serialize(file, progress);
        file.Close();

        FileStream file2 = File.Open(saveFilePath, FileMode.Open);
        control.saveFiles[fileNumber] = (Progress)bf.Deserialize(file2);
        file2.Close();
    }

    public bool LoadProgress(int fileNumber)
    {
        string saveFilePath;

        if (fileNumber > 0)
            saveFilePath = Application.persistentDataPath + "/saveFile" + fileNumber + ".kzt";
        else
            saveFilePath = Application.persistentDataPath + "/autosave.kzt";

        bool fileExists = File.Exists(saveFilePath);

        if (fileExists)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);

            control.progress = (Progress)bf.Deserialize(file);
            file.Close();
        }

        return fileExists;
    }

    public void EraseProgress(int fileNumber)
    {
        string saveFilePath;

        if (fileNumber > 0)
            saveFilePath = Application.persistentDataPath + "/saveFile" + fileNumber + ".kzt";
        else
            saveFilePath = Application.persistentDataPath + "/autosave.kzt";

        bool fileExists = File.Exists(saveFilePath);

        if (fileExists)
        {
            File.Delete(saveFilePath);
            control.saveFiles[fileNumber] = null;
        }
    }

    public bool ProgressFileExists(int fileNumber)
    {
        string saveFilePath;

        if (fileNumber > 0)
            saveFilePath = Application.persistentDataPath + "/saveFile" + fileNumber + ".kzt";
        else
            saveFilePath = Application.persistentDataPath + "/autosave.kzt";

        return File.Exists(saveFilePath);
    }

    public void LoadItemsFromProgress(int world, int level)
    {
        control.items.crystalCount = 0;

        if (world > 0 && level > 0)
        {
            if (level < 5)
            {
                for (int i = 0; i < 3; i++)
                    control.items.lettersCollected[i] = control.progress.lettersCollected[world - 1, level - 1, i];
                control.items.hasGem = control.progress.gemsCollected[world - 1, level - 1];
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
                control.items.lettersCollected[i] = false;
            control.items.hasGem = false;
        }
    }

    public bool RetrieveSettings()
    {
        string saveFilePath = Application.persistentDataPath + "/settings.kzt";

        if (File.Exists(saveFilePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);

            control.settings = (Settings)bf.Deserialize(file);
            file.Close();
        }

        return File.Exists(saveFilePath);
    }

    public void SaveSettings()
    {
        string saveFilePath = Application.persistentDataPath + "/settings.kzt";

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);

        Settings settings = control.settings;

        bf.Serialize(file, settings);
        file.Close();
    }

    public void saveItemsToCheckpoint()
    {
        itemsFromLastCheckpoint.crystalCount = items.crystalCount;
        for (int i = 0; i < 3; i++)
            itemsFromLastCheckpoint.lettersCollected[i] = items.lettersCollected[i];
        itemsFromLastCheckpoint.hasGem = items.hasGem;
    }

    public void revertItemsToLastCheckpoint()
    {
        items.crystalCount = itemsFromLastCheckpoint.crystalCount;
        for (int i = 0; i < 3; i++)
            items.lettersCollected[i] = itemsFromLastCheckpoint.lettersCollected[i];
        items.hasGem = itemsFromLastCheckpoint.hasGem;
    }

    public class Items
    {
        public int crystalCount;
        public bool[] lettersCollected = new bool[3];
        public bool hasGem;

        public int totalCrystalsInLevel;
        public int crystalsShownInGUI;
        public int crystalsInCollection;
    }

    [Serializable]
    public class Settings
    {
        public int musicVolume = 70;
        public int effectsVolume = 100;

        public KeyCode upKey = KeyCode.W;
        public KeyCode downKey = KeyCode.S;
        public KeyCode leftKey = KeyCode.A;
        public KeyCode rightKey = KeyCode.D;

        public KeyCode selectKey = KeyCode.Return;
        public KeyCode cancelKey = KeyCode.Backspace;
        public KeyCode pauseKey = KeyCode.P;

        public bool usingGamepad;
    }

    [Serializable]
    public class Progress
    {
        public int[,] crystalHighScores = new int[5, 3];

        //[World, Level, Letter Index]
        public bool[,,] lettersCollected = new bool[5, 3, 3];
        //[World, Level]
        public bool[,] gemsCollected = new bool[5, 4];
        //[World, Level]
        public bool[,] levelsUnlocked = new bool[5, 5];

        //public bool gameCompleted;

        public int currentWorld;
        public int currentLevel;

        //Initializes the progress values to their default initial state
        public void InitNewGameProgress()
        {
            for (int i = 0; i < crystalHighScores.GetLength(0); i++)
            {
                for (int j = 0; j < crystalHighScores.GetLength(1); j++)
                {
                    crystalHighScores[i, j] = 0;
                }
            }

            for (int i = 0; i < lettersCollected.GetLength(0); i++)
            {
                for (int j = 0; j < lettersCollected.GetLength(1); j++)
                {
                    for (int k = 0; k < lettersCollected.GetLength(2); k++)
                    {
                        lettersCollected[i, j, k] = false;
                    }
                }
            }

            for (int i = 0; i < gemsCollected.GetLength(0); i++)
            {
                for (int j = 0; j < gemsCollected.GetLength(1); j++)
                    gemsCollected[i, j] = false;
            }

            for (int i = 0; i < levelsUnlocked.GetLength(0); i++)
            {
                for (int j = 0; j < levelsUnlocked.GetLength(1); j++)
                {
                    levelsUnlocked[i, j] = (i == 0 && j == 0 ? true : false);
                }
            }

            currentWorld = 1;
            currentLevel = 1;
        }

        public int GetCrystalPercentage()
        {
            return 0; //Temporary
        }

        public int GetTotalPercentage()
        {
            return 0; //Will do later
        }

        public int GetWorldsUnlocked()
        {
            for (int i = 0; i < 5; i++)
            {
                if (!levelsUnlocked[i, 0])
                    return i;
            }
            return 5;
        }

        public int GetRedGemCount()
        {
            int total = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gemsCollected[i, j])
                        total++;
                }
            }

            return total;
        }
    }

    private void GetSaveFiles()
    {
        for (int i = 0; i < 4; i++)
        {
            string saveFilePath;

            if (i > 0)
                saveFilePath = Application.persistentDataPath + "/saveFile" + i + ".kzt";
            else
                saveFilePath = Application.persistentDataPath + "/autosave.kzt";

            if (File.Exists(saveFilePath))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(saveFilePath, FileMode.Open);

                control.saveFiles[i] = (Progress)bf.Deserialize(file);
                file.Close();
            }
            else
            {
                control.saveFiles[i] = null;
            }
        }
    }
}
