using UnityEngine;
using System.Collections;

public class enableSpriteRendererWhileMouseHeld : MonoBehaviour {
	
	public GameObject objectWithSpriteRenderer;
	private SpriteRenderer sr;
	[Space(10)]
	public bool reverse = false;
	public bool reverseWhenNotHeld = false;

	void Start () {
		if(objectWithSpriteRenderer == null){
			objectWithSpriteRenderer = this.gameObject;
		}
		sr = objectWithSpriteRenderer.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			sr.enabled = !reverse;
		}
		if (reverseWhenNotHeld && Input.GetMouseButtonUp (0)) {
			sr.enabled = reverse;
		}
	}
}
