using UnityEngine;
using System.Collections;

public class sinkWhilePlayerCollide : MonoBehaviour {

	[Header("Main Variables")]
	public bool sinkOtherObjectInstead = false;
	public GameObject otherObject;
	private Vector3 target;

	private bool sinking = false;
	private float defaultY;


	[Header("Speed")]
	private float currentSpeed;
	public float sinkSpeed;
	public float riseSpeed;

	// Use this for initialization
	void Start () {
		if(!sinkOtherObjectInstead)
			defaultY = transform.position.y;
		else
			target = new Vector3(otherObject.transform.position.x,otherObject.transform.position.y,0);
	}
	
	// Update is called once per frame
	void Update () {

		if(sinking)
			currentSpeed = sinkSpeed;
		else
			currentSpeed = riseSpeed;

		if(!sinkOtherObjectInstead)
			transform.position = Vector3.MoveTowards(transform.position,new Vector3(transform.position.x,defaultY,0.0f),currentSpeed * Time.deltaTime);
		else{
			if(sinking)
				otherObject.transform.position = new Vector3(otherObject.transform.position.x,otherObject.transform.position.y + (currentSpeed * Time.deltaTime),0.0f);
			else
				otherObject.transform.position = Vector3.MoveTowards(otherObject.transform.position,target,currentSpeed * Time.deltaTime);
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		sinking = true;
	}

	void OnCollisionExit2D(Collision2D coll){
		sinking = false;
	}
}
