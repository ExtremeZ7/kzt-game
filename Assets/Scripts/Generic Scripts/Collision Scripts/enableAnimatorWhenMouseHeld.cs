using UnityEngine;
using System.Collections;

public class enableAnimatorWhenMouseHeld : MonoBehaviour {

	public Animator animator;

	[Space(10)]
	public bool reverse = false;
	public bool enableOnlyWhenDown = false;
	public bool disableOnlyWhenUp = false;

	[Space(10)]
	public bool enableWhenHeld = true;
	public bool disableWhenNot = true;

	void Start () {
		if (animator == null)
			animator = GetComponent<Animator>();
	}

	void Update () {
		if (enableWhenHeld && (enableOnlyWhenDown ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0))){
			animator.enabled = !reverse;
		}
		if (disableWhenNot && (disableOnlyWhenUp ? Input.GetMouseButtonUp(0) : !Input.GetMouseButton(0))){
			animator.enabled = reverse;
		}
	}
}
