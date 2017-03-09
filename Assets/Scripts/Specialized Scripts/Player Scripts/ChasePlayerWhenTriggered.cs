using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class ChasePlayerWhenTriggered : MonoBehaviour
    {

        public TriggerSwitch trigger;
        public Rigidbody2D rb2d;

        [Space(10)]
        public float acceleration;
        public float speedScale = 100f;

        private PlayerControl playerControl;
        private bool chasing;

        void Start()
        {
            if (trigger == null)
                trigger = GetComponent<TriggerSwitch>();
            if (rb2d == null)
                rb2d = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if (Help.WaitForPlayer(ref playerControl))
            {
                if (trigger.ActivatedOnCurrentFrame)
                    chasing = true;

                if (chasing)
                {
                    Vector2 targetVector = playerControl.transform.position - transform.position;
                    rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, new Vector2(targetVector.x * speedScale, targetVector.y * speedScale), acceleration * Time.deltaTime);
                }
            }
        }
    }
}