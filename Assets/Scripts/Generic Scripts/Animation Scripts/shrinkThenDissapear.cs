using UnityEngine;
using System.Collections;

public class shrinkThenDissapear : MonoBehaviour {

	public float time;
	public iTween.EaseType scaleEaseType;

	void Awake() {
		iTween.ScaleTo(gameObject,iTween.Hash("scale",new Vector3(0f,0f,0f),"time",time,"easeType",scaleEaseType,"onComplete","DestroySelf","onCompleteTarget",gameObject));
	}

	void DestroySelf(){
		iTween.Destroy(gameObject);
	}
}
