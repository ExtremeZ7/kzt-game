using UnityEngine;
using System.Collections;

public class pulsateScale : MonoBehaviour {

	public enum LoopType{Normal,HalfSine,NegativeHalfSine};

	public Vector2 pulseSpeed;
	public Vector2 maxScale = new Vector2(1f,1f);
	public Vector2 scaleOffset = new Vector2(0.1f,0.1f);

	[Space(10)]
	private Vector2 rotationAngle;
	public Vector2 initialRotation;

	[Space(10)]
	public LoopType loopTypeX = LoopType.HalfSine;
	public LoopType loopTypeY = LoopType.HalfSine;

	void OnDrawGizmos(){
		Gizmos.color = Color.gray;
		Gizmos.DrawSphere(transform.position,0.125f);
	}

	void Awake(){
		rotationAngle = new Vector2 (initialRotation.x, initialRotation.y);
	}

	void Update () {
		rotationAngle = new Vector2((rotationAngle.x + (pulseSpeed.x * Time.deltaTime * 60f)) % 360f, (rotationAngle.y + (pulseSpeed.y * Time.deltaTime * 60f)) % 360f);
		Vector2 scaleMultiplier = new Vector2(Mathf.Sin(rotationAngle.x * Mathf.Deg2Rad), Mathf.Sin(rotationAngle.y * Mathf.Deg2Rad));

		switch(loopTypeX){
		case LoopType.HalfSine:
			scaleMultiplier.x = Mathf.Abs(scaleMultiplier.x);
			break;
		case LoopType.NegativeHalfSine:
			scaleMultiplier.x = -Mathf.Abs(scaleMultiplier.x);
			break;
		}

		switch(loopTypeY){
		case LoopType.HalfSine:
			scaleMultiplier.y = Mathf.Abs(scaleMultiplier.y);
			break;
		case LoopType.NegativeHalfSine:
			scaleMultiplier.y = -Mathf.Abs(scaleMultiplier.y);
			break;
		}

		if (scaleMultiplier.x == 0f)
			scaleMultiplier.x = 1f;
		if (scaleMultiplier.y == 0f)
			scaleMultiplier.y = 1f;

		transform.localScale = new Vector3((maxScale.x * scaleMultiplier.x) + scaleOffset.x, (maxScale.y * scaleMultiplier.y) + scaleOffset.y, transform.localScale.z);
	}
}
