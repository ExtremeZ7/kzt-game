using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class ActivateTurtleTurbo : MonoBehaviour
    {

        private TriggerSwitch trigger;

        void Start()
        {
            trigger = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (trigger.ActivatedOnCurrentFrame)
            {
                transform.parent.gameObject.GetComponent<Rigidbody2D>().WakeUp();
                transform.parent.gameObject.GetComponent<AccelerateForward>().enabled = true;
                transform.parent.gameObject.GetComponent<flipObjectTowardsOtherObjectWithTag>().enabled = false;
                transform.parent.gameObject.GetComponent<FreezeToInitialPosition>().enabled = false;
            }
        }
    }
}