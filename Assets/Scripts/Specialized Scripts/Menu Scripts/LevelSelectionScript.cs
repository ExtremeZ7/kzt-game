using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectionScript : MonoBehaviour {

	public enum SelectionState{SelectingAWorld,SelectingALevel,LevelSelected,Loading};

	private SelectionState selectionState;

	private int levelSelectIndex;
	private int worldSelectIndex;

	private KeyCode leftKey;
	private KeyCode rightKey;
	private KeyCode selectKey;
	private KeyCode cancelKey;

	private int worldsUnlocked = 5;

	void Start (){
		GameControl.control.barrierIsOpen = true;

		leftKey = GameControl.control.settings.leftKey;
		rightKey = GameControl.control.settings.rightKey;
		selectKey = GameControl.control.settings.selectKey;
		cancelKey = GameControl.control.settings.cancelKey;

		levelSelectIndex = 1;
		worldSelectIndex = GameControl.control.progress.currentWorld;

		if(GameControl.control.restrictLocked){
			worldsUnlocked = GameControl.control.progress.GetWorldsUnlocked();
		}
	}

	void Update () {
		if(!GameControl.control.paused){
			switch(selectionState){
			case SelectionState.SelectingAWorld:	
				if(Input.GetKeyDown(leftKey))
					worldSelectIndex -= worldSelectIndex > 1 ? 1 : 0;
				if(Input.GetKeyDown(rightKey))
					worldSelectIndex += worldSelectIndex < worldsUnlocked ? 1 : 0;
				if(Input.GetKeyDown(selectKey))
					selectionState = SelectionState.SelectingALevel;
				break;

			case SelectionState.SelectingALevel:	
				if(Input.GetKeyDown(leftKey))
					levelSelectIndex = levelSelectIndex == 1 ? 5: levelSelectIndex - 1;
				if(Input.GetKeyDown(rightKey))
					levelSelectIndex = (levelSelectIndex % 5) + 1;
				if(Input.GetKeyDown(selectKey)){
					if(FindObjectOfType<LevelSelectWheelScript>().AttemptLevelEntry(worldSelectIndex, levelSelectIndex))
					{
						GameControl.control.progress.currentWorld = worldSelectIndex;
						GameControl.control.progress.currentLevel = levelSelectIndex;

						GameControl.control.MoveToOtherScene("world" + worldSelectIndex + "_level" + levelSelectIndex, 0f);
						selectionState = SelectionState.LevelSelected;
					}
				}
				if(Input.GetKeyDown(cancelKey))
					selectionState = SelectionState.SelectingAWorld;
				break;

			case SelectionState.LevelSelected:
				//Put Some Animations Here, I Guess. . .
				break;
			}
		}
	}

	public int getWorldSelectIndex (){
		return worldSelectIndex;
	}

	public int getLevelSelectIndex (){
		return levelSelectIndex;
	}

	public void setLevelSelectIndex (int index){
		levelSelectIndex = index;
	}

	public int getSelectionState (){
		return (int) selectionState;
	}
}
