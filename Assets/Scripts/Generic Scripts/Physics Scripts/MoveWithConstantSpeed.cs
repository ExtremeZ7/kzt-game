using UnityEngine;
using Common.Extensions;

public class MoveWithConstantSpeed : MonoBehaviour
{

    public float xSpeed;
    public float ySpeed;

    [Space(10)]
    public string[] directionShiftTags;

    void Start()
    {
        xSpeed *= Mathf.Sign(transform.localScale.x);
        ySpeed *= Mathf.Sign(transform.localScale.y);
    }

    void Update()
    {
        transform.position = new Vector3(xSpeed != 0f ? transform.position.x + (xSpeed * Time.deltaTime) : transform.position.x,
            ySpeed != 0f ? transform.position.y + (ySpeed * Time.deltaTime) : transform.position.y,
            transform.position.z);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (directionShiftTags.Contains(coll.gameObject.tag))
        {
            xSpeed *= Mathf.Sign(transform.position.x - coll.gameObject.transform.position.x) * Mathf.Sign(xSpeed);
        }
    }
}
