using UnityEngine;
using Controllers;

public class DestroySelfWhenOutsideCamBounds : MonoBehaviour
{

    private CameraController cameraControl;

    void Start()
    {
        cameraControl = Camera.main.gameObject.GetComponent<CameraController>();
    }

    void Update()
    {
        if (transform.position.x < cameraControl.topLeftBound.position.x
        || transform.position.y > cameraControl.topLeftBound.position.y
        || transform.position.x > cameraControl.bottomRightBound.position.x
        || transform.position.y < cameraControl.bottomRightBound.position.y)
            Object.Destroy(gameObject);
    }
}
