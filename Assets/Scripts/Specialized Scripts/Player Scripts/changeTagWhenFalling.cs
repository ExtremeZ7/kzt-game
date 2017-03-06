using UnityEngine;
using System.Collections;

public class changeTagWhenFalling : MonoBehaviour {

	//Specialized Script

	public string tagToAssign;
	public float minimumFallSpeed = 0.0f;

	[Tooltip("Turn This On For Head Stomping")]
	public bool reverseFalling;


	private Vector3 previousLocation;
	private float fallSpeed;

	void Start(){
		previousLocation = transform.position;
	
		if(reverseFalling)
			minimumFallSpeed *= -1;
	}

	void Update(){
		fallSpeed = previousLocation.y - transform.position.y;
		previousLocation = transform.position;

		if ((!reverseFalling && fallSpeed > minimumFallSpeed) || (reverseFalling && fallSpeed < minimumFallSpeed)) {
			gameObject.tag = tagToAssign;
		} else {
			gameObject.tag = "Untagged";
		}
	}
}
