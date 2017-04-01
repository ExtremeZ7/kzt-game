using UnityEngine;
using Common.Extensions;
using System.Collections.Generic;

namespace Common.Physics2D
{
    public enum CollisionSides2D
    {
        none,
        Top,
        Bottom,
        Left,
        Right
    }

    public static class Collision2D : System.Object
    {
        public static CollisionSides2D[] GetCollisionSides2D(UnityEngine.Collision2D other)
        {
            var sides = new List<CollisionSides2D>();

            Vector3 contactPoint = other.contacts[0].point;
            Vector3 center = other.collider.bounds.center;

            float right = contactPoint.x - center.x;
            float top = contactPoint.y - center.y;

            if (!top.isNearZero())
            {
                
                if (top > 0f)
                {
                    sides.Add(CollisionSides2D.Top);
                }
                else
                {
                    sides.Add(CollisionSides2D.Bottom);
                }
            }

            if (!right.isNearZero())
            {

                if (right > 0f)
                {
                    sides.Add(CollisionSides2D.Right);
                }
                else
                {
                    sides.Add(CollisionSides2D.Left);
                }
            }

            if (sides.Count == 0)
            {
                sides.Add(CollisionSides2D.none);
            }

            return sides.ToArray();
        }
    }
}