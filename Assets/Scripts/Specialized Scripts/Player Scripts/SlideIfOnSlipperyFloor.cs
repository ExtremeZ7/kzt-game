using UnityEngine;
using System.Collections;

public class SlideIfOnSlipperyFloor : MonoBehaviour {

	private PlayerController playerControl;

	public string[] tags;

	void Start () {
		playerControl = GetComponent<PlayerController>()	;
	}

	void OnTriggerStay2D(Collider2D coll){
		if(tags.Contains(coll.gameObject.tag)){
			playerControl.ChangeMovementState(PlayerController.MovementState.SlipperyFloor);
		}
	}
}
