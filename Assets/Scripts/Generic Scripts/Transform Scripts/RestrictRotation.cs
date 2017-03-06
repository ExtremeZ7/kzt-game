using UnityEngine;
using System.Collections;

public class RestrictRotation : MonoBehaviour {

	public float maxTilt;

	void Update () {
		if(transform.rotation.eulerAngles.z > maxTilt && transform.rotation.eulerAngles.z < 360f - maxTilt){
			if(transform.rotation.eulerAngles.z < 180f)
				transform.rotation = Quaternion.Euler(new Vector3(0f,0f,maxTilt));
			else
				transform.rotation = Quaternion.Euler(new Vector3(0f,0f,360f - maxTilt));
		}
	}
}
