using UnityEngine;
using System.Collections;

public class flipSpriteWithDirection : MonoBehaviour {

	public SpriteRenderer spriteRenderer;

	[Space(10)]
	public bool enableX;
	public bool enableY;

	[Space(10)]
	public bool flipX;
	public bool flipY;

	[Tooltip("The Speed The Object Needs To Exceed To Flip Itself | Default = 0")]
	[Space(10)]
	public float flipSpeed = 0.0f;

	private Vector3 previousLocation;

	void Start () {
		if (spriteRenderer == null)
			spriteRenderer = GetComponent<SpriteRenderer>();

		previousLocation = transform.position;
	}
		
	void Update () {
		
		if(Vector3.Distance(transform.position,previousLocation) > flipSpeed){
			if(enableX)
				spriteRenderer.flipX = (Mathf.Sign(transform.position.x - previousLocation.x) * (flipX ? -1 : 1)) > 0 ? false : true;
	
	    	if(enableY)
				spriteRenderer.flipY = (Mathf.Sign(transform.position.y - previousLocation.y) * (flipY ? -1 : 1)) > 0 ? false : true;
	   	}

        previousLocation = transform.position;
	}
}
