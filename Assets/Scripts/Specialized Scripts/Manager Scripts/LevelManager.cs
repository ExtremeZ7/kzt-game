using UnityEngine;
using UnityEngine.SceneManagement;
using Controllers;

namespace Managers
{
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

        [HideInInspector]
        public bool noGravity;

        void Start()
        {
            lastCheckpoint = GameObject.FindGameObjectWithTag("Checkpoints").transform.GetChild(0);
            respawnStateKeeper = gameObject.GetComponent<keepRespawnState>();
            timer = loadingCloseDelay;
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;

            GameController.Instance.LoadItemsFromProgress(GameController.Instance.progress.currentWorld, GameController.Instance.progress.currentLevel);
            GameController.Instance.saveItemsToCheckpoint();

            levelState = LevelState.PlayingLevel;

            GameController.Instance.barrierIsOpen = true;
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
                                if (GameController.Instance.barrierIsOpen)
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
                                GameController.Instance.barrierIsOpen = !GameController.Instance.barrierIsOpen;
                            }
                            else
                            {
                                LevelReset();
                                timer = loadingOpenDelay;
                            }
                        }
                        break;
                    case LevelState.LevelComplete:
                        GameController.Instance.barrierIsOpen = false;
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
            levelState = LevelState.PlayingLevel;
            playerControl.transform.position = lastCheckpoint.position;
            playerControl.transform.parent = GameObject.FindGameObjectWithTag("GAMEOBJECTS").transform;
            mainCamera.position = new Vector3(mainCamera.position.x, playerControl.transform.position.y + 10, -10);
        }

        void LevelReset()
        {
            FindObjectOfType<CameraController>().enabled = true;

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

            GameController.Instance.revertItemsToLastCheckpoint();

            GameController.Instance.items.crystalsShownInGUI = GameController.Instance.items.crystalCount;
            GameController.Instance.items.crystalsInCollection = GameController.Instance.items.crystalCount;

            respawnStateKeeper.resetRespawnState();
            levelReset = false;
        }

        public void saveCheckpoint(Transform newCheckpoint)
        {
            playerControl.gameObject.transform.parent = null;
            lastCheckpoint = newCheckpoint;
            GameController.Instance.saveItemsToCheckpoint();
            respawnStateKeeper.saveNewRespawnState();
        }

        public void CompleteLevel()
        {
            int world = GameController.Instance.progress.currentWorld - 1;
            int level = GameController.Instance.progress.currentLevel - 1;

            if (level < 3)
            {
                if (GameController.Instance.items.crystalCount > GameController.Instance.progress.crystalHighScores[world, level])
                    GameController.Instance.progress.crystalHighScores[world, level] = GameController.Instance.items.crystalCount;

                for (int i = 0; i < 3; i++)
                {
                    if (GameController.Instance.items.lettersCollected[i])
                        GameController.Instance.progress.lettersCollected[world, level, i] = GameController.Instance.items.lettersCollected[i];
                }
            }
		 
            if (level < 4)
            {
                if (GameController.Instance.items.hasGem)
                    GameController.Instance.progress.gemsCollected[world, level] = GameController.Instance.items.hasGem;
            }

            int nextWorld = level < 4 ? world : (world + 1);
            int nextLevel = level < 4 ? (level + 1) : 0;

            if (world < 4 || level < 4)
            {
                GameController.Instance.progress.levelsUnlocked[nextWorld, nextLevel] = true;
            }
            /*else
			GameControl.control.progress.theMachineisUnlocked = true;
		*/

            GameController.Instance.SaveProgress(0);

            levelState = LevelState.LevelComplete;
            timer = loadingCloseDelay;
        }
    }
}