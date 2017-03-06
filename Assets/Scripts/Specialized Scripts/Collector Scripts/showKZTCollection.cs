using UnityEngine;
using System.Collections;

public class showKZTCollection : MonoBehaviour {

	public float hideSpeed;
	public float showSpeed;
	public float hideDelayTime;
	private float hideDelay;
	private Transform hidePoint;
	private Transform showPoint;

	void Start(){
		hidePoint = transform.parent.transform.GetChild(1);
		showPoint = transform.parent.transform.GetChild(2);

		transform.position = hidePoint.position;
	}

	void Update () {
		for (int i = 0; i < 3; i++) {
			if (GameControl.control.items.lettersCollected [i] != transform.GetChild (i).gameObject.GetComponent<Animator> ().GetBool ("Collected")) {
				transform.GetChild (i).gameObject.GetComponent<Animator> ().SetBool ("Collected", GameControl.control.items.lettersCollected [i]);
				if (GameControl.control.items.lettersCollected[i])
					hideDelay = hideDelayTime;
			}
		}

		if(Input.GetKeyDown("space"))
			hideDelay = hideDelayTime;

		hideDelay = Mathf.MoveTowards(hideDelay,0.0f,Time.deltaTime);

		transform.position = Vector3.MoveTowards(transform.position,
			(hideDelay > 0.0f ? showPoint.position : hidePoint.position),
			(hideDelay > 0.0f ? showSpeed : hideSpeed) * Time.deltaTime);
	}
}
