using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

public class EnableCollidersWithTimer : TimerListener
{
    [Header("Main Fields")]
    [FormerlySerializedAs("colliderComponents")]
    public List<Collider2D> colliders;

    void Start()
    {
        colliders = colliders.Where(i => i != null).ToList();

        // If the list is empty, try to populate it with
        // all the colliders in the current gameobject
        //
        if (colliders.Count == 0)
        {
            foreach (Collider2D colliderComp in GetComponents<Collider2D>())
            {
                colliders.Add(colliderComp);
            }
        }
    }

    public override void ManagedUpdate()
    {
        foreach (Collider2D colliderComp in colliders)
        {
            colliderComp.enabled = Listener.IsActivated;  
        }

    }
}
