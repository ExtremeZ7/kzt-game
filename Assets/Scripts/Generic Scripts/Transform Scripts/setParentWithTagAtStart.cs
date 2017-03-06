using UnityEngine;
using System.Collections;

public class setParentWithTagAtStart : MonoBehaviour {

	public string parentTag;

	void Awake () {
		GameObject newParent = GameObject.FindGameObjectWithTag(parentTag);
		transform.parent = newParent.transform;
	}
}
