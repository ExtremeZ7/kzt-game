using UnityEngine;
using UnityEngine.SceneManagement;
using AssemblyCSharp;

public class LevelManager : MonoBehaviour
{

    public enum Key
    {
        C,
        CSharp,
        D,
        DSharp,
        E,
        F,
        FSharp,
        G,
        GSharp,
        A,
        ASharp,
        B}

    ;

    public enum Scale
    {
        Major,
        Minor}

    ;

    public enum LevelState
    {
        PlayingLevel,
        RespawningPlayer,
        LevelComplete}

    ;

    private LevelState levelState;
    private string completeLevelName;

    private PlayerController playerControl;
    private keepRespawnState respawnStateKeeper;

    private bool levelReset = false;
    private float timer;

    private float loadingCloseDelay = 1.5f;
    private float loadingOpenDelay = 1.2f;

    private Transform lastCheckpoint;

    [Header("Miscellaneous Level Settings")]
    public float musicBPM;

    private Transform mainCamera;
    private PlayerController.MovementState savedMovementState = PlayerController.MovementState.Normal;

    [HideInInspector]
    public bool noGravity;

    void Start()
    {
        lastCheckpoint = GameObject.FindGameObjectWithTag("Checkpoints").transform.GetChild(0);
        respawnStateKeeper = gameObject.GetComponent<keepRespawnState>();
        timer = loadingCloseDelay;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        GameControl.control.LoadItemsFromProgress(GameControl.control.progress.currentWorld, GameControl.control.progress.currentLevel);
        GameControl.control.saveItemsToCheckpoint();

        levelState = LevelState.PlayingLevel;

        GameControl.control.barrierIsOpen = true;
    }

    void Update()
    {
        if (Help.WaitForPlayer(ref playerControl))
        {
            switch (levelState)
            {
                case LevelState.PlayingLevel:
                    break;
                case LevelState.RespawningPlayer:
                    if (Help.UseAsTimer(ref timer))
                    {
                        if (!levelReset)
                        {
                            if (GameControl.control.barrierIsOpen)
                            {
                                timer = 0.6f;
                                levelReset = true;
                                drawHelpfulHint helperHint = GameObject.FindObjectOfType<drawHelpfulHint>();
                                if (helperHint != null)
                                    Object.Destroy(helperHint.gameObject);
                            }
                            else
                            {
                                ResetPlayer();
                            }
                            GameControl.control.barrierIsOpen = !GameControl.control.barrierIsOpen;
                        }
                        else
                        {
                            LevelReset();
                            timer = loadingOpenDelay;
                        }
                    }
                    break;
                case LevelState.LevelComplete:
                    GameControl.control.barrierIsOpen = false;
                    if (Help.UseAsTimer(ref timer))
                    {
                        SceneManager.LoadScene("World Map");
                    }
                    break;
            }
        }
    }

    public void StartRespawnSequence()
    {
        timer = loadingCloseDelay;
        playerControl.gameObject.transform.parent = null;
        Vector2 theScale = playerControl.transform.localScale;
        theScale.x = Mathf.Abs(theScale.x);
        playerControl.transform.localScale = theScale;
        playerControl.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, playerControl.GetComponent<Rigidbody2D>().velocity.y);
        playerControl.gameObject.SetActive(false);
        playerControl.GetComponent<Rigidbody2D>().gravityScale = 4;
        levelState = LevelState.RespawningPlayer;
    }

    void ResetPlayer()
    {
        playerControl.gameObject.SetActive(true);
        playerControl.shieldAnimator.SetTrigger("Force Remove Shield");
        playerControl.ChangeMovementState(savedMovementState);
        levelState = LevelState.PlayingLevel;
        playerControl.transform.position = lastCheckpoint.position;
        playerControl.transform.parent = GameObject.FindGameObjectWithTag("GAMEOBJECTS").transform;
        mainCamera.position = new Vector3(mainCamera.position.x, playerControl.transform.position.y + 10, -10);
    }

    void LevelReset()
    {
        FindObjectOfType<CameraControl>().enabled = true;

        destroySelfWhenStateReset[] objectsToDestroy = FindObjectsOfType<destroySelfWhenStateReset>();
        for (int i = 0; i < objectsToDestroy.Length; i++)
        {
            iTween.Destroy(objectsToDestroy[i].gameObject);
        }

        travelAcrossARail[] railsToCheck = FindObjectsOfType<travelAcrossARail>();
        for (int i = 0; i < railsToCheck.Length; i++)
        {
            railsToCheck[i].StateReset();
        }

        GameControl.control.revertItemsToLastCheckpoint();

        GameControl.control.items.crystalsShownInGUI = GameControl.control.items.crystalCount;
        GameControl.control.items.crystalsInCollection = GameControl.control.items.crystalCount;

        respawnStateKeeper.resetRespawnState();
        levelReset = false;
    }

    public void saveCheckpoint(Transform newCheckpoint)
    {
        savedMovementState = playerControl.getMovementState();
        playerControl.gameObject.transform.parent = null;
        lastCheckpoint = newCheckpoint;
        GameControl.control.saveItemsToCheckpoint();
        respawnStateKeeper.saveNewRespawnState();
    }

    public void CompleteLevel()
    {
        int world = GameControl.control.progress.currentWorld - 1;
        int level = GameControl.control.progress.currentLevel - 1;

        if (level < 3)
        {
            if (GameControl.control.items.crystalCount > GameControl.control.progress.crystalHighScores[world, level])
                GameControl.control.progress.crystalHighScores[world, level] = GameControl.control.items.crystalCount;

            for (int i = 0; i < 3; i++)
            {
                if (GameControl.control.items.lettersCollected[i])
                    GameControl.control.progress.lettersCollected[world, level, i] = GameControl.control.items.lettersCollected[i];
            }
        }
		 
        if (level < 4)
        {
            if (GameControl.control.items.hasGem)
                GameControl.control.progress.gemsCollected[world, level] = GameControl.control.items.hasGem;
        }

        int nextWorld = level < 4 ? world : (world + 1);
        int nextLevel = level < 4 ? (level + 1) : 0;

        if (world < 4 || level < 4)
        {
            GameControl.control.progress.levelsUnlocked[nextWorld, nextLevel] = true;
        }
        /*else
			GameControl.control.progress.theMachineisUnlocked = true;
		*/

        GameControl.control.SaveProgress(0);

        levelState = LevelState.LevelComplete;
        timer = loadingCloseDelay;
    }
}
