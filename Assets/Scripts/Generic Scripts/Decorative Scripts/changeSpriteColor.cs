using UnityEngine;
using System.Collections;

public class changeSpriteColor : MonoBehaviour {

	public SpriteRenderer spriteRenderer;

	[Space(10)]
	public Color color;

	void Awake(){
		if(spriteRenderer == null)
			spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update () {
		spriteRenderer.color = color;
	}
}
