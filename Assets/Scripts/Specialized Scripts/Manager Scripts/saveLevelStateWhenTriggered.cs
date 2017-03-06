using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class saveLevelStateWhenTriggered : MonoBehaviour
    {

        private LevelManager levelManager;

        public TriggerSwitch binarySwitch;

        public GameObject alertPrefab;

        private int checkpointIndex;
        private Transform playerCheckpoint;

        void OnDrawGizmos()
        {

            checkpointIndex = transform.GetSiblingIndex() + 1;

            if (checkpointIndex < GameObject.FindGameObjectWithTag("Checkpoint List").transform.childCount)
            {
                Gizmos.color = Color.gray;
                Gizmos.DrawLine(transform.position, GameObject.FindGameObjectWithTag("Checkpoint List").transform.GetChild(checkpointIndex).position);

                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(GameObject.FindGameObjectWithTag("Checkpoint List").transform.GetChild(checkpointIndex).position, 0.25f);
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(transform.position, 0.25f);
            }
        }

        void Start()
        {
            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();

            checkpointIndex = transform.GetSiblingIndex() + 1;

            playerCheckpoint = GameObject.FindGameObjectWithTag("Checkpoint List").transform.GetChild(checkpointIndex);

            levelManager = FindObjectOfType<LevelManager>();
        }

        void Update()
        {
            if (binarySwitch.ActivatedOnCurrentFrame)
            {
                levelManager.saveCheckpoint(playerCheckpoint);
                if (alertPrefab != null)
                    Instantiate(alertPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}