using UnityEngine;
using Controllers;

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
                FindObjectOfType<CameraController>().enabled = false;
        }
    }
}