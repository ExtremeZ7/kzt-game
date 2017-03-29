using UnityEngine;
using Common.Math;

public class rotateObjectTowardsObjectWithTag : MonoBehaviour
{

    public GameObject objectToRotate;
    public string targetTag;
    private GameObject objectToPointTo;
    [Tooltip("Set this in case a parent usually flips scale.")]
    public GameObject parentObject;

    [Space(10)]
    public float speed;
    public float angleOffset;
    [Tooltip("This will freeze the rotation angle to the inputted angleOffset.")]
    public bool staticRotation;

    void Start()
    {
        if (objectToRotate == null)
            objectToRotate = gameObject;

        objectToPointTo = GameObject.FindGameObjectWithTag(targetTag);
    }

    void Update()
    {
        if (objectToPointTo != null)
        {
            if (objectToPointTo.gameObject.activeSelf)
            {
                if (!staticRotation)
                {
                    float angle = Trigo.GetAngleBetweenPoints(objectToRotate.transform.position, objectToPointTo.transform.position);

                    if (parentObject != null && parentObject.transform.localScale.x < 0)
                        angle = 180 - angle;

                    objectToRotate.transform.rotation = Quaternion.Euler(new Vector3(0, 0, speed > 0 ? Mathf.MoveTowardsAngle(objectToRotate.transform.rotation.eulerAngles.z, angle + angleOffset, speed * Time.deltaTime) : angle + angleOffset));
                }
                else
                {
                    objectToRotate.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleOffset));
                }
            }
        }
        else
        {
            Start();
        }
    }
}
