using UnityEngine;
using System.Collections;

public class changePositionBasedOnCamera : MonoBehaviour {

	private Camera cam;

	[Range(0f,1f)]
	public float offsetFactor = 0.7f;

	[ContextMenu ("Position To Camera")]
	void PositionToCamera(){
		transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,0f);
	}

	void OnValidate(){
		PositionToCamera();
	}

	void Awake() {
		PositionToCamera();
		cam = Camera.main;
	}

	void Update () {
		transform.position = new Vector3(cam.transform.position.x - (offsetFactor * cam.transform.position.x),cam.transform.position.y - (offsetFactor * cam.transform.position.y),10f);
	}
}
