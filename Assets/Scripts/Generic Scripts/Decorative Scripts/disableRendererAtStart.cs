using UnityEngine;
using System.Collections;

public class disableRendererAtStart : MonoBehaviour {

	void Start () {
        GetComponent<Renderer>().enabled = false;
	}
}
