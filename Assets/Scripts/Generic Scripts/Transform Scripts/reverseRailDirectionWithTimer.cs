using UnityEngine;
using AssemblyCSharp;

public class reverseRailDirectionWithTimer : MonoBehaviour
{

    public enum TriggerMode
    {
Disabled,
WhileSwitched,
OnRecentSwitch}

    ;

    public enum SwitchMode
    {
Normal,
ReverseRailDirection}

    ;

    public travelAcrossARail railToChange;
    public TimerSwitch binarySwitch;

    [Space(10)]
    public bool reverse = true;

    [Space(10)]
    public TriggerMode onTriggerMode = TriggerMode.WhileSwitched;
    public TriggerMode offTriggerMode = TriggerMode.WhileSwitched;

    [Space(10)]
    public SwitchMode onRecentSwitchAction;

    void OnValidate()
    {
        if (onTriggerMode != TriggerMode.OnRecentSwitch)
            onRecentSwitchAction = SwitchMode.Normal;
    }

    void Awake()
    {
        if (railToChange == null)
            railToChange = GetComponent<travelAcrossARail>();
        if (binarySwitch == null)
            binarySwitch = GetComponent<TimerSwitch>();
    }

    void Update()
    {
        switch (onTriggerMode)
        {
            case TriggerMode.WhileSwitched:
                if (binarySwitch.IsActivated)
                    Switch();
                break;
            case TriggerMode.OnRecentSwitch:
                if (binarySwitch.ActivatedOnCurrentFrame)
                {
                    switch (onRecentSwitchAction)
                    {
                        case SwitchMode.Normal:
                            Switch();
                            break;
                        case SwitchMode.ReverseRailDirection:
                            railToChange.reverseRailDirection();
                            break;
                    }
                }
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

    void Switch()
    {
        railToChange.setRailDirection(binarySwitch.IsActivated != reverse);
    }
}
