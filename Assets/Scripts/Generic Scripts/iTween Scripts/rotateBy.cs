using UnityEngine;
using System.Collections;

public class rotateBy : MonoBehaviour {

	public string targetTag = "";

	[Space(10)]
	public Vector3 amount;
	public bool isLocal;
	public float time;
	public float delay;
	public iTween.EaseType easeType;
	public iTween.LoopType loopType;

	[Space(10)]
	public bool randomizeStartAngle;
	public float amountRandomizer;
	public float timeRandomizer;

	void OnValidate (){
		if(amountRandomizer < 0f)
			amountRandomizer = 0f;

		if(timeRandomizer < 0f)
			timeRandomizer = 0f;

	}

	void Awake(){
		iTween.Init(gameObject);
	}


	void Start () {
		GameObject target = (targetTag == "" ? gameObject : GameObject.FindGameObjectWithTag(targetTag));

		if(randomizeStartAngle)
			transform.rotation = Quaternion.Euler(new Vector3(0f,0f,Random.Range(0f,360f)));

		amount = new Vector3(0f,0f,amount.z + Random.Range(-amountRandomizer,amountRandomizer));
		time += Random.Range(-timeRandomizer,timeRandomizer);

		iTween.RotateBy(target, iTween.Hash(
			"amount",amount,
			"islocal",isLocal,
			"time",time,
			"delay",delay,
			"easetype",easeType,
			"looptype",loopType));
	}
}
