using UnityEngine;
using System.Collections;

public class randomlyOffsetPositionAtStart : MonoBehaviour {

	public float xRange;
	public float yRange;
	
	void Start () {
		transform.position = new Vector3(transform.position.x + Random.Range(0.0f,xRange)
								,transform.position.y + Random.Range(0.0f,xRange),0);
	}
}
