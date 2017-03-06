using UnityEngine;
using System.Collections;

public class wobbleLikeASeesaw : MonoBehaviour
{

	public enum Rotation
	{
CounterClockwise,
Clockwise}

	;

	public enum LoopType
	{
Normal,
HalfSine,
NegativeHalfSine}

	;

	[Tooltip ("Wobbles Per Minute")]
	public float WPM;
	public Rotation initialDirection = Rotation.Clockwise;

	[Space (10)]
	private float rotationAngle;
	[Tooltip ("In Degrees")]
	public float maxTilt;

	[Space (10)]
	public float rotationOffset;

	[Space (10)]
	public bool randomizeInitialAngle;
	public float wpmVariation;

	[Space (10)]
	public LoopType loopType;

	[Header ("Performance Tweaks")]
	public bool stopWhenNotVisible;
	private bool isVisible;

	void OnValidate ()
	{
		if (WPM < 0f)
			WPM = 0f;

		if (randomizeInitialAngle)
			rotationAngle = Random.Range (0f, float.MaxValue) % 360f;
		else
			rotationAngle = 0f;

		float angle = Mathf.Sin (rotationAngle * Mathf.Deg2Rad);

		transform.rotation = Quaternion.Euler (
			new Vector3 (0, 0, rotationOffset + (maxTilt * angle)));
	}

	void Start ()
	{
		if (randomizeInitialAngle)
			rotationAngle = Random.Range (0f, float.MaxValue) % 360f;
		else
			rotationAngle = 0f;

		WPM = WPM.Variation (wpmVariation);
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere (transform.position, 0.05f);
	}

	void Update ()
	{
		if (!(stopWhenNotVisible && !isVisible) && !GameControl.control.paused) {
			rotationAngle = (rotationAngle + (WPM * 0.1f * Time.deltaTime * 60f)) % 360.0f;

			float angle = Mathf.Sin (rotationAngle * Mathf.Deg2Rad);

			switch (loopType) {
			case LoopType.HalfSine:
				angle = Mathf.Abs (angle);
				break;
			case LoopType.NegativeHalfSine:
				angle = -Mathf.Abs (angle);
				break;
			default:
				break;
			}

			transform.rotation = Quaternion.Euler (
				new Vector3 (0, 0, rotationOffset + (maxTilt * angle)));
		}
	}

	void OnBecameInvisible ()
	{
		isVisible = false;
	}

	void OnBecameVisible ()
	{
		isVisible = true;
	}
}
