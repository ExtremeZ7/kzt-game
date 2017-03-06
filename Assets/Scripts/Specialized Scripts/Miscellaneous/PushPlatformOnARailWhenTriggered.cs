using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class PushPlatformOnARailWhenTriggered : MonoBehaviour
    {

        public TriggerSwitch activator;
        public travelAcrossARail rail;

        [Space(10)]
        public float maxSpeed;
        public float minSpeed;
        public float acceleration;
        public float deceleration;

        void Start()
        {
            if (rail == null)
                rail = GetComponent<travelAcrossARail>();
            if (activator == null)
                activator = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            rail.defaultSpeed = Mathf.MoveTowards(rail.defaultSpeed, 
                (rail.getDirection() > 0 && activator.IsActivated) || (rail.getDirection() < 0 && !activator.IsActivated) ? (activator.IsActivated ? maxSpeed : minSpeed) : 0,
                (activator.IsActivated ? acceleration : deceleration) * Time.deltaTime);

            if (rail.defaultSpeed == 0)
            {
                rail.setRailDirection(activator.IsActivated);
            }
        }
    }
}