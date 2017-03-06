using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class activateGameObjectWithTrigger : MonoBehaviour
    {

        public enum TriggerMode
        {
            Disabled,
            WhileSwitched,
            OnRecentSwitch}

        ;

        public enum StartActivation
        {
            DoNothing,
            ActivateAtStart,
            DeactivateAtStart,
            ReverseActivation}

        ;

        public GameObject objectToActivate;
        public TriggerSwitch binarySwitch;

        [Space(10)]
        public bool reverse;

        [Space(10)]
        public StartActivation startActivation;

        [Space(10)]
        public TriggerMode onTriggerMode = TriggerMode.WhileSwitched;
        public TriggerMode offTriggerMode = TriggerMode.WhileSwitched;

        void Start()
        {
            if (objectToActivate == null)
                objectToActivate = gameObject;
            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();

            switch (startActivation)
            {
                case StartActivation.ActivateAtStart:
                    objectToActivate.SetActive(true);
                    break;
                case StartActivation.DeactivateAtStart:
                    objectToActivate.SetActive(false);
                    break;
                case StartActivation.ReverseActivation:
                    objectToActivate.SetActive(!objectToActivate.activeSelf);
                    break;
            }
        }

        void Update()
        {
            if (objectToActivate != null && binarySwitch != null)
            {
                switch (onTriggerMode)
                {
                    case TriggerMode.WhileSwitched:
                        if (binarySwitch.IsActivated)
                            Switch(true);
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if (binarySwitch.ActivatedOnCurrentFrame)
                            Switch(true);
                        break;
                }

                switch (offTriggerMode)
                {
                    case TriggerMode.WhileSwitched:
                        if (!binarySwitch.IsActivated)
                            Switch(false);
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if (binarySwitch.ActivatedOnCurrentFrame)
                            Switch(false);
                        break;
                }
            }
        }

        void Switch(bool active)
        {
            objectToActivate.SetActive(active == !reverse);
        }
    }
}