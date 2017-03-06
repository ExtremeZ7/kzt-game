using UnityEngine;
using AssemblyCSharp;

public class ClonePrefabsWithTimer : MonoBehaviour
{

    public GameObject[] prefabsToClone;
    public Transform cloneLocation;

    [Space(10)]
    public TimerSwitch binarySwitch;

    [Space(10)]
    public bool cloneWhenSwitchedOn = true;
    public bool cloneWhenSwitchedOff;

    [Space(10)]
    public bool flipPrefabScaleWithSelf = true;
    public bool flipXScale;
    public bool flipYScale;
    public bool matchParent;

    void Awake()
    {
        //iTween.Init(gameObject);
    }

    void Start()
    {
        if (cloneLocation == null)
            cloneLocation = transform;
        if (binarySwitch == null)
            binarySwitch = GetComponent<TimerSwitch>();
    }

    void Update()
    {
        if (binarySwitch.isActiveAndEnabled && cloneLocation.gameObject.activeInHierarchy && (binarySwitch.ActivatedOnCurrentFrame && cloneWhenSwitchedOn || binarySwitch.DeactivatedOnCurrentFrame && cloneWhenSwitchedOff))
        {
            for (int i = 0; i < prefabsToClone.Length; i++)
            {	
                /*GameObject newClone = iTween.Instantiate(prefabsToClone[i]);
				newClone.transform.position = cloneLocation.position;
				newClone.transform.rotation = Quaternion.identity;*/
                GameObject newClone = Instantiate(prefabsToClone[i], cloneLocation.position, Quaternion.identity) as GameObject;
                newClone.name = prefabsToClone[i].name;

                if (flipPrefabScaleWithSelf)
                {
                    Vector2 scale = newClone.transform.localScale;
                    scale.x *= Mathf.Sign(transform.localScale.x) * (flipXScale ? -1 : 1);
                    scale.y *= Mathf.Sign(transform.localScale.y) * (flipYScale ? -1 : 1);
                    newClone.transform.localScale = scale;
                }

                if (matchParent)
                    newClone.transform.parent = transform.parent;
            }
        }
    }
}
