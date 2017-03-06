using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{

    public class switchColliderWithTrigger : MonoBehaviour
    {

        public enum TriggerMode
        {
Disabled,
WhileSwitched,
OnRecentSwitch}

        ;

        public Collider2D collider2d;
        public TriggerSwitch binarySwitch;

        [Space(10)]
        public bool reverse = false;

        [Space(10)]
        public TriggerMode onTriggerMode = TriggerMode.WhileSwitched;
        public TriggerMode offTriggerMode = TriggerMode.WhileSwitched;

        void Start()
        {
            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();
            if (collider2d == null)
                collider2d = GetComponent<Collider2D>();
        }

        void Update()
        {
            if (binarySwitch.gameObject.GetComponent<Collider2D>().enabled)
            {
			
                switch (onTriggerMode)
                {
                    case TriggerMode.WhileSwitched:
                        if (binarySwitch.IsActivated)
                            Switch();
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if (binarySwitch.ActivatedOnCurrentFrame)
                            Switch();
                        break;
                }

                switch (offTriggerMode)
                {
                    case TriggerMode.WhileSwitched:
                        if (!binarySwitch.IsActivated)
                            Switch();
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if (binarySwitch.ActivatedOnCurrentFrame)
                            Switch();
                        break;
                }
            }
        }

        void Switch()
        {
            collider2d.enabled = (binarySwitch.IsActivated != reverse);
        }
    }
}