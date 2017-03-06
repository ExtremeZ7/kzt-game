using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class bounceSteelCrateWhenTriggered : MonoBehaviour
    {

        public TriggerSwitch trigger;

        [Space(10)]
        public float bounceForce;

        [Space(10)]
        public GameObject audioToPlay;

        void Start()
        {
            if (trigger == null)
                trigger = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (trigger.ActivatedOnCurrentFrame)
            {

                if (audioToPlay != null)
                    Instantiate(audioToPlay, transform.position, Quaternion.identity);

                Rigidbody2D collidedRigidbody = trigger.TriggeringObject.transform.parent.gameObject.GetComponent<Rigidbody2D>();
                collidedRigidbody.velocity = new Vector2(collidedRigidbody.velocity.x, bounceForce);
            }
        }
    }
}