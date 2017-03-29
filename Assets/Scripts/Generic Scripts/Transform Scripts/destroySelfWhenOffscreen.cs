using UnityEngine;
using Controllers;

public class destroySelfWhenOffscreen : MonoBehaviour
{

    Vector2 cameraSize;
    Transform cameraTransform;

    void Start()
    {
        Camera cam = Camera.main;
        cameraSize.y = 2f * cam.orthographicSize;
        cameraSize.x = cameraSize.y * cam.aspect;
        cameraTransform = FindObjectOfType<CameraController>().transform;
    }

    void Update()
    {
        Vector3 cameraPosition = cameraTransform.position;
        Rect cameraRect = new Rect(cameraPosition.x - (cameraSize.x / 2f), cameraPosition.y - (cameraSize.y / 2f), cameraSize.x, cameraSize.y);

        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);

        if (!cameraRect.Contains(currentPosition))
            Object.Destroy(gameObject);
    }
}