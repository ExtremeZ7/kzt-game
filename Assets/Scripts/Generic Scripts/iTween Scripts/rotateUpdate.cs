using UnityEngine;
using System.Collections;

public class rotateUpdate : MonoBehaviour {

	public string optionalTransformTag;
	public Transform targetTransform;
	public bool isLocal;
	public float time;

	[Space(10)]
	public float rotationOffset;

	void Start (){
		iTween.Init(gameObject);

		if(optionalTransformTag != "")
			targetTransform = GameObject.FindGameObjectWithTag(optionalTransformTag).transform;
	}

	void Update () {
		iTween.RotateUpdate(gameObject, iTween.Hash(
			"rotation", targetTransform,
			"islocal", isLocal,
			"time", time
		));
	}
}
