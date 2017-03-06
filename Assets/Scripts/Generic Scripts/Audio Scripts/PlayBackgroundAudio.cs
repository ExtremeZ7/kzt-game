using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class PlayBackgroundAudio : MonoBehaviour {

	[SerializeField]
	private AudioSource audioSource;

	[Space(10)]
	public AudioClip intro;
	public AudioClip loop;

	private bool introFinished;

	void OnValidate(){
		audioSource = GetComponent<AudioSource>();
	}

	void Start () {
		OnValidate();
	}

	void Update () {
		if(!audioSource.isPlaying){
			if(intro != null && !introFinished){
				audioSource.clip = intro;
				audioSource.loop = false;

				audioSource.Play();
				introFinished = true;
			}
			else if(loop != null){
				audioSource.clip = loop;
				audioSource.loop = true;

				audioSource.Play();
			}
		}
	}
}
