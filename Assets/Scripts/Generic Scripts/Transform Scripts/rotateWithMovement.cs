using UnityEngine;
using Common.Math;

public class rotateWithMovement : MonoBehaviour
{

    public float speedToExceed = 0.01f;

    [Space(10)]
    public float angleOffset;

    [Space(10)]
    public bool preventFromBeingUpsideDown = true;

    [Space(10)]
    public float idleRotationSpeed;
    public float idleRotationVariation;
    //public bool aimTowardsChangeInSpeed;

    private Vector3 previousPosition;
    //private Vector2 previousSpeed;


    void Start()
    {
        previousPosition = transform.position;
        //previousSpeed = Vector2.zero;
    }

    void Update()
    {
        float angle;

        if (Vector3.Distance(transform.position, previousPosition) > speedToExceed)
        {
            angle = Trigo.GetAngleBetweenPoints(previousPosition, transform.position);


            if (transform.localScale.x < 0)
                angle = 180 + angle;

        }
        else
        {
            angle = (transform.localRotation.eulerAngles.z + (idleRotationSpeed + Random.Range(0, idleRotationVariation)) * Time.deltaTime) % 360;

            if (transform.localScale.x < 0)
                angle = 180 + angle;
        }

        iTween.RotateUpdate(gameObject, new Vector3(0, 0, angle + angleOffset), 0f);

        //previousSpeed = transform.position - previousPosition;
        previousPosition = transform.position;
    }
}
