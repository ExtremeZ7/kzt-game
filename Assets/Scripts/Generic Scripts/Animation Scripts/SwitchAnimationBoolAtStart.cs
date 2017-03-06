using UnityEngine;
using System.Collections;

public class SwitchAnimationBoolAtStart : MonoBehaviour {

	public enum SwitchTo{True,False,Reverse};

	public string paramName;
	public SwitchTo switchTo;

	[Space(10)]
	public Animator animator;

	void Start () {
		if(animator == null)
			animator = GetComponent<Animator>();

		switch(switchTo){
		case SwitchTo.True:
			animator.SetBool(paramName,true);
			break;
		case SwitchTo.False:
			animator.SetBool(paramName,false);
			break;
		case SwitchTo.Reverse:
			animator.SetBool(paramName,!animator.GetBool(paramName));
			break;
		}
	}
}
