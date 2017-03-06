using UnityEngine;
using System.Collections;

public class clonePrefab : MonoBehaviour {

	public GameObject prefab;

	[Space(10)]
	public bool setAsChild;

	void Awake () {
		GameObject newClone = Instantiate(prefab,transform.position,Quaternion.identity) as GameObject;

		if(setAsChild)
			newClone.transform.parent = transform;
	}
}
