using UnityEngine;
using System.Collections;

public class setAniParamFloatWhileMouseHeld : MonoBehaviour {

	public string parameterName;
	public float value;

	[Space(10)]
	public Animator animator;

	[Space(10)]
	public bool enableOnlyWhenDown = false;

	void Start(){
		if(animator == null)
			animator = GetComponent<Animator>();
	}

	void Update () {
		if ((enableOnlyWhenDown ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0))) {
			animator.SetFloat(parameterName,value);
		}
	}
}
