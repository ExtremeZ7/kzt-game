using UnityEngine;
using Managers;

namespace AssemblyCSharp
{
    public class completeLevelWhenTriggered : MonoBehaviour
    {

        private TriggerSwitch trigger;

        void Start()
        {
            trigger = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (trigger.ActivatedOnCurrentFrame)
                FindObjectOfType<LevelManager>().CompleteLevel();
        }
    }
}