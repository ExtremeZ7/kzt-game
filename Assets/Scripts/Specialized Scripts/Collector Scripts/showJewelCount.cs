using UnityEngine;
using UnityEngine.Audio;
using Controllers;

public class showJewelCount : MonoBehaviour
{

    public int guiX = 10;
    public int guiY = 10;
    public int guiWidth = 100;
    public int guiHeight = 20;
    private float guiYOffset;
    public float hideYPos;

    [Space(10)]
    public float incrementDelay;
    private float delayTimer = 0.0f;

    [Space(10)]
    public float guiHideSpeed;
    public float guiShowSpeed;
    public float guiHideDelay;
    [HideInInspector]
    public float hideDelay;
    private GameObject jewelCollectorSprite;
    private Transform hidePoint;
    private Transform showPoint;

    [Space(10)]
    public float scaleBurstSize;
    public float shrinkSpeed;

    [Space(30)]
    public GUIStyle textStyle;
    public GUIStyle textStyle2;

    [Space(10)]
    public GameObject jewelBeatAudioSource;

    [Space(10)]
    public AudioMixer jewelComboMixer;

    private int jewelCombo;

    void Start()
    {
        guiYOffset = hideYPos;

        jewelCollectorSprite = transform.GetChild(0).gameObject;
        hidePoint = transform.GetChild(1);
        showPoint = transform.GetChild(2);
        jewelCollectorSprite.transform.position = new Vector3(jewelCollectorSprite.transform.position.x,
            hidePoint.position.y,
            0.0f);
		
        GameController.Instance.items.totalCrystalsInLevel = GameObject.FindGameObjectsWithTag("Single Jewel Tag").Length;
        GameController.Instance.items.totalCrystalsInLevel += GameObject.FindGameObjectsWithTag("5-Jewel Tag").Length * 5;
        GameController.Instance.items.totalCrystalsInLevel += GameObject.FindGameObjectsWithTag("7-Jewel Tag").Length * 7;
        GameController.Instance.items.totalCrystalsInLevel += GameObject.FindGameObjectsWithTag("10-Jewel Tag").Length * 10;
        GameController.Instance.items.totalCrystalsInLevel += GameObject.FindGameObjectsWithTag("20-Jewel Tag").Length * 20;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
            hideDelay = guiHideDelay;

        Help.UseAsTimer(ref hideDelay);

        guiYOffset = Mathf.MoveTowards(guiYOffset,
            (hideDelay > 0 ? 0 : hideYPos),
            (hideDelay > 0 ? guiShowSpeed : guiHideSpeed) * 60 * Time.deltaTime);

        jewelCollectorSprite.transform.position = Vector3.MoveTowards(jewelCollectorSprite.transform.position,
            (hideDelay > 0 ? showPoint.position : hidePoint.position),
            (hideDelay > 0 ? guiShowSpeed : guiHideSpeed) * Time.deltaTime);

        jewelCollectorSprite.transform.localScale = Vector3.MoveTowards(jewelCollectorSprite.transform.localScale,
            new Vector3(1.0f, 1.0f, 1.0f),
            shrinkSpeed * Time.deltaTime);

        if (Help.UseAsTimer(ref delayTimer) && GameController.Instance.items.crystalsShownInGUI < GameController.Instance.items.crystalsInCollection)
        {
            Instantiate(jewelBeatAudioSource, transform.position, Quaternion.identity);
            jewelCollectorSprite.transform.localScale = new Vector3(
                jewelCollectorSprite.transform.localScale.x + scaleBurstSize,
                jewelCollectorSprite.transform.localScale.y + scaleBurstSize,
                1.0f);

            GameController.Instance.items.crystalsShownInGUI++;

            delayTimer = incrementDelay;
            hideDelay = guiHideDelay;
            jewelCombo++;
        }

        if (hideDelay == 0)
            jewelCombo = 0;

        jewelComboMixer.SetFloat("Jewel Combo Pitch", 1f + (0.01f * jewelCombo));
    }

    void OnGUI()
    {
        float xScale = Screen.width / GameController.Instance.standardScreenWidth;
        float yScale = Screen.height / GameController.Instance.standardScreenHeight;
        GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(xScale, yScale, 1));

        Vector2 contentOffset = textStyle.contentOffset;
        contentOffset.y = guiYOffset;
        textStyle.contentOffset = contentOffset;

        Vector2 contentOffset2 = textStyle2.contentOffset;
        contentOffset2.y = guiYOffset + 1;
        textStyle2.contentOffset = contentOffset2;

        GUI.Label(new Rect(guiX, guiY, guiWidth, guiHeight), GameController.Instance.items.crystalsShownInGUI + "/" + GameController.Instance.items.totalCrystalsInLevel, textStyle2);
        GUI.Label(new Rect(guiX, guiY, guiWidth, guiHeight), GameController.Instance.items.crystalsShownInGUI + "/" + GameController.Instance.items.totalCrystalsInLevel, textStyle);
    }
}
