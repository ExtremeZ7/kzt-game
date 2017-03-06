using UnityEngine;
using System.Collections;

public class FollowObjectTransform : MonoBehaviour {

	public Transform target;

	[Space(10)]
	public Vector3 offset;

	void Update () {
		transform.position = new Vector3(target.position.x + offset.x,target.position.y + offset.y,target.position.z + offset.z);
	}
}
