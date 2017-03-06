using UnityEngine;
using System.Collections;

public class changeSpriteColorTo : MonoBehaviour {

	public Color color;

	void Start () {
		GetComponent<SpriteRenderer>().color = color	;
	}
}