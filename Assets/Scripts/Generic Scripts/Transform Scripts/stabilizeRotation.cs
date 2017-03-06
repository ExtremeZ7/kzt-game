using UnityEngine;
using System.Collections;

public class stabilizeRotation : MonoBehaviour {

	public bool manuallyValidate;

	[Space(10)]
	[Tooltip("This script rotates the gameObject to make sure it maintains a constant angle in the scene even when its parent is rotated")]
	public float angleToMaintain = 0.0f;

	void OnValidate(){
		manuallyValidate = false;

		if(transform.parent != null)
			transform.rotation = Quaternion.Euler(new Vector3 (0.0f, 0.0f, angleToMaintain - transform.parent.rotation.z ));
	}

	void Update () {
		if(transform.parent != null)
			transform.rotation = Quaternion.Euler(new Vector3 (0.0f, 0.0f, angleToMaintain - transform.parent.rotation.z ));
	}
}
