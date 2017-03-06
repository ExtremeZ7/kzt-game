using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class muteAudioObjectWithTrigger : MonoBehaviour
    {

        public enum TriggerMode
        {
            Disabled,
            WhileSwitched,
            OnRecentSwitch}

        ;

        public AudioSource audioSource;
        public TriggerSwitch binarySwitch;

        [Space(10)]
        public bool reverse;

        [Space(10)]
        public TriggerMode onTriggerMode = TriggerMode.WhileSwitched;
        public TriggerMode offTriggerMode = TriggerMode.WhileSwitched;

        void Awake()
        {
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (audioSource != null && binarySwitch != null)
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
            audioSource.mute = active != reverse;
        }
    }
}