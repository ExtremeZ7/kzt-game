using UnityEngine;
using Common.Extensions;

public class pushOtherObjectWithRigidbodyForce : MonoBehaviour
{

    public string[] tags;

    [Space(10)]
    public Vector2 rigidbodyForce;

    [Space(10)]
    public Vector2 maximumVelocity;

    void OnTriggerStay2D(Collider2D coll)
    {
        if (tags.Contains(coll.gameObject.tag))
        {
            Rigidbody2D rigidbody = coll.gameObject.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(rigidbodyForce);

            if (rigidbody.velocity.x > maximumVelocity.x)
                rigidbody.velocity = new Vector2(maximumVelocity.x, rigidbody.velocity.y);

            if (rigidbody.velocity.y > maximumVelocity.y)
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, maximumVelocity.y);
        }
    }
}
