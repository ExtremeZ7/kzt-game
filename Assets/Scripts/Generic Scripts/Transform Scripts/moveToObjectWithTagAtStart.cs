using UnityEngine;
using System.Collections;

public class moveToObjectWithTagAtStart : MonoBehaviour {

	public float moveSpeed;
	public float startDelay = 0.0f;

	[Space(10)]
	public string targetTag;
	private Transform targetTransform;

	void Start () {
		GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
		targetTransform = targetObject.transform;
	}

	void Update () {
		if((startDelay = Mathf.MoveTowards(startDelay,0.0f,Time.deltaTime)) == 0.0f)
			transform.position = Vector3.MoveTowards(transform.position,targetTransform.position,moveSpeed * Time.deltaTime);
	}
}
