using UnityEngine;
using System.Collections;

public class disableGameObjectWhilePlayerIsMoving : MonoBehaviour {

	/*private PlayerControl pc;
	public GameObject objectToDisable;
	public string tagOfObjectToFind;

	[Space(10)]
	public bool reverse = false;

	[Space(10)]
	public bool disableWhenMoving = true;
	public bool enableWhenNot = true;

	void Start () {
		pc = FindObjectOfType<PlayerControl>();
	}

	void Update () {
		if (objectToDisable	!= null) {
			if (pc.getDirection() == 0 && pc.ground) {
				if (enableWhenNot)
					objectToDisable.SetActive (!reverse);
			} else {
				if (disableWhenMoving)
					objectToDisable.SetActive (reverse);
			}
		}
		else{
			objectToDisable = GameObject.FindGameObjectWithTag(tagOfObjectToFind);
		}
	}*/
}
