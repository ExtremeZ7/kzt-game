using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class moveAwayFromObjectWhenTriggered : MonoBehaviour
    {

        public TriggerSwitch binarySwitch;

        [Space(10)]
        public float moveDelay;
        public float moveSpeed;

        private float xSpeed = 0;
        private float ySpeed = 0;
        private bool objectHit;

        void Start()
        {
            if (binarySwitch != null)
            {
                binarySwitch = GetComponent<TriggerSwitch>();
            }
        }

        void Update()
        {
            if (binarySwitch.ActivatedOnCurrentFrame && binarySwitch.TriggeringObject != null)
            {
                float angle = Trigo.GetAngleBetweenPoints(transform.position, binarySwitch.TriggeringObject.transform.position);

                xSpeed = Trigo.PythagoreanAdjacent(angle - 180.0f, moveSpeed);
                ySpeed = Trigo.PythagoreanOpposite(angle - 180.0f, moveSpeed);
                objectHit = true;
            }

            if (objectHit && Helper.UseAsTimer(ref moveDelay))
                transform.position = new Vector3(transform.position.x + (xSpeed * Time.deltaTime), transform.position.y + (ySpeed * Time.deltaTime), 0.0f);
        }
    }
}