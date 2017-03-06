using UnityEngine;
using System.Collections;
using System;

public class TakeAScreenshot : MonoBehaviour {

	void Update () {
		if(Input.GetMouseButtonDown(1)){
			string filename = "C:/KZT/screenshot_" + 
				DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" +
				DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond +  ".png";
			Application.CaptureScreenshot(filename);
			Debug.Log("Saved Screenshot at " + filename + "!");
		}
	}
}
