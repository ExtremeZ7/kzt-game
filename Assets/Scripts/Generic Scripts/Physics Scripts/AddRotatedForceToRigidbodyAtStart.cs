using UnityEngine;
using System.Collections;

public class AddRotatedForceToRigidbodyAtStart : MonoBehaviour {

	public GameObject self;
	public bool useSelfRotation = true;

	[Space(10)]
	public float rotationOffset;
	public float forceMagnitude;

	private float rotationAngle;

	void Start () {
		rotationAngle = ((useSelfRotation ? transform.rotation.eulerAngles.z : 0f) + rotationOffset) % 360f;

		if(self == null)
			self = gameObject;

		Rigidbody2D rb2d = self.GetComponent<Rigidbody2D>();
		rb2d.velocity = Trigo.GetRotatedVector(rotationAngle,forceMagnitude);
	}
}
