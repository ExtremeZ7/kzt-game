using UnityEngine;
using System.Collections;

public class shakePosition : MonoBehaviour {

	public string targetTag = "";

	[Space(10)]
	public Vector3 amount;
	public bool isLocal;
	public bool orientToPath;
	public float time;
	public float delay;
	public iTween.LoopType loopType;
	public iTween.EaseType easeType;


	void Start () {
		iTween.Init(gameObject);

		GameObject target = (targetTag == "" ? gameObject : GameObject.FindGameObjectWithTag(targetTag));

		iTween.ShakePosition(target, iTween.Hash(
			"amount",amount,
			"islocal",isLocal,
			"orienttopath",orientToPath,
			"time",time,
			"delay",delay,
			"looptype",loopType,
			"easetype",easeType
		));
	}

	void instantiatePrefab(GameObject prefab){
		Instantiate(prefab,transform.position,Quaternion.identity);
	}
}
