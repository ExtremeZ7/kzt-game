using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[Range (10f, 80f)]
	public float attackPower = 10f;

	//#if UNITY_EDITOR
	//[MyPropertyDrawers.PopuAttribute ("Hello, "Howdy", "DIE!!", "How can I help you?", "Let's dance!")]
	//#endif
	public string defaultGreeting;

	//#if UNITY_EDITOR
	//[MyPropertyDrawers.IncrementalChange(5f)]
	//#endif
	public float lookAngle = 0f;
}
