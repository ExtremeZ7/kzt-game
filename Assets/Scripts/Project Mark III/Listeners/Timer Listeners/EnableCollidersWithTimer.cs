using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnableCollidersWithTimer : TimerListener
{
    [Header("Main Fields")]
    public List<Collider2D> colliderComponents;

    void Start()
    {
        colliderComponents.Where(i => i != null).ToList();

        // If the list is empty, try to populate it with
        // all the colliders in the current gameobject
        //
        if (colliderComponents.Count == 0)
        {
            foreach (Collider2D colliderComp in GetComponents<Collider2D>())
            {
                colliderComponents.Add(colliderComp);
            }
        }
    }

    public override void ManagedUpdate()
    {
        foreach (Collider2D colliderComp in colliderComponents)
        {
            colliderComp.enabled = Listener.IsActivated;  
        }

    }
}
