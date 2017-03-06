using UnityEngine;
using System.Collections;

public class enableObjectWhileMouseHeld : MonoBehaviour {

	public GameObject objectToEnable;

	[Space(10)]
	public bool reverse = false;
	public bool enableOnlyWhenDown = false;
	public bool disableOnlyWhenUp = false;

	[Space(10)]
	public bool setWhenHeld = true;
	public bool reverseWhenNotHeld = true;
	
	void Start(){
		if(reverseWhenNotHeld)
			objectToEnable.SetActive(reverse);
	}

	void Update () {
		if (setWhenHeld && (enableOnlyWhenDown ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0))) {
			objectToEnable.SetActive(!reverse);
		}
		if (reverseWhenNotHeld && (disableOnlyWhenUp ? Input.GetMouseButtonUp(0) : !Input.GetMouseButton(0))) {
			objectToEnable.SetActive(reverse);
		}
	}
}
