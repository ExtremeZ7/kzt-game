using UnityEngine;
using Common.Extensions;

public class teleportObjectToPointWhenTriggered : MonoBehaviour
{

    public string[] tags;
    public Transform pointToTeleportTo;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (tags.Contains(coll.gameObject.tag))
        {
            coll.gameObject.transform.position = pointToTeleportTo.position;
        }
    }
}
