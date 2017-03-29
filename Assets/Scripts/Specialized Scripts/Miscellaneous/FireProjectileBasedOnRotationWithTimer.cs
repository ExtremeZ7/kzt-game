using UnityEngine;
using Common.Math;

public class FireProjectileBasedOnRotationWithTimer : MonoBehaviour
{

    public GameObject projectile;
    public TimerSwitch timer;

    [Space(10)]
    public Transform self;
    public Transform firePosition;
    public GameObject objectToRecoil;

    [Space(10)]
    public float speed;
    public float angleOffset;

    [Space(10)]
    public bool fireWhenSwitchedOn = true;
    public bool fireWhenSwitchedOff = true;

    [Header("Miscellaneous Stuff")]
    public bool flipWithScale;

    void Awake()
    {
        if (objectToRecoil != null)
            iTween.Init(objectToRecoil);
    }

    void Start()
    {
        if (timer == null)
            timer = GetComponent<TimerSwitch>();
        if (self == null)
            self = transform;
    }

    void Update()
    {
        if (timer.isActiveAndEnabled)
        {
            if (timer.ActivatedOnCurrentFrame && fireWhenSwitchedOn || timer.DeactivatedOnCurrentFrame && fireWhenSwitchedOff)
            {
                GameObject newObject = Instantiate(projectile, firePosition.position, Quaternion.identity) as GameObject;
                Rigidbody2D rb2d = newObject.GetComponent<Rigidbody2D>();
                float selfAngle = transform.rotation.eulerAngles.z + (flipWithScale && transform.localScale.x < 0 ? 180f : 0f);
                rb2d.velocity = new Vector2(Trigo.PythagoreanAdjacent(selfAngle + angleOffset, speed), Trigo.PythagoreanOpposite(selfAngle + angleOffset, speed));

                if (objectToRecoil != null)
                {
                    iTween.PunchPosition(objectToRecoil, iTween.Hash(
                            "name", "" + gameObject.GetInstanceID(),
                            "amount", new Vector3(0f, -0.25f),
                            "time", 0.4f,
                            "looptype", iTween.LoopType.none,
                            "easetype", iTween.EaseType.linear
                        ));
                }
					
            }
        }
    }
}
