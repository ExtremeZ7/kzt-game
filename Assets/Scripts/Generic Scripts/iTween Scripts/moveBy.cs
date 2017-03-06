using UnityEngine;
using System.Collections;

public class moveBy : MonoBehaviour {

	public string targetTag = "";

	[Space(10)]
	public string optionalName;
	public Vector3 amount;
	public Space space;
	public float time;
	public float delay;
	public iTween.EaseType easeType;
	public iTween.LoopType loopType;

	[Space(10)]
	public float timeRandomizer;

	void OnDrawGizmos (){
		Vector3[] path = new Vector3[2];
		path[0] = transform.position;
		path[1] = new Vector3(transform.position.x + amount.x, transform.position.y + amount.y, transform.position.z + amount.z);

		iTween.DrawLineGizmos(path,Color.red);
	}

	void OnValidate (){
		if(timeRandomizer < 0f)
			timeRandomizer = 0f;
	}

	void Start () {
		iTween.Init(gameObject);

		GameObject target = (targetTag == "" ? this.gameObject : GameObject.FindGameObjectWithTag(targetTag));

		time += Random.Range(-timeRandomizer,timeRandomizer);

		iTween.MoveBy(target, iTween.Hash(
			"name", optionalName,
			"amount",amount,
			"space",space,
			"time",time,
			"delay",delay,
			"easetype",easeType,
			"looptype",loopType));
	}
}
