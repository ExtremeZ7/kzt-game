using UnityEngine;
using System.Collections;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class activateRotatorsWithTrigger : MonoBehaviour
    {

        public enum TriggerMode
        {
Disabled,
WhileSwitched,
OnRecentSwitch}

        ;

        public rotateObject[] rotators;
        public TriggerSwitch trigger;

        [Space(10)]
        public bool reverse;

        [Space(10)]
        public TriggerMode onTriggerMode = TriggerMode.WhileSwitched;
        public TriggerMode offTriggerMode = TriggerMode.WhileSwitched;

        void Awake()
        {
            if (trigger == null)
                trigger = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (trigger != null)
            {
                switch (onTriggerMode)
                {
                    case TriggerMode.WhileSwitched:
                        if (trigger.IsActivated)
                            Switch(true);
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if (trigger.ActivatedOnCurrentFrame)
                            Switch(true);
                        break;
                }

                switch (offTriggerMode)
                {
                    case TriggerMode.WhileSwitched:
                        if (!trigger.IsActivated)
                            Switch(false);
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if (trigger.ActivatedOnCurrentFrame)
                            Switch(false);
                        break;
                }
            }
        }

        void Switch(bool active)
        {
            for (int i = 0; i < rotators.Length; i++)
                rotators[i].enabled = (active == !reverse);
        }
    }
}