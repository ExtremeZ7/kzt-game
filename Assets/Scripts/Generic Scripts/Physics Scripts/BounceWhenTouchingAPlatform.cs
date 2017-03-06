using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class BounceWhenTouchingAPlatform : MonoBehaviour
    {

        public float bounceSpeed;

        [Space(10)]
        public TriggerSwitch trigger;

        void Start()
        {
            if (trigger == null)
                trigger = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (trigger.ActivatedOnCurrentFrame)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, bounceSpeed);
            }
        }
    }
}