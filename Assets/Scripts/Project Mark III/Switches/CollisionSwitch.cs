//———————————————————————–
// <copyright file=”CollisionSwitch.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————-
using UnityEngine;
using System;
using Common.Physics2D;

public class CollisionSwitch : CollisionHandler
{
    public CollisionSides sides;

    void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        Trigger(other.gameObject);
    }

    void OnCollisionStay2D(UnityEngine.Collision2D other)
    {
        if (activateOnStay)
        {
            Trigger(other.gameObject);
        }
    }

    void OnCollisionExit2D(UnityEngine.Collision2D other)
    {
        ExitTrigger(other.gameObject);
    }

    [Serializable]
    public class CollisionSides
    {
        public bool top = true;
        public bool bottom = true;
        public bool left = true;
        public bool right = true;

        public bool CollidedAtEnabledSide(Transform objHit, Transform objOther)
        {
            CollisionSides2D[] sides =
                Common.Physics2D.Collision2D.GetCollisionSides2D(objHit, objOther);
        }
    }
}