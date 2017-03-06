using UnityEngine;
using System.Collections;

public class setAniParamBoolWhileMouseHeld : MonoBehaviour {

	public string parameterName;

	[Space(10)]
	public Animator animator;

	[Space(10)]
	public bool reverse = false;
	public bool enableOnlyWhenDown = false;
	public bool disableOnlyWhenUp = false;

	[Space(10)]
	public bool setWhenHeld = true;
	public bool reverseWhenNotHeld = false;

	void Start(){
		if(animator == null)
			animator = GetComponent<Animator>();
	}

	void Update () {
		if (setWhenHeld && (enableOnlyWhenDown ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0))) {
			animator.SetBool(parameterName,!reverse);
		}
		if (reverseWhenNotHeld && (disableOnlyWhenUp ? Input.GetMouseButtonUp(0) : !Input.GetMouseButton(0))) {
			animator.SetBool(parameterName,reverse);
		}
	}
}
