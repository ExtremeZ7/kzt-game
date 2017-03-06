using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class FloatPlayerInABubble : MonoBehaviour
    {

        public enum BubbleState
        {
Idle,
Floating}

        ;

        public float upGravityScale;
        public float floatGravityScale;
        public float downGravityScale;

        [Space(10)]
        public float triggerDistance;

        private TriggerSwitch trigger;
        private PlayerControl playerControl;
        private GameObject player;

        [HideInInspector]
        public BubbleState bubbleState;

        void Start()
        {
            trigger = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (player == null && Helper.WaitForPlayer(ref playerControl))
            {
                player = playerControl.gameObject;
            }

            if (player != null)
            {
                switch (bubbleState)
                {
                    case BubbleState.Idle:
                        if (Vector3.Distance(transform.position, player.transform.position) <= triggerDistance)
                        {
                            FloatPlayerInABubble[] otherBubbles = FindObjectsOfType<FloatPlayerInABubble>();
                            for (int i = 0; i < otherBubbles.Length; i++)
                                if (otherBubbles[i].bubbleState == BubbleState.Floating)
                                    Object.Destroy(otherBubbles[i].gameObject);
                            bubbleState = BubbleState.Floating;
                        }
                        break;
                    case BubbleState.Floating:
                        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
                        int direction = 0;

                        if (Input.GetKey(GameControl.control.settings.upKey))
                            direction += 1;
                        if (Input.GetKey(GameControl.control.settings.downKey))
                            direction -= 1;

                        if (direction == 0)
                            playerRigidbody.gravityScale = floatGravityScale;
                        else if (direction == 1)
                            playerRigidbody.gravityScale = upGravityScale;
                        else if (direction == -1)
                            playerRigidbody.gravityScale = downGravityScale;

                        if (playerRigidbody.velocity.y < -playerRigidbody.gravityScale * 2)
                            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, Mathf.MoveTowards(playerRigidbody.velocity.y, -playerRigidbody.gravityScale * 2, 60f * Time.deltaTime));
				
                        transform.position = player.transform.position;


                        if (trigger.ActivatedOnCurrentFrame || !player.activeSelf)
                        {
                            playerRigidbody.gravityScale = 4f;
                            GameObject newBubble = Instantiate(Resources.Load("Prefabs/Golden Bubble Pop"), transform.position, Quaternion.identity) as GameObject;
                            newBubble.transform.localScale = transform.GetChild(0).localScale;
                            Object.Destroy(gameObject);
                        }

                        break;
                }
            }
        }
    }
}