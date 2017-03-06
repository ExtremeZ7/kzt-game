using UnityEngine;
using UnityEngine.Serialization;
using System.Collections;

public class triggerAnimationTriggerAtStart : MonoBehaviour {

	public GameObject objectWithAnimator;
	[Tooltip("If the former parameter is null, this tag parameter will be used instead.")]
	public string searchWithThisTagInstead;

	[Space(10)]
	public string triggerName;
	[FormerlySerializedAs("delay")]
	public float delayTime;

	private bool delayIsOver;

	void Start(){
		StartCoroutine(DelayExecution(delayTime));
	}

	void Update () {
		if(delayIsOver){
			if (objectWithAnimator == null)
				objectWithAnimator = GameObject.FindGameObjectWithTag(searchWithThisTagInstead);
			objectWithAnimator.GetComponent<Animator>().SetTrigger(triggerName);
			this.enabled = false;
		}
	}

	private IEnumerator DelayExecution(float delay){
		yield return new WaitForSeconds(delay);
		delayIsOver = true;
	}
}
