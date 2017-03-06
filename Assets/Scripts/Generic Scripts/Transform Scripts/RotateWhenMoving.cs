using UnityEngine;
using System.Collections;

public class RotateWhenMoving : MonoBehaviour {

	public float rotationAmount;

	private Vector3 previousLocation;

	void Start(){
		previousLocation = transform.position;
	}

	void Update () {
		float angleSpeed = Vector3.Distance(transform.position, previousLocation) * Mathf.Sign(previousLocation.x - transform.position.x) * rotationAmount * 60f;
		transform.rotation = Quaternion.Euler(new Vector3(0,0,transform.eulerAngles.z + angleSpeed));

		previousLocation = transform.position;
	}
}