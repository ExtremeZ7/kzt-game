using UnityEngine;
using System.Collections;

public class DestroySelfWhenOutsideCamBounds : MonoBehaviour {

	private CameraControl cameraControl;

	void Start () {
		cameraControl = Camera.main.gameObject.GetComponent<CameraControl>();
	}

	void Update () {
		if(transform.position.x < cameraControl.topLeftBound.position.x
			|| transform.position.y > cameraControl.topLeftBound.position.y
			|| transform.position.x > cameraControl.bottomRightBound.position.x
			|| transform.position.y < cameraControl.bottomRightBound.position.y)
			Object.Destroy(gameObject);
	}
}
