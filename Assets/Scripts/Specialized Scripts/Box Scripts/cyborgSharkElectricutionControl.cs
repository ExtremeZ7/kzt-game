using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class cyborgSharkElectricutionControl : MonoBehaviour
    {

        public SpriteRenderer regularShark;
        public GameObject electricutedShark;

        [Space(10)]
        public TriggerSwitch sharkProjectileWeakspot;

        [Space(10)]
        public SendChildrenOnOrbit jumpControl;
        public travelAcrossARail railControl;
        public SendChildrenOnOrbit floatControl;

        [Space(10)]
        public GameObject exclamationPoint;
        public TimerSwitch timerOfExclamationPoint;
        public switchAnimatorBoolWithTimer animatorBoolOfExclamationPoint;

        [Space(10)]
        public float electricutionTime;

        private float electricutionTimer = 0f;
        private bool electrified = false;

        void Awake()
        {
            electricutedShark.SetActive(false);
        }

        void Update()
        {
            if (!electrified)
            {
                if (sharkProjectileWeakspot.ActivatedOnCurrentFrame)
                {
                    regularShark.enabled = false;
                    electricutedShark.SetActive(true);
                    electricutedShark.transform.parent.GetComponent<matchTransformWithOtherObject>().enabled = false;

                    jumpControl.enabled = false;
                    railControl.enabled = false;
                    floatControl.enabled = false;

                    timerOfExclamationPoint.enabled = false;
                    animatorBoolOfExclamationPoint.enabled = false;

                    exclamationPoint.SetActive(false);

                    electricutionTimer = electricutionTime;

                    electrified = true;
                }
            }
            else
            {
                if (Help.UseAsTimer(ref electricutionTimer))
                {
                    regularShark.enabled = true;
                    electricutedShark.SetActive(false);
                    electricutedShark.transform.parent.GetComponent<matchTransformWithOtherObject>().enabled = true;

                    jumpControl.enabled = true;
                    railControl.enabled = true;
                    floatControl.enabled = true;

                    timerOfExclamationPoint.enabled = true;
                    animatorBoolOfExclamationPoint.enabled = true;

                    exclamationPoint.SetActive(true);

                    electrified = false;
                }
            }
        }
    }
}