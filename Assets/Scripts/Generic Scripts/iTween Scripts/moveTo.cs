using UnityEngine;
using System.Collections;

public class moveTo : MonoBehaviour {

	public enum OnComplete{DoNothing,DestroySelf};
	
	[Space(10)]
	public Transform optionalTransform;
	public string optionalTransformTag = "";
	public Vector3 position;
	public bool isLocal;
	public float time;
	public float speed;
	public float delay;
	public iTween.EaseType easeType;
	public iTween.LoopType loopType;

	[Space(10)]
	public OnComplete onComplete;

	void Awake(){
		iTween.Init(gameObject);
	}

	void Start () {
		if(optionalTransform != null)
			position = optionalTransform.position;
		else if(optionalTransformTag != "")
			position = GameObject.FindGameObjectWithTag(optionalTransformTag).transform.position;

		string movementMethod = speed > 0 ? "speed" : "time";
		float movementParam = speed > 0 ? speed : time;

		switch(onComplete){
		case OnComplete.DoNothing:	
			iTween.MoveTo(gameObject, iTween.Hash(
				"position",position,
				"islocal",isLocal,
				movementMethod ,movementParam,
				"delay",delay,
				"easetype",easeType,
				"looptype",loopType
			));
			break;
		case OnComplete.DestroySelf:
			iTween.MoveTo(gameObject, iTween.Hash(
				"position",position,
				"islocal",isLocal,
				movementMethod ,movementParam,
				"delay",delay,
				"easetype",easeType,
				"looptype",loopType,
				"oncomplete","DestroySelf",
				"oncompletetarget",gameObject
			));
			break;
		}
	}

	void DestroySelf(){
		iTween.Destroy(gameObject);
	}
}
