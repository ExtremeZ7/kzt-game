using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class activateARailIfTriggered : MonoBehaviour
    {

        public enum TriggerMode
        {
Disabled,
WhileSwitched,
OnRecentSwitch}

        ;

        public travelAcrossARail railToActivate;
        public TriggerSwitch binarySwitch;

        [Space(10)]
        public bool reverse = false;

        [Space(10)]
        public TriggerMode onTriggerMode = TriggerMode.OnRecentSwitch;
        public TriggerMode offTriggerMode = TriggerMode.Disabled;


        void Start()
        {
            if (railToActivate == null)
                railToActivate = GetComponent<travelAcrossARail>();
            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (railToActivate != null && binarySwitch != null)
            {
                switch (onTriggerMode)
                {
                    case TriggerMode.WhileSwitched:
                        if (binarySwitch.IsActivated)
                            Switch(true);
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if (binarySwitch.ActivatedOnCurrentFrame)
                            Switch(true);
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
            railToActivate.enabled = (active != reverse);
        }
    }
}