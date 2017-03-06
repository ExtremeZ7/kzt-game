using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTo : MonoBehaviour {

	public Vector3 scale;
	public float time;
	public float speed;
	public float delay;
	public iTween.EaseType easeType;
	public iTween.LoopType loopType;

	void Awake(){
		iTween.Init(gameObject);
	}

	void Start () {
		string movementMethod = speed > 0 ? "speed" : "time";
		float movementParam = speed > 0 ? speed : time;

		iTween.ScaleTo(gameObject, iTween.Hash(
			"scale",scale,
			movementMethod ,movementParam,
			"delay",delay,
			"easetype",easeType,
			"looptype",loopType
		));
	}
}
