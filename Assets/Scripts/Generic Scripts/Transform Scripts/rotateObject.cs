#pragma warning disable 0168
namespace System.Collections
{
    using UnityEngine;
    using Controllers;

    public class rotateObject : MonoBehaviour
    {

        public enum Rotation
        {
            CounterClockwise,
            Clockwise}

        ;

        public Transform objectToRotate;

        [Space(10)]
        public float RPM;
        [Tooltip("in seconds")]
        [SerializeField]
        private float timePerRotation;

        [Space(10)]
        public bool randomizeRPM = false;
        public float minRPM;
        public float maxRPM;

        [Space(10)]
        public Rotation rotation;

        [Space(10)]
        public bool randomizeAngle = false;
        public float startAngle;
        private float angle;

        [Header("Performance Tweaks")]
        public bool stopWhenNotVisible;
        private bool isVisible;

        void OnValidate()
        {
            if (randomizeAngle)
                startAngle = Random.Range(0f, float.MaxValue) % 360f;

            if (objectToRotate == null)
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, startAngle));
            else
                objectToRotate.rotation = Quaternion.Euler(new Vector3(0, 0, startAngle));

            if (RPM < 0f)
                RPM = 0f;

            if (randomizeRPM)
                RPM = 0f;
            else
            {
                minRPM = 0f;
                maxRPM = 0f;
            }

            timePerRotation = 1f / (RPM / 60f);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.05f);
        }

        void Start()
        {
            if (objectToRotate == null)
                objectToRotate = transform;

            if (randomizeAngle)
                startAngle = Random.Range(0f, float.MaxValue) % 360f;

            if (randomizeRPM)
            {
                RPM = Random.Range(minRPM, maxRPM);
            }

            angle = startAngle;
            timePerRotation = timePerRotation + 0f;
        }

        void Update()
        {
            if (!(stopWhenNotVisible && !isVisible) && !GameController.Instance.paused)
            {
                switch (rotation)
                {
                    case Rotation.Clockwise:
                        angle = (angle - (RPM * 0.1f * Time.deltaTime * 60f));
                        if (angle < 0f)
                            angle += 360f;
                        break;
                    case Rotation.CounterClockwise:
                        angle = ((angle + (RPM * 0.1f * Time.deltaTime * 60f)) % 360.0f);
                        break;
                }

                if (Mathf.Abs(angle) > 360.0f)
                    angle -= 360.0f * Mathf.Sign(angle);
                objectToRotate.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }

        void OnBecameInvisible()
        {
            isVisible = false;
        }

        void OnBecameVisible()
        {
            isVisible = true;
        }
    }
}
