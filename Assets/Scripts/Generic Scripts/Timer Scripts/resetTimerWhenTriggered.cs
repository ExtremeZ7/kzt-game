using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class resetTimerWhenTriggered : MonoBehaviour
    {

        public TimerSwitch timerToReset;

        [Space(10)]
        public TriggerSwitch binarySwitch;

        [Space(10)]
        public bool resetWhenSwitchedOn = true;
        public bool resetWhenSwitchedOff;

        void Start()
        {
            if (timerToReset == null)
                timerToReset = GetComponent<TimerSwitch>();
            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (binarySwitch.gameObject != null && binarySwitch.isActiveAndEnabled)
            {
                if ((binarySwitch.ActivatedOnCurrentFrame && resetWhenSwitchedOn) || (binarySwitch.ActivatedOnCurrentFrame && resetWhenSwitchedOff))
                    timerToReset.ResetTimer();
            }
        }
    }
}