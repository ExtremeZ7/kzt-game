using UnityEngine;
using AssemblyCSharp;

public class activateGameObjectWithTimer : MonoBehaviour
{

    public enum TriggerMode
    {
Disabled,
WhileSwitched,
OnRecentSwitch}

    ;

    public GameObject objectToActivate;
    public TimerSwitch binarySwitch;

    [Space(10)]
    public bool reverse;
    public bool forceEnableBehaviour;

    [Space(10)]
    public TriggerMode onTriggerMode = TriggerMode.WhileSwitched;
    public TriggerMode offTriggerMode = TriggerMode.WhileSwitched;

    void Start()
    {
        if (binarySwitch == null)
        {
            binarySwitch = GetComponent<TimerSwitch>();
        }
        if (forceEnableBehaviour)
            binarySwitch.enabled = true;
    }

    void Update()
    {
        if (objectToActivate != null && binarySwitch != null)
        {
            switch (onTriggerMode)
            {
                case TriggerMode.WhileSwitched:
                    if (binarySwitch.IsActivated)
                        Switch();
                    break;
                case TriggerMode.OnRecentSwitch:
                    if (binarySwitch.ActivatedOnCurrentFrame)
                        Switch();
                    break;
            }

            switch (offTriggerMode)
            {
                case TriggerMode.WhileSwitched:
                    if (!binarySwitch.IsActivated)
                        Switch();
                    break;
                case TriggerMode.OnRecentSwitch:
                    if (binarySwitch.DeactivatedOnCurrentFrame)
                        Switch();
                    break;
            }
        }
    }

    void Switch()
    {
        objectToActivate.SetActive(binarySwitch.IsActivated == !reverse);
    }
}
