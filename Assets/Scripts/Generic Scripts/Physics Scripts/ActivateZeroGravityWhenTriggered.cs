using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class ActivateZeroGravityWhenTriggered : MonoBehaviour
    {

        public TriggerSwitch trigger;
        private PlayerControl playerControl;

        [Space(10)]
        public bool reverse;

        void Start()
        {
            if (trigger == null)
                trigger = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (Help.WaitForPlayer(ref playerControl))
            {
                if (trigger.ActivatedOnCurrentFrame)
                {
                    playerControl.changeMovementState(!reverse ? PlayerControl.MovementState.NoGravity : PlayerControl.MovementState.Normal);
                }
            }
        }
    }
}