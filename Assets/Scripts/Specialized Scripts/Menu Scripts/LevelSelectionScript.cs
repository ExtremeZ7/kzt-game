using UnityEngine;
using Controllers;

public class LevelSelectionScript : MonoBehaviour
{

    public enum SelectionState
    {
        SelectingAWorld,
        SelectingALevel,
        LevelSelected,
        Loading}

    ;

    SelectionState selectionState;

    int levelSelectIndex;
    int worldSelectIndex;

    KeyCode leftKey;
    KeyCode rightKey;
    KeyCode selectKey;
    KeyCode cancelKey;

    int worldsUnlocked = 5;

    void Start()
    {
        GameController.Instance.barrierIsOpen = true;

        leftKey = GameController.Instance.settings.leftKey;
        rightKey = GameController.Instance.settings.rightKey;
        selectKey = GameController.Instance.settings.selectKey;
        cancelKey = GameController.Instance.settings.cancelKey;

        levelSelectIndex = 1;
        worldSelectIndex = GameController.Instance.progress.currentWorld;

        if (GameController.Instance.restrictLocked)
        {
            worldsUnlocked = GameController.Instance.progress.GetWorldsUnlocked();
        }
    }

    void Update()
    {
        if (!GameController.Instance.paused)
        {
            switch (selectionState)
            {
                case SelectionState.SelectingAWorld:	
                    if (Input.GetKeyDown(leftKey))
                        worldSelectIndex -= worldSelectIndex > 1 ? 1 : 0;
                    if (Input.GetKeyDown(rightKey))
                        worldSelectIndex += worldSelectIndex < worldsUnlocked ? 1 : 0;
                    if (Input.GetKeyDown(selectKey))
                        selectionState = SelectionState.SelectingALevel;
                    break;

                case SelectionState.SelectingALevel:	
                    if (Input.GetKeyDown(leftKey))
                        levelSelectIndex = levelSelectIndex == 1 ? 5 : levelSelectIndex - 1;
                    if (Input.GetKeyDown(rightKey))
                        levelSelectIndex = (levelSelectIndex % 5) + 1;
                    if (Input.GetKeyDown(selectKey))
                    {
                        if (FindObjectOfType<LevelSelectWheelScript>().AttemptLevelEntry(worldSelectIndex, levelSelectIndex))
                        {
                            GameController.Instance.progress.currentWorld = worldSelectIndex;
                            GameController.Instance.progress.currentLevel = levelSelectIndex;

                            GameController.Instance.MoveToOtherScene("world" + worldSelectIndex + "_level" + levelSelectIndex, 0f);
                            selectionState = SelectionState.LevelSelected;
                        }
                    }
                    if (Input.GetKeyDown(cancelKey))
                        selectionState = SelectionState.SelectingAWorld;
                    break;

                case SelectionState.LevelSelected:
				//Put Some Animations Here, I Guess. . .
                    break;
            }
        }
    }

    public int getWorldSelectIndex()
    {
        return worldSelectIndex;
    }

    public int getLevelSelectIndex()
    {
        return levelSelectIndex;
    }

    public void setLevelSelectIndex(int index)
    {
        levelSelectIndex = index;
    }

    public int getSelectionState()
    {
        return (int)selectionState;
    }
}
