//———————————————————————–
// <copyright file=”TriggerSwitch.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————-
using UnityEngine;
using System.Linq;

public class TriggerSwitch : CollisionHandler
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Trigger(other.gameObject);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (!activateOnStay)
        {
            return;
        }

        Trigger(other.gameObject);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        ExitTrigger(other.gameObject);
    }
}