using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class generateHelpfulHintWhenTriggered : MonoBehaviour
    {

        public TriggerSwitch binarySwitch;
        public GameObject hintPrefab;

        [Space(10)]
        [TextArea(1, 3)]
        public string hintString;

        private generateHelpfulHintWhenTriggered self;

        void Start()
        {
            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();
            self = this;
        }

        void Update()
        {
            if (binarySwitch.ActivatedOnCurrentFrame && !hintString.Equals(""))
            {
                Help.GenerateHintBox(hintString);
                self.enabled = false;
            }
        }
    }
}