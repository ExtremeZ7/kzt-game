using UnityEngine;
using System.Collections;

public class SlideIfOnSlipperyFloor : MonoBehaviour {

	private PlayerControl playerControl;

	public string[] tags;

	void Start () {
		playerControl = GetComponent<PlayerControl>()	;
	}

	void OnTriggerStay2D(Collider2D coll){
		if(tags.Contains(coll.gameObject.tag)){
			playerControl.changeMovementState(PlayerControl.MovementState.SlipperyFloor);
		}
	}
}
