using UnityEngine;
using System.Collections;

public class animationEventScripts : MonoBehaviour {

	private Animator animator;

	public bool executeWhileInTransition = true;
	[Space(10)]

	[Header("Spawner Only")]
	public Transform spawner;

	[Header("RB2D Force Only")]
	public float rotationOffset;
	public float forceMagnitude;

	void Start(){
		animator = GetComponent<Animator>();
	}

	public void activateParameter(string param){
		animator.SetBool(param,true);
	}

	public void deactivateParameter(string param){
		animator.SetBool(param,false);
	}

	public void destroySelf(){
		Object.Destroy(gameObject);
	}

	public void destroyParent(int additionalDepth){
		GameObject parentToDestroy = transform.parent.gameObject;

		for(int i = 0; i < additionalDepth; i++)
			parentToDestroy = parentToDestroy.transform.parent.gameObject;

		Object.Destroy(gameObject);
	}

	public void deactivateSelf(){
		gameObject.SetActive(false);
	}

	public void spawnPrefab(GameObject newPrefab){
		if(executeWhileInTransition || !animator.IsInTransition(0))
			Instantiate(newPrefab,transform.position,Quaternion.identity);
	}

	public void spawnPrefabAtSpawner(GameObject newPrefab){
		Instantiate(newPrefab,spawner.transform.position,Quaternion.identity);
	}

	public void spawnPrefabAtSpawnerAndCopyScaleAndRotation(GameObject newPrefab){
		GameObject newObject = Instantiate(newPrefab,spawner.transform.position,Quaternion.identity)  as GameObject;
		newObject.transform.localScale = transform.localScale;
		newObject.transform.rotation = spawner.transform.rotation;
	}

	public void spawnPrefabAtSpawnerAndAddRotatedRigidbodyForce(GameObject newPrefab){
		GameObject newObject = Instantiate(newPrefab,spawner.transform.position,Quaternion.identity)  as GameObject;
		Rigidbody2D rb2d = newObject.GetComponent<Rigidbody2D>();
		rb2d.velocity = Trigo.GetRotatedVector(transform.rotation.eulerAngles.z + rotationOffset,forceMagnitude);
	}

	public void changeSpeed(int newSpeed){
		animator.speed = newSpeed;
	}

	public void setBoolToTrue(string name){
		animator.SetBool (name, true);
	}

	public void activateTrigger(string name){
		animator.SetTrigger(name);
	}
}