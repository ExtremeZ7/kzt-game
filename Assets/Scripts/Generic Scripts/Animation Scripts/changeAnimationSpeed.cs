using UnityEngine;
using System.Collections;

public class changeAnimationSpeed : MonoBehaviour {

	[Tooltip("0 = Frozen | 1 = Full Speed")]
	public float newSpeed;
	private Animator ani;

	[Space(10)]
	public float randomIncrease;

	void Start () {
		ani = GetComponent<Animator>();
		ani.speed = newSpeed + Random.Range(0.0f,randomIncrease);
	}
}
