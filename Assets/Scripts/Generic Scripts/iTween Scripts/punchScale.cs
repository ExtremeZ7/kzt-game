using UnityEngine;
using System.Collections;

public class punchScale : MonoBehaviour {

	public enum OnComplete{DoNothing,DestroySelf};

	[Space(10)]
	public Vector3 amount;
	public float time;
	public float delay;
	public iTween.LoopType loopType;
	public OnComplete onComplete;

	[Space(10)]
	public float timeRandomizer;

	void OnValidate (){
		if(timeRandomizer < 0f)
			timeRandomizer = 0f;
	}

	void Awake(){
		iTween.Init(gameObject);
	}

	void Start () {

		time += Random.Range(-timeRandomizer,timeRandomizer);

		switch (onComplete){
		case OnComplete.DoNothing:	
			iTween.PunchScale(gameObject, iTween.Hash(
				"name", "" + gameObject.GetInstanceID(),
				"amount",amount,
				"time",time,
				"delay",delay,
				"looptype",loopType
			));
			break;
		case OnComplete.DestroySelf:
			iTween.PunchScale(gameObject, iTween.Hash(
				"name", "" + gameObject.GetInstanceID(),
				"amount",amount,
				"time",time,
				"delay",delay,
				"looptype",loopType,
				"oncomplete", "DestroySelf",
				"oncompletetarget", gameObject
			));
			break;
		}
	}

	void DestroySelf () {
		iTween.Destroy(gameObject);
	}
}
