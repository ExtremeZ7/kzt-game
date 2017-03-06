using UnityEngine;
using System.Collections;

public class setParentAtStart : MonoBehaviour {

	public GameObject newParent;

	void Start () {
		transform.parent = newParent.transform;
	}
}
