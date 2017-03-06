using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{

    public class switchAnimationWithTrigger : MonoBehaviour
    {

        public enum TriggerMode
        {
            Disabled,
            WhileSwitched,
            OnRecentSwitch}

        ;

        public string parameterName;

        [Space(10)]
        public string optionalAnimatorTransformTag;
        public Animator animator;
        public TriggerSwitch binarySwitch;

        [Space(10)]
        public bool reverse = false;

        [Space(10)]
        public TriggerMode onTriggerMode = TriggerMode.WhileSwitched;
        public TriggerMode offTriggerMode = TriggerMode.WhileSwitched;

        void Start()
        {
            if (optionalAnimatorTransformTag == "")
            {
                if (animator == null)
                {
                    animator = GetComponent<Animator>();
                }
            }

            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();
        }

        void Update()
        {

            if (animator == null)
            {
                GameObject tagSearchObject = GameObject.FindGameObjectWithTag(optionalAnimatorTransformTag);
                if (tagSearchObject != null)
                    animator = tagSearchObject.GetComponent<Animator>();
                else
                    goto Finish;
            }

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

            Finish:
            ;
        }

        void Switch(bool switched)
        {
            animator.SetBool(parameterName, switched == !reverse);
        }
    }
}