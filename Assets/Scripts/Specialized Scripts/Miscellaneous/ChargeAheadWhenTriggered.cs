using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class ChargeAheadWhenTriggered : MonoBehaviour
    {

        public enum TriggerMode
        {
Disabled,
WhileSwitched,
OnRecentSwitch}

        ;

        public TriggerSwitch trigger;

        public MoveHorizontallyBetweenTwoPoints moveHorizontallyBetweenTwoPoints;
        public SendChildrenOnOrbit orbiter;
        public Rigidbody2D rb2d;
        public Animator animator;
        public string triggerTag;

        [Space(10)]
        public TriggerMode triggerMode = TriggerMode.OnRecentSwitch;

        [Space(10)]
        public float initialChargeDelay;
        public float chargeVelocity;
        public float decelerationSpeed;
        public float postChargeDelay;

        private float initialChargeTimer;
        private bool charging = false;
        private float postChargeTimer;

        private bool justEntered = true;

        void Start()
        {
            if (trigger == null)
                trigger = GetComponent<TriggerSwitch>();
            if (moveHorizontallyBetweenTwoPoints == null)
                moveHorizontallyBetweenTwoPoints = GetComponent<MoveHorizontallyBetweenTwoPoints>();
            if (rb2d == null)
                rb2d = GetComponent<Rigidbody2D>();
            if (animator == null)
                animator = GetComponent<Animator>();
        }

        void Update()
        {
            if (charging && Helper.UseAsTimer(ref initialChargeTimer))
            {
                if (justEntered)
                {
                    rb2d.velocity = new Vector2(chargeVelocity * Mathf.Sign(rb2d.transform.localScale.x), rb2d.velocity.y);
                    justEntered = false;
                }
                rb2d.velocity = Vector2.MoveTowards(rb2d.velocity, Vector2.zero, decelerationSpeed * Time.deltaTime);
                if (rb2d.velocity.x == 0f && Helper.UseAsTimer(ref postChargeTimer))
                {
                    moveHorizontallyBetweenTwoPoints.enabled = true;
                    charging = false;
                    justEntered = true;

                    if (animator != null)
                        animator.SetBool(triggerTag, false);
                    if (orbiter != null)
                        orbiter.enabled = true;
                }
            }

            switch (triggerMode)
            {
                case TriggerMode.WhileSwitched:
                    if (trigger.IsActivated)
                    {
                        Charge();
                    }
                    break;
                case TriggerMode.OnRecentSwitch:
                    if (trigger.ActivatedOnCurrentFrame)
                    {
                        Charge();
                    }
                    break;
            }
        }

        void Charge()
        {
            if (!charging)
            {
                initialChargeTimer = initialChargeDelay;
                moveHorizontallyBetweenTwoPoints.enabled = false;
                postChargeTimer = postChargeDelay;
                charging = true;

                if (animator != null)
                    animator.SetBool(triggerTag, true);
                if (orbiter != null)
                    orbiter.enabled = false;
            }
        }
    }
}