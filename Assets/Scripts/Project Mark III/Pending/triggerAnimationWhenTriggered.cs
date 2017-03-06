using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class triggerAnimationWhenTriggered : MonoBehaviour
    {

        public enum TriggerMode
        {
Disabled,
WhileSwitched,
OnRecentSwitch,
TriggerAtStart}

        ;

        public string triggerName;

        [Space(10)]
        public Animator animator;
        public TriggerSwitch binarySwitch;

        [Space(10)]
        public TriggerMode onTriggerMode = TriggerMode.OnRecentSwitch;

        void Start()
        {
            if (animator == null)
                animator = GetComponent<Animator>();
            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();

            switch (onTriggerMode)
            {
                case TriggerMode.TriggerAtStart:
                    animator.SetTrigger(triggerName);
                    break;
            }
        }

        void Update()
        {
            if ((animator != null && binarySwitch != null) && animator.isActiveAndEnabled && binarySwitch.isActiveAndEnabled)
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
            }
        }

        void Switch()
        {
            animator.SetTrigger(triggerName);
        }
    }
}