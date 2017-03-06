using UnityEngine;
using System.Collections;

public class flipScaleWithDirection : MonoBehaviour {

	public Transform transformToFlip;

	[Space(10)]
	public bool enableX;
	public bool enableY;
	
	public bool flipX;
	public bool flipY;

	[Tooltip("The Speed The Object Needs To Exceed To Flip Itself | Default = 0")]
	[Space(10)]
	public float flipXSpeed = 0.0f;
	public float flipYSpeed = 0.0f;

	private Vector3 previousLocation;

	void Start () {
		if(transformToFlip == null)
			transformToFlip = transform;
		previousLocation = transform.position;
	}

	void Update () {

		Vector3 theScale = transformToFlip.localScale;
		
		if(enableX && Mathf.Abs(transform.position.x - previousLocation.x) > flipXSpeed)
			theScale.x = Mathf.Abs(theScale.x) * Mathf.Sign(transform.position.x - previousLocation.x) * (flipX ? -1 : 1);
	
		if(enableY && Mathf.Abs(transform.position.y - previousLocation.y) > flipYSpeed)
			theScale.y = Mathf.Abs(theScale.y) * Mathf.Sign(transform.position.y - previousLocation.y) * (flipY ? -1 : 1);

		transformToFlip.localScale = theScale;

		previousLocation = transform.position;
	}
}
