using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class clonePrefabWithTrigger : MonoBehaviour
    {

        public TriggerSwitch trigger;

        [Space(10)]
        public GameObject prefab;

        [Space(10)]
        public bool setAsChild;

        void Awake()
        {
            if (trigger == null)
                trigger = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (trigger.ActivatedOnCurrentFrame)
            {
                GameObject newClone = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;

                if (setAsChild)
                    newClone.transform.parent = transform;
            }
        }
    }
}