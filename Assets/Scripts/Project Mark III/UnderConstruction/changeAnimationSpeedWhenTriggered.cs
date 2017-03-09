using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class changeAnimationSpeedWhenTriggered : MonoBehaviour
    {

        public enum TriggerMode
        {
            Disabled,
            WhileSwitched,
            OnRecentSwitch,
            WhileSwitchedOff}

        ;

        public float newSpeed = 1f;

        [Space(10)]
        public Animator animator;
        public TriggerSwitch binarySwitch;

        [Space(10)]
        public TriggerMode onTriggerMode = TriggerMode.OnRecentSwitch;
        public bool reverseTrigger;

        void Start()
        {
            if (animator == null)
                animator = GetComponent<Animator>();
            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (animator.isActiveAndEnabled && binarySwitch.isActiveAndEnabled)
            {
                switch (onTriggerMode)
                {
                    case TriggerMode.WhileSwitched:
                        if ((!reverseTrigger && binarySwitch.IsActivated || (reverseTrigger && !binarySwitch.IsActivated)))
                            Switch();
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if ((!reverseTrigger && binarySwitch.ActivatedOnCurrentFrame) || (reverseTrigger && binarySwitch.ActivatedOnCurrentFrame))
                            Switch();
                        break;
                }
            }
        }

        void Switch()
        {
            animator.speed = newSpeed;
        }
    }
}