using UnityEngine;
using System.Collections;

public class playerStartLocationContextMenu : MonoBehaviour {

	[ContextMenu ("Return To Starting Point")]
	void ReturnToStartingPoint() {
		transform.position = GameObject.FindGameObjectWithTag ("Checkpoint List").transform.GetChild (0).position;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(GameObject.FindGameObjectWithTag ("Checkpoint List").transform.GetChild (0).position,0.25f);
	}
}
