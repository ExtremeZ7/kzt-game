using UnityEngine;
using System.Collections;

public class matchTransformWithOtherObject : MonoBehaviour {

	public Transform transformToMatch;

	[Space(10)]
	public bool matchPosition = true;
	public bool matchRotation = true;
	public bool matchScale = true;

	void Update () {
		if(transformToMatch.gameObject.activeInHierarchy){
			if(matchRotation)
				transform.position = transformToMatch.position;
			if(matchRotation)
				transform.rotation = transformToMatch.rotation;
			if(matchScale)
				transform.localScale = transformToMatch.localScale;
		}
	}
}
