using UnityEngine;
using System.Collections;

public class rotateToAndFromWithMouse : MonoBehaviour {

	public string targetTag = "";

	[Space(10)]
	public string optionalName;
	public Vector3 rotateWhenDown;
	public Vector3 rotateWhenUp;
	public bool isLocal;
	public float time;
	public float delay;
	public iTween.EaseType easeType;
	public iTween.LoopType loopType;

	private GameObject target;

	void Awake(){
		iTween.Init(gameObject);

		target = (targetTag == "" ? this.gameObject : GameObject.FindGameObjectWithTag(targetTag));

		rotateToUp();
	}

	void Update(){
		if(Input.GetMouseButtonDown(0))
			rotateToDown();

		if(Input.GetMouseButtonUp(0))
			rotateToUp();
	}

	void rotateToDown() {
		iTween.RotateTo(target, iTween.Hash(
			"name", optionalName,
			"rotation", rotateWhenDown,
			"islocal", isLocal,
			"time",time,
			"delay",delay,
			"easetype",easeType,
			"looptype",loopType));
	}

	void rotateToUp (){
		iTween.RotateTo(target, iTween.Hash(
			"name", optionalName,
			"rotation", rotateWhenUp,
			"islocal", isLocal,
			"time",time,
			"delay",delay,
			"easetype",easeType,
			"looptype",loopType));
	}
}
