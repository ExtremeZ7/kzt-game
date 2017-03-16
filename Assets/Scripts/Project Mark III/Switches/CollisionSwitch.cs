//———————————————————————–
// <copyright file=”CollisionSwitch.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————-
using UnityEngine;

public class CollisionSwitch : CollisionHandler
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        Trigger(coll.gameObject);
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (activateOnStay)
        {
            Trigger(coll.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        ExitTrigger(coll.gameObject);
    }
}