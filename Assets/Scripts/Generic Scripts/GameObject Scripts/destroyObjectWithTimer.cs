using UnityEngine;
using AssemblyCSharp;

public class destroyObjectWithTimer : MonoBehaviour
{

    public GameObject target;

    [Space(10)]
    public TimerSwitch binarySwitch;

    void Start()
    {
        if (target == null)
            target = gameObject;
        if (binarySwitch == null)
            binarySwitch = GetComponent<TimerSwitch>();
    }

    void Update()
    {
        if (binarySwitch.ActivatedOnCurrentFrame)
            iTween.Destroy(target);
    }
}
