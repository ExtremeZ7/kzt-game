using UnityEngine;
using System.Collections;

public class alwaysEnableAnimatorComponent : MonoBehaviour {

	public Animator ani;

	[Space(10)]
	public bool reverse;

	void Start()	{
		if (ani == null)
			ani = GetComponent<Animator>();
	}

	void Update () {
		ani.enabled = !reverse;
	}
}
