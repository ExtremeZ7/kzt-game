using UnityEngine;
using System.Collections;

public class enableObjectBasedOnAnimatorParam : MonoBehaviour {

	public GameObject objectToEnable;
	[Tooltip("Not Required. Defaults to self if not set")]
	public GameObject objectWithAnimator;
	private Animator ani;

	[Space(10)]
	public string param;

	[Space(10)]
	public bool reverse = false;
	public bool setWhenTrue = true;
	public bool reverseWhenNotTrue = true;

	void Start () {
		if (objectWithAnimator == null)
			objectWithAnimator = this.gameObject;
		ani = objectWithAnimator.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (setWhenTrue) {
			objectToEnable.SetActive(ani.GetBool(param) == reverse);
		}
		if (reverseWhenNotTrue) {
			objectToEnable.SetActive(ani.GetBool(param) == !reverse);
		}
	}
}
