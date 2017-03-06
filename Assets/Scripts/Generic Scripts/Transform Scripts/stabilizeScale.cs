using UnityEngine;
using System.Collections;

public class stabilizeScale : MonoBehaviour {

	public bool manuallyValidate;

	[Space(10)]
	public Vector2 scaleToMaintain = new Vector2(1f,1f);

	void OnValidate(){
		manuallyValidate = false;

		if (transform.parent != null) {
			transform.localScale = new Vector3 (scaleToMaintain.x / transform.parent.localScale.x, scaleToMaintain.y / transform.parent.localScale.y, transform.localScale.z);
		}
	}

	void Update () {
		if (transform.parent != null) {
			transform.localScale = new Vector3 (scaleToMaintain.x / transform.parent.localScale.x, scaleToMaintain.y / transform.parent.localScale.y, transform.localScale.z);
		}
		else{
			transform.localScale = new Vector3(scaleToMaintain.x, scaleToMaintain.y, 1f);
		}
	}
}
