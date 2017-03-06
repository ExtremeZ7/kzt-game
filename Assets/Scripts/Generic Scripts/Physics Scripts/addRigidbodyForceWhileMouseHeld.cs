using UnityEngine;
using System.Collections;

public class addRigidbodyForceWhileMouseHeld : MonoBehaviour {

	public Rigidbody2D rb2d;

	[Space(10)]
	public float xForce;
	public float yForce;

	[Space(10)]
	public bool setInsteadOfAdd;
	[Tooltip("Apply force When Mouse Is Not Held Instead")]
	public bool reverseMouseActivation;

	[Space(10)]
	[Range(0.0f,2.0f)]
	public int mouseButton;

	void Start () {
		if (rb2d == null)
			rb2d = GetComponent<Rigidbody2D>();
	}

	void Update () {
		if (Input.GetMouseButton (mouseButton) != reverseMouseActivation) {
			Vector2 force = new Vector2 (xForce, yForce);
			if (!setInsteadOfAdd)
				rb2d.AddForce (force);
			else
				rb2d.velocity = force;
		}
	}
}
