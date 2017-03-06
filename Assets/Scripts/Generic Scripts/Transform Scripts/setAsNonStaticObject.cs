using UnityEngine;
using System.Collections;

public class setAsNonStaticObject : MonoBehaviour {

	private Transform respawnKeeper;

	void Update () {
		if(transform.parent == null && respawnKeeper == null){
			respawnKeeper = GameObject.FindGameObjectWithTag("State Keeper").transform;
			transform.parent = respawnKeeper;
		}
	}
}
