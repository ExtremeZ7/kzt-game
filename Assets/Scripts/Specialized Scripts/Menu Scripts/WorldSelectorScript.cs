using UnityEngine;
using AssemblyCSharp;

public class WorldSelectorScript : MonoBehaviour
{

    private LevelSelectionScript levelSelectionScript;
    private int lastSelectedWorldIndex = 1;
    private string worldName;
    private string worldDiff;

    public Transform worldLocations;

    [Space(10)]
    public float speed;
    public iTween.EaseType movementEaseType;

    [Space(10)]
    public GUIStyle textStyle;
    public GUIStyle strokeStyle;

    void Start()
    {
        iTween.Init(gameObject);

        levelSelectionScript = FindObjectOfType<LevelSelectionScript>();

        transform.position = worldLocations.GetChild(GameControl.control.progress.currentWorld - 1).position;
        lastSelectedWorldIndex = GameControl.control.progress.currentWorld;

        for (int i = 1; i < 5; i++)
        {
            worldLocations.GetChild(i).GetChild(0).gameObject.SetActive(
                GameControl.control.restrictLocked && !GameControl.control.progress.levelsUnlocked[i, 0]);
        }
    }


    void Update()
    {
        if (!GameControl.control.paused)
        {
            int worldSelectIndex = levelSelectionScript.getWorldSelectIndex();

            worldName = Help.GetWorldName(levelSelectionScript.getWorldSelectIndex());
            worldDiff = Help.GetWorldDifficulty(levelSelectionScript.getWorldSelectIndex());

            if (lastSelectedWorldIndex != worldSelectIndex)
            {
                iTween.MoveTo(gameObject, iTween.Hash(
                        "position", worldLocations.GetChild(worldSelectIndex - 1),
                        "speed", speed,
                        "easetype", movementEaseType)
                );
            }

            lastSelectedWorldIndex = worldSelectIndex;
        }
    }

    void OnGUI()
    {
        if (levelSelectionScript.getSelectionState() == 0)
        {
            float xScale = Screen.width / GameControl.control.standardScreenWidth;
            float yScale = Screen.height / GameControl.control.standardScreenHeight;
            GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(xScale, yScale, 1));

            float rectWidth = 200;
            float rectHeight = 100;

            float screenWidth = GameControl.control.standardScreenWidth;
            float screenHeight = GameControl.control.standardScreenHeight;

            Rect levelNameRect = new Rect(screenWidth / 2 - rectWidth / 2, screenHeight / 2 - rectHeight / 2 + 200, rectWidth, rectHeight);
            Rect levelDiffRect = new Rect(screenWidth / 2 - rectWidth / 2, screenHeight / 2 - rectHeight / 2 + 262, rectWidth, rectHeight);

            GUI.Label(levelNameRect, worldName, strokeStyle);
            GUI.Label(levelNameRect, worldName, textStyle);

            GUI.Label(levelDiffRect, "DIFFICULTY: " + worldDiff.ToUpper(), strokeStyle);
            GUI.Label(levelDiffRect, "DIFFICULTY: " + worldDiff.ToUpper(), textStyle);
        }
    }
}
