using UnityEngine;
using Controllers;

public class GainShieldWhenTriggered : MonoBehaviour
{

    public enum TriggerMode
    {
        Disabled,
        WhileSwitched,
        OnRecentSwitch}

    ;

    public TriggerSwitch trigger;

    [Space(10)]
    public TriggerMode onTriggerMode = TriggerMode.OnRecentSwitch;

    private PlayerController playerControl;

    void Start()
    {
        if (!trigger)
            trigger = GetComponent<TriggerSwitch>();
    }

    void Update()
    {
        if (Help.WaitForPlayer(ref playerControl))
        {
            switch (onTriggerMode)
            {
                case TriggerMode.WhileSwitched:
                    if (trigger.IsActivated)
                        playerControl.GainShield();
                    break;
                case TriggerMode.OnRecentSwitch:
                    if (trigger.ActivatedOnCurrentFrame)
                        playerControl.GainShield();
                    break;
            }
        }
    }
}