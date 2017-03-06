using UnityEngine;
using System.Collections;

public class matchFloatParameterWithObjectSpeed : MonoBehaviour {

	public Transform objectToMatch;
	public Animator animator;

	[Space(10)]
	public string floatParameterName;

	[Space(10)]
	public float multiplier = 1.0f;

	private Vector3 previousLocation;

	void Awake () {
		if(objectToMatch == null)
			objectToMatch = transform;
		if(animator == null)
			animator = GetComponent<Animator>();

		previousLocation = transform.position;
	}

	void Update () {
		animator.SetFloat(floatParameterName, (Vector3.Distance(previousLocation,transform.position) * multiplier) * Time.deltaTime * 3600f);

		previousLocation = transform.position;
	}
}
