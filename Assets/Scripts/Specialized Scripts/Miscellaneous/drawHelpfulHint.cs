using UnityEngine;
using AssemblyCSharp;

public class drawHelpfulHint : MonoBehaviour
{

    [HideInInspector]
    public string stringToDraw = "";
    private string stringInGUI = "";

    public float textSpeed;
    private float textDelay = 0.0f;

    [Space(10)]
    public float boxScaleSpeed;
    public float destroyDelay;
    private bool closing = false;
    private float actualRectWidth = 0.0f;

    [Space(10)]
    public float distanceFromCeiling;
    public float boxHeight;
    public float sideMargin;

    [Space(30)]
    public GUIStyle boxStyle;
    public GUIStyle textStyle;
    public GUIStyle textStyle2;

    private GameObject textPop;

    private int textCombo;

    void Start()
    {
        textPop = Resources.Load("Prefabs/Audio Source [text-pop]", typeof(GameObject)) as GameObject;
    }

    void Update()
    {
        float screenWidth = GameControl.control.standardScreenWidth;

        actualRectWidth = Mathf.MoveTowards(actualRectWidth,
            (closing ? 0.0f : screenWidth - (sideMargin * 2.0f)),
            boxScaleSpeed);

        if (actualRectWidth == screenWidth - (sideMargin * 2.0f))
        {
            if (stringToDraw.Length > 0)
            {
                if (Help.UseAsTimer(ref textDelay))
                {
                    stringInGUI += stringToDraw.Substring(0, 1);

                    if (!stringToDraw.Substring(0, 1).Equals(" "))
                    {
                        Instantiate(textPop, transform.position, Quaternion.identity);
                        textCombo++;
                    }
                    else
                    {
                        textCombo = 0;
                    }

                    stringToDraw = stringToDraw.Substring(1);
                    textDelay = 1.0f / textSpeed;
                }
            }
            else
            {
                if (stringInGUI.Length > 0)
                {
                    if (Help.UseAsTimer(ref destroyDelay))
                    {
                        stringInGUI = "";
                        closing = true;
                    }
                }
            }
        }

        if (closing && actualRectWidth == 0.0f)
        {
            Object.Destroy(gameObject);
        }
    }

    void OnGUI()
    {
        float xScale = Screen.width / GameControl.control.standardScreenWidth;
        float yScale = Screen.height / GameControl.control.standardScreenHeight;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(xScale, yScale, 1));

        float screenWidth = GameControl.control.standardScreenWidth;

        GUI.Label(new Rect(screenWidth / 2 - (actualRectWidth / 2), distanceFromCeiling, actualRectWidth, boxHeight), "", boxStyle);
        GUI.Label(new Rect(screenWidth / 2 - (actualRectWidth / 2), distanceFromCeiling, actualRectWidth, boxHeight), stringInGUI, textStyle2);
        GUI.Label(new Rect(screenWidth / 2 - (actualRectWidth / 2), distanceFromCeiling, actualRectWidth, boxHeight), stringInGUI, textStyle);
    }
}
