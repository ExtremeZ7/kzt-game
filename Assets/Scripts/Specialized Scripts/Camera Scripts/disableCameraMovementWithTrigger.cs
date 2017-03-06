using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class disableCameraMovementWithTrigger : MonoBehaviour
    {

        public TriggerSwitch trigger;

        void Start()
        {
            if (trigger == null)
                trigger = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (trigger.ActivatedOnCurrentFrame)
                FindObjectOfType<CameraControl>().enabled = false;
        }
    }
}