using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class enableGameObjectsWithTrigger : MonoBehaviour
    {

        public enum TriggerMode
        {
Disabled,
WhileSwitched,
OnRecentSwitch}

        ;

        public enum StartTrigger
        {
None,
Off,
On}

        ;

        public GameObject[] gameObjects;
        public TriggerSwitch trigger;

        [Space(10)]
        public bool reverse;

        [Space(10)]
        public TriggerMode onTriggerMode = TriggerMode.WhileSwitched;
        public TriggerMode offTriggerMode = TriggerMode.WhileSwitched;
        public StartTrigger startTrigger;



        void Start()
        {
            if (trigger == null)
                trigger = GetComponent<TriggerSwitch>();

            switch (startTrigger)
            {
                case StartTrigger.Off:
                    Switch(false);
                    break;
                case StartTrigger.On:
                    Switch(true);
                    break;
            }
        }

        void Update()
        {
            if (trigger != null)
            {
                switch (onTriggerMode)
                {
                    case TriggerMode.WhileSwitched:
                        if (trigger.IsActivated)
                            Switch(true);
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if (trigger.ActivatedOnCurrentFrame)
                            Switch(true);
                        break;
                }

                switch (offTriggerMode)
                {
                    case TriggerMode.WhileSwitched:
                        if (!trigger.IsActivated)
                            Switch(false);
                        break;
                    case TriggerMode.OnRecentSwitch:
                        if (trigger.ActivatedOnCurrentFrame)
                            Switch(false);
                        break;
                }
            }
        }

        void Switch(bool active)
        {
            for (int i = 0; i < gameObjects.Length; i++)
                gameObjects[i].SetActive(active == !reverse); 
        }
    }
}