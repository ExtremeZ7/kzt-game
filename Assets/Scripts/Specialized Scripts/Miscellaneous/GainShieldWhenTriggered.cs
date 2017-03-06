using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
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

        private PlayerControl playerControl;

        void Start()
        {
            if (!trigger)
                trigger = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (Helper.WaitForPlayer(ref playerControl))
            {
                switch (onTriggerMode)
                {
                    case TriggerMode.WhileSwitched:
                        if (trigger.IsActivated)
                            playerControl.gainShield();
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if (trigger.ActivatedOnCurrentFrame)
                            playerControl.gainShield();
                        break;
                }
            }
        }
    }
}