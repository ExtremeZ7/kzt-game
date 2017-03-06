using UnityEngine;
using System.Collections;

public class deactivateObjectWhenOtherObjectIsDestroyed : MonoBehaviour {

	public GameObject objectToCheck;
	public GameObject objectToChange;

	[Space(10)]
	public bool selfDestructInstead;

	void  Start(){
		if(objectToChange == null)
			objectToChange = gameObject;
	}

	void Update () {
		if(objectToCheck == null){
			if (!selfDestructInstead)
				objectToChange.SetActive(false);
			else
				Object.Destroy(objectToChange);
		}
	}
}
