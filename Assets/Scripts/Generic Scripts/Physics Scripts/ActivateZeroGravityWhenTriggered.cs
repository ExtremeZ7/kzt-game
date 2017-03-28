using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class ActivateZeroGravityWhenTriggered : MonoBehaviour
    {

        public TriggerSwitch trigger;
        private PlayerController playerControl;

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
                    playerControl.ChangeMovementState(!reverse ? PlayerController.MovementState.NoGravity : PlayerController.MovementState.Normal);
                }
            }
        }
    }
}