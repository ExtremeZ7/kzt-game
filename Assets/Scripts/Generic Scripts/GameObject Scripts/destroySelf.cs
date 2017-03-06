using UnityEngine;
using System.Collections;

public class destroySelf : MonoBehaviour {

	public float initialDelay = 0.0f;

	void Update() {
		if((initialDelay = Mathf.MoveTowards(initialDelay,0.0f,Time.deltaTime)) == 0.0f){
			Object.Destroy(this.gameObject);
		}
	}
}
