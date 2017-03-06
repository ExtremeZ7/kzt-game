using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class reverseRailDirectionWhileTriggered : MonoBehaviour
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
        public TriggerSwitch binarySwitch;

        [Space(10)]
        public bool reverse = true;

        [Space(10)]
        public TriggerMode onTriggerMode = TriggerMode.WhileSwitched;
        public TriggerMode offTriggerMode = TriggerMode.WhileSwitched;

        [Space(10)]
        public SwitchMode onRecentSwitchAction;
        public bool onlyTriggerWhenNotMoving;

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
                binarySwitch = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (binarySwitch.gameObject.GetComponent<Collider2D>().enabled && !(onlyTriggerWhenNotMoving && railToChange.IsMoving()))
            {
			
                switch (onTriggerMode)
                {
                    case TriggerMode.WhileSwitched:
                        if (binarySwitch.IsActivated)
                            Switch(true);
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if (binarySwitch.ActivatedOnCurrentFrame)
                        {
                            switch (onRecentSwitchAction)
                            {
                                case SwitchMode.Normal:
                                    Switch(true);
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
                            Switch(false);
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if (binarySwitch.ActivatedOnCurrentFrame)
                            Switch(false);
                        break;
                }

            }
        }

        void Switch(bool active)
        {
            railToChange.setRailDirection(active != reverse);
        }
    }
}