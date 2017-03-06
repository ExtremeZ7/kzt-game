using UnityEngine;
using System.Collections;

public class initializeRigidbodyVelocity : MonoBehaviour {

	public Rigidbody2D objectWithRigidbody;

	[Space(10)]
	public float xSpeed;
	public float ySpeed;

	[Space(10)]
	public bool randomlyFlipX;
	public bool randomlyFlipY;

	void Start () {
		if (objectWithRigidbody == null)
			objectWithRigidbody = GetComponent<Rigidbody2D>();

		objectWithRigidbody.velocity = new Vector2(xSpeed * (randomlyFlipX ? Mathf.Sign(Random.Range(-1.0f,1.0f)) : 1.0f),
																					ySpeed * (randomlyFlipY ? Mathf.Sign(Random.Range(-1.0f,1.0f)) : 1.0f));

		objectWithRigidbody.velocity = new Vector2 (xSpeed * Mathf.Sign(objectWithRigidbody.gameObject.transform.localScale.x),
																						ySpeed * Mathf.Sign(objectWithRigidbody.gameObject.transform.localScale.y));
	}
}
