using UnityEngine;
using System.Collections;

public class followMousePosition : MonoBehaviour {

	public float xOffset;
	public float yOffset;

	private Vector3 mousePosition;
	private Transform cameraPosition;
	private Camera cam;

	private float height;
	private float width;

	void Start () {
		cam = FindObjectOfType<Camera>();

		cam = Camera.main;
		cameraPosition = cam.transform;
		height = 2f * cam.orthographicSize;
		width = height * cam.aspect;
		mousePosition = transform.position;
	}

	void Update () {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		mousePosition.z = transform.position.z;
		if (mousePosition.x > cameraPosition.position.x - width / 2 && mousePosition.x < cameraPosition.position.x + width / 2)
		{
			if (mousePosition.y > cameraPosition.position.y - height / 2 && mousePosition.y < cameraPosition.position.y + height / 2)
			{
				transform.position = new Vector3(mousePosition.x + xOffset,mousePosition.y + yOffset,transform.position.z);
			}
		}
	}
}
