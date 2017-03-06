using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class EraseSpriteAtStart : MonoBehaviour {

	void Start () {
		GetComponent<SpriteRenderer>().sprite = null;
	}

}
