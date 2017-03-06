using UnityEngine;
using System.Collections;

public class FreezeToInitialPosition : MonoBehaviour {

	private Vector3 initialPosition;

	void Start () {
		initialPosition = transform.position;
	}

	void Update () {
		transform.position = initialPosition;
	}
}
