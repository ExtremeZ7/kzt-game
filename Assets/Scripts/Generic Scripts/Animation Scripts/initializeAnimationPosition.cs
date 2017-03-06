using UnityEngine;
using System.Collections;

public class initializeAnimationPosition : MonoBehaviour {

	private Animation anim;
	public float animationTime;
	public bool randomizeTime;

	[Space(10)]
	public string animationName;

	void Start () {
		anim = GetComponent<Animation>();
		anim[animationName].time = (!randomizeTime ? animationTime : Random.Range(0.0f,(float) animationTime));
	}
}
