using UnityEngine;
using AssemblyCSharp;

public class AnimateCheckpointAlert : MonoBehaviour
{

    [Header("Movement")]
    public float distanceBetweenLetters;
    public float delayBetweenLetters;
    public float speedTowardsPositions;
    public float delayBeforeLeave;

    [Header("Orbit")]
    public float RPM;
    public float orbitSize;

    [Header("Position")]
    public float heightOffset;

    [Space(10)]
    public GUIStyle textStyle;

    private int lettersSummoned;
    private float timer;
    private Camera mainCamera;
    private Vector2 cameraSize;

    private Vector3[] letterPositions = new Vector3[10];
    private string text = "CHECKPOINT";

    private float orbitDegree;

    private Vector3 startPosition;

    void Start()
    {
        mainCamera = Camera.main;
        cameraSize.y = 2f * mainCamera.orthographicSize;
        cameraSize.x = cameraSize.y * mainCamera.aspect;

        startPosition = transform.position;

        for (int i = 0; i < 10; i++)
        {
            letterPositions[i] = transform.position;
        }
    }

    void Update()
    {
        if (Help.UseAsTimer(ref timer) && lettersSummoned < 10)
        {
            lettersSummoned++;
            timer = delayBetweenLetters;
        }

        orbitDegree += RPM * 0.1f * Time.deltaTime * 60f * (lettersSummoned + 1);

        if (lettersSummoned >= 10 && Help.UseAsTimer(ref delayBeforeLeave))
        {
            heightOffset = Mathf.MoveTowards(heightOffset, -400f, speedTowardsPositions * Time.deltaTime * 10f);

            if (heightOffset == -400f)
                Object.Destroy(gameObject);
        }
    }

    void OnGUI()
    {
        float xScale = Screen.width / GameControl.control.standardScreenWidth;
        float yScale = Screen.height / GameControl.control.standardScreenHeight;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(xScale, yScale, 1));

        for (int i = 0; i < lettersSummoned; i++)
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(letterPositions[i]);
            Vector3 targetPosition = new Vector3((GameControl.control.standardScreenWidth / 2f) + (distanceBetweenLetters / 2f * Mathf.Sign(i - 5)) + (distanceBetweenLetters * (i - 5 < 0 ? i - 4 : i - 5)), GameControl.control.standardScreenHeight / 2f);

            float orbitDistance = orbitSize * Mathf.Sin((orbitDegree + (36 * i)) * Mathf.Deg2Rad);
            textStyle.normal.textColor = new Color(textStyle.normal.textColor.r, (Mathf.Sin((orbitDegree - 90 + (36 * i)) * Mathf.Deg2Rad) * 0.5f) + 0.5f, 0f);

            targetPosition = new Vector3(targetPosition.x * xScale, (targetPosition.y + orbitDistance + heightOffset) * yScale);
            targetPosition = mainCamera.ScreenToWorldPoint(targetPosition);

            GUIStyle actualTextStyle = new GUIStyle(textStyle);
            actualTextStyle.fontSize = (int)(Vector3.Distance(startPosition, letterPositions[i]) / Vector3.Distance(startPosition, targetPosition) * (float)textStyle.fontSize);

            GUIStyle textStroke = new GUIStyle(actualTextStyle);
            textStroke.normal.textColor = Color.black;
            textStroke.fontStyle = FontStyle.Bold;

            GUI.Label(new Rect((screenPosition.x / xScale) - 50, GameControl.control.standardScreenHeight - (screenPosition.y / yScale) - 50, 100, 100), text[i].ToString(), textStroke);
            GUI.Label(new Rect((screenPosition.x / xScale) - 50, GameControl.control.standardScreenHeight - (screenPosition.y / yScale) - 50, 100, 100), text[i].ToString(), actualTextStyle);

            letterPositions[i] = Vector3.MoveTowards(letterPositions[i], targetPosition, speedTowardsPositions * Time.deltaTime);
        }
    }
}
