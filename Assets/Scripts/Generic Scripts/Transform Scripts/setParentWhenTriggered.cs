using UnityEngine;
using Common.Extensions;

public class setParentWhenTriggered : MonoBehaviour
{
    public string[] tags;

    [Space(10)]
    public Component objectToSetParentTo;
    public Transform newParent;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (tags.Contains(coll.gameObject.tag))
        {
            objectToSetParentTo.gameObject.transform.parent = newParent;
        }
    }
}
