using UnityEngine;
using System.Collections;

public class startRespawnSequence : MonoBehaviour {

	void Awake () {
		FindObjectOfType<LevelManager>().StartRespawnSequence();
	}
}
