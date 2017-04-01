//———————————————————————–
// <copyright file=”CollisionSwitch.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————-
using UnityEngine;
using System;
using Common.Physics2D;
using Common.Extensions;

public class CollisionSwitch : CollisionHandler
{
    public CollisionSides sides;

    void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        if (!sides.CollidedAtEnabledSide(other))
        {
            return;
        }

        Trigger(other.gameObject);
    }

    void OnCollisionStay2D(UnityEngine.Collision2D other)
    {
        if (!activateOnStay)
        {
            return;
        }

        if (!sides.CollidedAtEnabledSide(other))
        {
            return;
        }

        Trigger(other.gameObject);
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

        public bool CollidedAtEnabledSide(UnityEngine.Collision2D other)
        {
            CollisionSides2D[] sides =
                Common.Physics2D.Collision2D.GetCollisionSides2D(other);

            if (sides.Contains<CollisionSides2D>(CollisionSides2D.none))
            {
                return false;
            }

            if (top && sides.Contains<CollisionSides2D>(CollisionSides2D.Top))
            {
                return true;
            }

            if (bottom && sides.Contains<CollisionSides2D>(CollisionSides2D.Bottom))
            {
                return true;
            }

            if (left && sides.Contains<CollisionSides2D>(CollisionSides2D.Left))
            {
                return true;
            }

            if (right && sides.Contains<CollisionSides2D>(CollisionSides2D.Right))
            {
                return true;
            }

            return false;
        }
    }
}