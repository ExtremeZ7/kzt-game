using UnityEngine;
using Controllers;

public class ShowJewelCountAtTransform : MonoBehaviour
{

    public GUIStyle textStyle;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void OnGUI()
    {
        if (GameController.Instance.items.crystalsShownInGUI < GameController.Instance.items.totalCrystalsInLevel)
        {
            float xScale = Screen.width / GameController.Instance.standardScreenWidth;
            float yScale = Screen.height / GameController.Instance.standardScreenHeight;
            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(xScale, yScale, 1));

            GUIStyle textStroke = new GUIStyle(textStyle);
            textStroke.normal.textColor = Color.black;
            textStroke.fontStyle = FontStyle.Bold;

            string jewelCountString = "" + GameController.Instance.items.crystalsShownInGUI + "/" + GameController.Instance.items.totalCrystalsInLevel;

            Vector3 screenPosition = mainCamera.WorldToScreenPoint(transform.position);

            GUI.Label(new Rect((screenPosition.x / xScale) - 50, GameController.Instance.standardScreenHeight - (screenPosition.y / yScale) - 50, 100, 100), jewelCountString, textStroke);
            GUI.Label(new Rect((screenPosition.x / xScale) - 50, GameController.Instance.standardScreenHeight - (screenPosition.y / yScale) - 50, 100, 100), jewelCountString, textStyle);
        }
    }
}
