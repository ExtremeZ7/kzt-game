using UnityEngine;
using AssemblyCSharp;

public class launchABlueCannonballTowardsThePlayer : MonoBehaviour
{

    public GameObject prefabOfCannonball;
    public Transform shootPoint;

    [Space(10)]
    public float launchForce;
    public float distantForceIncrease;

    [Space(10)]
    public Transform maxRange;

    [Space(10)]
    public TimerSwitch shootTimer;

    private PlayerController playerControl;
    private Transform playerTransform;

    [Space(10)]
    public iTween.EaseType rotationEaseType;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(maxRange.position, 0.2f);
    }

    void Awake()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 45f);
    }

    void Update()
    {
        if (playerTransform != null || Help.WaitForPlayer(ref playerControl))
        {
            playerTransform = playerControl.gameObject.transform;

            if (shootTimer.ActivatedOnCurrentFrame)
            {
                float targetAngle;

                float playerDistance = Mathf.Abs(playerTransform.position.x - maxRange.position.x);
                float turtleDistance = Mathf.Abs(transform.position.x - maxRange.position.x);

                targetAngle = 45f * ((turtleDistance - playerDistance) / turtleDistance);

                float shootAngle = transform.rotation.eulerAngles.z + 90f;
                if (transform.parent.localScale.x < 0)
                    shootAngle = 180 - shootAngle;

                float finalLaunchForce = launchForce + (distantForceIncrease * ((turtleDistance - playerDistance) / turtleDistance));

                GameObject cannonball = Instantiate(prefabOfCannonball, shootPoint.position, Quaternion.identity) as GameObject;
                cannonball.GetComponent<Rigidbody2D>().AddForce(new Vector2(finalLaunchForce * Mathf.Cos(shootAngle * Mathf.Deg2Rad), finalLaunchForce * Mathf.Sin(shootAngle * Mathf.Deg2Rad)));

                iTween.RotateTo(gameObject, iTween.Hash("rotation", new Vector3(0f, 0f, targetAngle), "time", 0.6f, "easeType", rotationEaseType, "delay", 0.1f));
                iTween.PunchScale(gameObject, iTween.Hash("amount", new Vector3(0.4f, 0.4f, 0f), "time", 0.6f));
            }
        }
    }
}
