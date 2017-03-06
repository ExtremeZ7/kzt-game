//———————————————————————–
// <copyright file=”TriggerSwitch.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————-
using UnityEngine;

public class TriggerSwitch : CollisionHandler
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        Trigger(coll.gameObject);
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (activateOnStay)
        {
            Trigger(coll.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        ExitTrigger(coll.gameObject);
    }
}