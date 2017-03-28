using UnityEngine;
using AssemblyCSharp;

namespace AssemblyCSharp
{
    public class bouncePlayerWhenTriggered : MonoBehaviour
    {

        public enum BounceType
        {
SmallBounce,
MidBounce,
HighBounce}

        ;

        public TriggerSwitch binarySwitch;

        [Space(10)]
        public BounceType bounceType;

        [Space(10)]
        public GameObject audioToPlay;

        private PlayerController playerControl;

        void Start()
        {
            if (binarySwitch == null)
                binarySwitch = GetComponent<TriggerSwitch>();
        }

        void Update()
        {
            if (Help.WaitForPlayer(ref playerControl))
            {
                if (binarySwitch.ActivatedOnCurrentFrame)
                {

                    if (audioToPlay != null)
                        Instantiate(audioToPlay, transform.position, Quaternion.identity);

                    if (playerControl.transform.position.y > transform.position.y)
                    {
                        switch (bounceType)
                        {
                            case BounceType.SmallBounce:
                                playerControl.Bounce();
                                break;
                            case BounceType.MidBounce:
                                playerControl.JumpBounce();
                                break;
                            case BounceType.HighBounce:
                                playerControl.UpBounce();
                                break;
                        }
                    }
                    else
                    {
                        playerControl.HeadBounce();
                    }
                }
            }
        }
    }
}