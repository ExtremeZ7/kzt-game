using UnityEngine;
using Common.Math;

namespace Controllers
{
    public class CrosshairController : MonoBehaviour
    {

        [Header("Rotation Control")]
        public Vector3 rotateWhenDown;
        public Vector3 rotateWhenUp;
        public bool isLocal;
        public float rotationTime;

        [Header("Scale Control")]
        public Vector3 scaleWhenDown;
        public Vector3 scaleWhenUp;
        public float scaleTime;

        [Space(10)]
        public Transform prefabOfRedXMark;
        public Transform prefabOfElectricBolt;

        [Space(10)]
        public GameObject audioWhenDown;
        public GameObject audioWhenUp;

        private GameObject crosshair;
        private Transform body;

        private Transform headJoint;
        private Transform shooterJoint;
        private Transform leftArmJoint;
        private Transform rightArmJoint;

        private Animator playerAnimator;

        void Awake()
        {
            body = transform.GetChild(0);

            headJoint = transform.GetChild(0).GetChild(1);
            leftArmJoint = transform.GetChild(0).GetChild(2);
            rightArmJoint = transform.GetChild(0).GetChild(3);
            shooterJoint = transform.GetChild(0).GetChild(4);

            playerAnimator = GetComponent<Animator>();
        }

        void Update()
        {
            if (crosshair == null)
            {
                crosshair = GameObject.FindGameObjectWithTag("Crosshair");

                if (crosshair != null)
                {
                    rotateToUp();
                    scaleToUp();
                }
            }
            else
            {
                bool aiming = playerAnimator.GetBool("Aiming");

                crosshair.GetComponent<Animator>().SetBool("Active", aiming);

                if (Input.GetMouseButtonUp(0))
                {
                    rotateToUp();
                    scaleToUp();

                    if (playerAnimator.GetBool("Aiming"))
                        Instantiate(audioWhenUp, crosshair.transform.position, Quaternion.identity);

                    if (crosshair.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("Ready To Fire"))
                        Instantiate(prefabOfElectricBolt, transform.position, Quaternion.identity);

                    playerAnimator.SetBool("Aiming", false);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (aiming)
                    {
                        rotateToDown();
                        scaleToDown();

                        Instantiate(audioWhenDown, crosshair.transform.position, Quaternion.identity);

                    }
                    else
                    {
                        Component newXMark = Instantiate(prefabOfRedXMark, crosshair.transform.position, Quaternion.identity) as Component;
                        newXMark.gameObject.transform.parent = crosshair.transform.parent;
                    }
                }

                PointTowardsCrosshairWhenAiming(aiming);
            }
        }

        void rotateToDown()
        {
            iTween.RotateTo(crosshair, iTween.Hash(
                    "rotation", rotateWhenDown,
                    "islocal", isLocal,
                    "time", rotationTime,
                    "delay", 0f,
                    "easetype", iTween.EaseType.easeOutElastic,
                    "looptype", iTween.LoopType.none));
        }

        void rotateToUp()
        {
            iTween.RotateTo(crosshair, iTween.Hash(
                    "rotation", rotateWhenUp,
                    "islocal", isLocal,
                    "time", rotationTime,
                    "delay", 0f,
                    "easetype", iTween.EaseType.easeOutElastic,
                    "looptype", iTween.LoopType.none));
        }

        void scaleToDown()
        {
            iTween.ScaleTo(crosshair, iTween.Hash(
                    "scale", scaleWhenDown,
                    "time", scaleTime,
                    "delay", 0f,
                    "easetype", iTween.EaseType.easeOutElastic,
                    "looptype", iTween.LoopType.none));
        }

        void scaleToUp()
        {
            iTween.ScaleTo(crosshair, iTween.Hash(
                    "scale", scaleWhenUp,
                    "time", scaleTime,
                    "delay", 0f,
                    "easetype", iTween.EaseType.easeOutElastic,
                    "looptype", iTween.LoopType.none));
        }

        private void PointTowardsCrosshairWhenAiming(bool aiming)
        {
            if (aiming)
            {
                bool noGravity = GetComponent<Animator>().GetBool("No Grav");

                Vector3 theScale = body.localScale;
                theScale.x = Mathf.Abs(theScale.x) * Mathf.Sign(crosshair.transform.position.x - transform.position.x);
                body.localScale = theScale;

                float angle = Trigo.GetAngleBetweenPoints(transform.position, crosshair.transform.position);

                //angle = theScale.x > 0 ? angle : 180 - angle;

                headJoint.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, theScale.x > 0 ? angle : angle + 180f % 360f));
                leftArmJoint.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + (!noGravity ? 90f : (theScale.x > 0 ? 135f : 45f))));
                rightArmJoint.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + (!noGravity ? 90f : (theScale.x > 0 ? 45f : 135f))));
                shooterJoint.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle + 90f));
            }
            else
            {
                headJoint.transform.rotation = Quaternion.Euler(Vector3.zero);
                leftArmJoint.transform.rotation = Quaternion.Euler(Vector3.zero);
                rightArmJoint.transform.rotation = Quaternion.Euler(Vector3.zero);
                shooterJoint.transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }
}