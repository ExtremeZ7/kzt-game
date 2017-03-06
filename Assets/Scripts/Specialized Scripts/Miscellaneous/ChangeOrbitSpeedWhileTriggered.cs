using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class ChangeOrbitSpeedWhileTriggered : MonoBehaviour
    {

        public TriggerSwitch activator;
        public SendChildrenOnOrbit orbiter;

        [Space(10)]
        public float defaultRPM;
        public float triggeredRPM;

        void Start()
        {
            if (activator == null)
                activator = GetComponent<TriggerSwitch>();
            if (orbiter == null)
                orbiter = GetComponent<SendChildrenOnOrbit>();
        }

        void Update()
        {
            orbiter.RPM = activator.IsActivated ? triggeredRPM : defaultRPM;
        }
    }
}