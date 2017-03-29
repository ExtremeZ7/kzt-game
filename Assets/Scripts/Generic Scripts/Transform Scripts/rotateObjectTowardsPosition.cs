using UnityEngine;
using Common.Math;

public class rotateObjectTowardsPosition : MonoBehaviour
{

    public Transform objectToRotate;
    public Transform objectToPointTo;
    [Tooltip("Set this in case a parent usually flips scale.")]
    public GameObject parentObject;

    [Space(10)]
    public float angleOffset;
    [Tooltip("This will freeze the rotation angle to the inputted angleOffset.")]
    public bool staticRotation;

    void Awake()
    {
        if (objectToRotate == null)
            objectToRotate = transform;
        if (objectToPointTo == null)
            objectToPointTo = transform;
    }

    void Update()
    {
        if (!staticRotation)
        {
            float angle = Trigo.GetAngleBetweenPoints(objectToRotate.transform.position, objectToPointTo.position);

            objectToRotate.rotation = Quaternion.Euler(new Vector3(0, 0, (parentObject != null && parentObject.transform.localScale.x < 0 ? 180 - angle : angle) + angleOffset));
        }
        else
        {
            objectToRotate.rotation = Quaternion.Euler(new Vector3(0, 0, angleOffset));
        }
    }
}
