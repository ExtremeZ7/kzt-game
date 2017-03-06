using UnityEngine;
using System.Collections;

public class matchObjectActiveStateWithOtherObject : MonoBehaviour {

	public GameObject objectToCheck;
	public GameObject objectToChange;

	[Space(10)]
	public bool reverse;

	[Space(10)]
	public bool changeWhenActivated = true;
	public bool changeWhenDeactivated = true;

	void Start(){
		if (objectToCheck == null)
			objectToCheck = this.gameObject;
		if (objectToChange == null)
			objectToChange = this.gameObject;
	}

	void Update () {
		if(objectToCheck.activeInHierarchy && changeWhenActivated)
			objectToChange.SetActive(!reverse);
		else if(!objectToCheck.activeInHierarchy && changeWhenDeactivated)
			objectToChange.SetActive(reverse);
	}
}
