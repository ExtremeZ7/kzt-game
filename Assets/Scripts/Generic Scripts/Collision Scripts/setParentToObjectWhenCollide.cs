using UnityEngine;
using UnityEngine.Serialization;
using Common.Extensions;

public class setParentToObjectWhenCollide : MonoBehaviour
{

    [FormerlySerializedAs("tags")]
    public string[] tagFilter;

    [Space(10)]
    public Transform assignParentTo;

    void Start()
    {
        if (assignParentTo == null)
            assignParentTo = transform;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.contacts.Length > 0)
        {
            ContactPoint2D contact = coll.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
            {
                if (tagFilter.Contains(coll.gameObject.tag))
                {
                    assignParentTo.parent = coll.gameObject.transform;
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.contacts.Length > 0)
        {
            ContactPoint2D contact = coll.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
            {
                if (assignParentTo.parent == null && tagFilter.Contains(coll.gameObject.tag))
                {
                    assignParentTo.parent = coll.gameObject.transform;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (tagFilter.Contains(coll.gameObject.tag))
        {
            assignParentTo.parent = null;
        }
    }
}
