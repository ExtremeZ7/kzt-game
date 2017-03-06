using UnityEngine;
using System.Collections;

public class cycleBetweenColors : MonoBehaviour {

	public float transitionSpeed;
	public float speedRandomizer;

	[Space(10)]
	public float startAngle;
	public bool randomizeAngle;

	private float angle;

	[Space(10)]
	public Color firstColor;
	public bool randomizeFirstColor;
	public Color secondColor;
	public bool randomizeSecondColor;
	private SpriteRenderer sr;

	[Space(10)]
	[Range(0f,1f)]
	public float maxBrightness = 1f;

	void OnValidate(){
		if(speedRandomizer < 0f)
			speedRandomizer = 0f;

		sr = GetComponent<SpriteRenderer>();

		float colorRatio = Mathf.Cos(startAngle * Mathf.Deg2Rad);
		sr.color = new Color(firstColor.r * ((colorRatio + 1) / 2) + secondColor.r * ((-colorRatio + 1) / 2)
			,firstColor.g * ((colorRatio + 1) / 2) + secondColor.g * ((-colorRatio + 1) / 2)
			,firstColor.b * ((colorRatio + 1) / 2) + secondColor.b * ((-colorRatio + 1) / 2)
			,firstColor.a * ((colorRatio + 1) / 2) + secondColor.a * ((-colorRatio + 1) / 2));
		angle = (angle + (transitionSpeed * Time.deltaTime * 60f)) % 360.0f;
	}

	void Start() {
		sr = GetComponent<SpriteRenderer>();

		if(!randomizeAngle)
			angle = startAngle;
		else
			angle = Random.Range(0f,360f);

		if(randomizeFirstColor)
			firstColor = new Color(Random.Range(0f,maxBrightness),Random.Range(0f,maxBrightness),Random.Range(0f,maxBrightness));

		if(randomizeSecondColor)
			secondColor = new Color(Random.Range(0f,maxBrightness),Random.Range(0f,maxBrightness),Random.Range(0f,maxBrightness));

		transitionSpeed += Random.Range(-speedRandomizer,speedRandomizer);
	}

	void Update () {
		if(firstColor != secondColor){
			float colorRatio = Mathf.Cos(angle * Mathf.Deg2Rad);
			sr.color = new Color(firstColor.r * ((colorRatio + 1) / 2) + secondColor.r * ((-colorRatio + 1) / 2)
								,firstColor.g * ((colorRatio + 1) / 2) + secondColor.g * ((-colorRatio + 1) / 2)
								,firstColor.b * ((colorRatio + 1) / 2) + secondColor.b * ((-colorRatio + 1) / 2)
				,firstColor.a * ((colorRatio + 1) / 2) + secondColor.a * ((-colorRatio + 1) / 2));
			angle = (angle + (transitionSpeed * Time.deltaTime * 60f)) % 360.0f;
		}
		else
			this.enabled = false;
	}
}
