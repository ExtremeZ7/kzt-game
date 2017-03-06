using UnityEngine;
using System.Collections;

public class keepRespawnState : MonoBehaviour {

	public GameObject objectToRespawn;
	private GameObject currentRespawnState;

	private string originalName;

	void Awake(){
		iTween.Init(gameObject);
	}

	void Start () {
		saveNewRespawnState();
		originalName = objectToRespawn.name;
	}

	public void saveNewRespawnState(){
		if(currentRespawnState != null){
			iTween.Destroy(currentRespawnState);
		}
		currentRespawnState = (GameObject) iTween.Instantiate(objectToRespawn);
		currentRespawnState.transform.position = objectToRespawn.transform.position;
		currentRespawnState.transform.rotation = Quaternion.identity;
		iTween.Init(currentRespawnState);
		currentRespawnState.transform.parent = objectToRespawn.transform.parent;
		currentRespawnState.name = objectToRespawn.name + " Respawn State";
		currentRespawnState.SetActive(false);
	}

	public void resetRespawnState(){
		iTween.Destroy(objectToRespawn.gameObject);
		objectToRespawn = (GameObject) iTween.Instantiate(currentRespawnState);
		objectToRespawn.transform.position = currentRespawnState.transform.position;
		objectToRespawn.transform.rotation = Quaternion.identity;
		iTween.Init(objectToRespawn);
		objectToRespawn.transform.parent = currentRespawnState.transform.parent;
		objectToRespawn.name = originalName;
		objectToRespawn.SetActive(true);
	}
}
