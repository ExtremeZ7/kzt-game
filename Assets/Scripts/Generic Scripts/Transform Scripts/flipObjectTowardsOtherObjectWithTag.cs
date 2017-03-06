using UnityEngine;
using System.Collections;

public class flipObjectTowardsOtherObjectWithTag : MonoBehaviour {

	public GameObject objectToFlip;
	public string targetTag;

	private GameObject objectToPointTo;

	[Space(10)]
	public bool reverse;

	[Space(10)]
	public bool onlyTriggerAtOnce;

	void Start(){
		if (objectToFlip == null)
			objectToFlip = this.gameObject;
	}

	void Update () {
		if (objectToPointTo != null) {
			Vector3 objectPosition = objectToFlip.transform.position;
			Vector3 targetPosition = objectToPointTo.transform.position;

			if ((targetPosition.x >= objectPosition.x && objectToFlip.transform.localScale.x <= 0) || (targetPosition.x < objectPosition.x && objectToFlip.transform.localScale.x > 0)) {
				Vector3 theScale = objectToFlip.transform.localScale;
				theScale.x *= -1;
				objectToFlip.transform.localScale = theScale;
			}

			if (onlyTriggerAtOnce)
				this.enabled = false;
		}
		else{
			objectToPointTo = GameObject.FindGameObjectWithTag(targetTag);
		}
	}
}
