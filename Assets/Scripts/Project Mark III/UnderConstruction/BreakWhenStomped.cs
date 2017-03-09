using UnityEngine;
using AssemblyCSharp;

//DELETE SOON
namespace AssemblyCSharp
{
    public class BreakWhenStomped : TriggerListener
    {

        [Space(10)]
        public TriggerSwitch binarySwitch;

        [Space(10)]
        [Tooltip("Spawn prefabs when stomped on")]
        public Component[] stompPrefabs;
        [Tooltip("Spawn prefabs when it gets destroyed")]
        public Component[] destroyPrefabs;

        private setBoxHits boxHits;

        void Start()
        {
            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();
            boxHits = GetComponent<setBoxHits>();
        }

        void Update()
        {
            if (binarySwitch.ActivatedOnCurrentFrame)
            {
                for (int i = 0; i < stompPrefabs.Length; i++)
                {
                    Instantiate(stompPrefabs[i], transform.position, Quaternion.identity);
                }

                boxHits.hits--;

                if (boxHits.hits <= 0)
                {
                    for (int i = 0; i < destroyPrefabs.Length; i++)
                    {
                        Instantiate(destroyPrefabs[i], transform.position, Quaternion.identity);
                    }
                    Object.Destroy(this.gameObject);
                }
            }
        }
    }
}