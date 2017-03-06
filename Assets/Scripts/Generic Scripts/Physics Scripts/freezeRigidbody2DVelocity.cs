using UnityEngine;
using System.Collections;

public class freezeRigidbody2DVelocity : MonoBehaviour {

	public Rigidbody2D rigidbodyOfObject;

	public bool freezeXVelocity = true;
	public bool freezeYVelocity = true;

	void Awake(){
		if(rigidbodyOfObject == null)
			rigidbodyOfObject = GetComponent<Rigidbody2D>();
	}
	void Update () {
		rigidbodyOfObject.velocity = new Vector2(freezeXVelocity ? 0f : rigidbodyOfObject.velocity.x,
			freezeYVelocity ? 0f : rigidbodyOfObject.velocity.y);
	}
}
