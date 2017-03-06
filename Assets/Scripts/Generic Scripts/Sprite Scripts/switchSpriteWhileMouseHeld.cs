using UnityEngine;
using System.Collections;

public class switchSpriteWhileMouseHeld : MonoBehaviour {

	public GameObject objectWithSpriteRenderer;
	public Sprite newSprite;

	private SpriteRenderer sr;

	void Start () {
		if (objectWithSpriteRenderer == null)
			objectWithSpriteRenderer = this.gameObject;
		sr = objectWithSpriteRenderer.GetComponent<SpriteRenderer>();
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			sr.sprite = newSprite;
		}
		if(Input.GetMouseButton(1)){
			
		}
	}
}
