using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class punchPositionWithTrigger : MonoBehaviour
    {

        public string targetTag = "";
        public TriggerSwitch trigger;

        [Space(10)]
        public string optionalName;
        public Vector3 amount;
        public Space space;
        public float time;
        public float delay;
        public iTween.LoopType loopType;

        [Space(10)]
        public float timeRandomizer;

        private GameObject target;

        void OnDrawGizmos()
        {
            Vector3[] path = new Vector3[2];
            path[0] = transform.position;
            path[1] = new Vector3(transform.position.x + amount.x, transform.position.y + amount.y, transform.position.z + amount.z);

            iTween.DrawLineGizmos(path, new Color(1f, 0.5f, 0f));
        }

        void OnValidate()
        {
            if (timeRandomizer < 0f)
                timeRandomizer = 0f;
        }

        void Awake()
        {
            iTween.Init(gameObject);
        }

        void Start()
        {
            if (trigger == null)
                trigger = GetComponent<TriggerSwitch>();

            target = (targetTag == "" ? this.gameObject : GameObject.FindGameObjectWithTag(targetTag));
        }

        void Update()
        {
            if (trigger.ActivatedOnCurrentFrame)
            {
                time += Random.Range(-timeRandomizer, timeRandomizer);

                iTween.PunchPosition(target, iTween.Hash(
                        "name", optionalName,
                        "amount", amount,
                        "space", space,
                        "time", time,
                        "delay", delay,
                        "looptype", loopType));
            }
        }
    }
}