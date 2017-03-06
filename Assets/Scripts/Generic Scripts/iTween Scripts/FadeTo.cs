using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTo : MonoBehaviour {

	public float alpha;
	//public float amount;
	public bool includeChildren;
	public float time;
	public float delay;
	public iTween.EaseType easeType;
	public iTween.LoopType loopType;
	public bool ignoreTimeScale;

	void Awake(){
		iTween.Init(gameObject);
	}

	void Start () {
		iTween.FadeTo(gameObject, iTween.Hash(
			"alpha",alpha,
			//"amount",amount,
			"includechildren", includeChildren,
			"time" , time,
			"delay",delay,
			"easetype",easeType,
			"looptype",loopType,
			"ignoretimescale", ignoreTimeScale
		));
	}
}
