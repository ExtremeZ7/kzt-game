using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTo : MonoBehaviour {
	
	public Color color;
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
		iTween.ScaleTo(gameObject, iTween.Hash(
			"r",color.r,
			"g",color.g,
			"b",color.b,
			"a",color.a,
			"includechildren", includeChildren,
			"time" , time,
			"delay",delay,
			"easetype",easeType,
			"looptype",loopType,
			"ignoretimescale", ignoreTimeScale
		));
	}
}
