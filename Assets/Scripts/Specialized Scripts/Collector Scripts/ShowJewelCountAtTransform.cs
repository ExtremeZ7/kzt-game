using UnityEngine;
using System.Collections;

public class ShowJewelCountAtTransform : MonoBehaviour {

	public GUIStyle textStyle;

	private Camera mainCamera;

	void Start () {
		mainCamera = Camera.main;
	}

	void OnGUI(){
		if(GameControl.control.items.crystalsShownInGUI < GameControl.control.items.totalCrystalsInLevel){
			float  xScale = Screen.width / GameControl.control.standardScreenWidth;
			float yScale = Screen.height / GameControl.control.standardScreenHeight;
			GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(xScale, yScale, 1));

			GUIStyle textStroke = new GUIStyle(textStyle);
			textStroke.normal.textColor = Color.black;
			textStroke.fontStyle = FontStyle.Bold;

			string jewelCountString = "" + GameControl.control.items.crystalsShownInGUI + "/" + GameControl.control.items.totalCrystalsInLevel;

			Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

			GUI.Label(new Rect((screenPosition.x / xScale) - 50, GameControl.control.standardScreenHeight - (screenPosition.y / yScale) - 50,100,100), jewelCountString, textStroke);
			GUI.Label(new Rect((screenPosition.x / xScale) - 50, GameControl.control.standardScreenHeight - (screenPosition.y / yScale) - 50,100,100), jewelCountString, textStyle);
		}
	}
}
