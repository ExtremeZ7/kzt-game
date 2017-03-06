using UnityEngine;
using System.Collections;

public class flipObjectTowardsOtherObject : MonoBehaviour {

	public GameObject objectToFlip;
	public Transform objectToPointTo;

	void Update () {
		Vector3 objectPosition = objectToFlip.transform.position;
		Vector3 targetPosition = objectToPointTo.position;

		if ((targetPosition.x >= objectPosition.x && objectToFlip.transform.localScale.x <= 0) || (targetPosition.x < objectPosition.x && objectToFlip.transform.localScale.x > 0)) {
			Vector3 theScale = objectToFlip.transform.localScale;
			theScale.x *= -1;
			objectToFlip.transform.localScale = theScale;
		}
	}
}
