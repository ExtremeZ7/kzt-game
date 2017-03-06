using UnityEngine;
using System.Collections;

public class scaleInAndOutToWithMouse : MonoBehaviour {

	public string targetTag = "";

	[Space(10)]
	public string optionalName;
	public Vector3 scaleWhenDown;
	public Vector3 scaleWhenUp;
	public float time;
	public float delay;
	public iTween.EaseType easeType;
	public iTween.LoopType loopType;

	private GameObject target;

	void Awake(){
		iTween.Init(gameObject);

		target = (targetTag == "" ? this.gameObject : GameObject.FindGameObjectWithTag(targetTag));

		scaleToUp();
	}

	void Update(){
		if(Input.GetMouseButtonDown(0))
			scaleToDown();

		if(Input.GetMouseButtonUp(0))
			scaleToUp();
	}

	void scaleToDown (){
		iTween.ScaleTo(target, iTween.Hash(
			"name", optionalName,
			"scale", scaleWhenDown,
			"time",time,
			"delay",delay,
			"easetype",easeType,
			"looptype",loopType));
	}

	void scaleToUp (){
		iTween.ScaleTo(target, iTween.Hash(
			"name", optionalName,
			"scale", scaleWhenUp,
			"time",time,
			"delay",delay,
			"easetype",easeType,
			"looptype",loopType));
	}
}
