using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class addLetterToCollectionWhenTriggered : MonoBehaviour
    {

        public enum CollectibleLetter
        {
K,
Z,
T}

        ;

        public CollectibleLetter letterID;

        [Space(10)]
        public TriggerSwitch binarySwitch;

        void Start()
        {
            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();
            if (GameControl.control.items.lettersCollected[(int)letterID])
                Object.Destroy(gameObject);
        }

        void Update()
        {
            if (binarySwitch.IsActivated)
            {
                GameControl.control.items.lettersCollected[(int)letterID] = true;
            }
        }
    }
}