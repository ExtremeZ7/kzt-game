//———————————————————————–
// <copyright file=”FollowAPath.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using UnityEngine;

public class FollowAPath : PathObject
{
    public TaggedFloat movement;

    const int levelOfDetail = 1;

    void Start()
    {
        path.SetDefaultPosition(transform.localPosition);
    }

    public override void ManagedUpdate()
    {
		
    }

    void OnDrawGizmos()
    {
        path.SetDefaultPosition(transform.position);

        if (path.x.keys.Length == 0 || path.y.keys.Length == 0)
        {
            return;
        }
            
        float time = 0f;

        Keyframe lastXKey = path.x.keys[path.x.keys.Length - 1];
        Keyframe lastYKey = path.y.keys[path.y.keys.Length - 1];

        var previousPosition = path.Evaluate(time);
        Gizmos.color = Color.red;
        while (time <= lastXKey.time || time <= lastYKey.time)
        {
            Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g,
                1.0f - Gizmos.color.b);

            time += 1.0f / (float)levelOfDetail;
            var nextPosition = path.Evaluate(time);

            //If the next position is equal to the previous position, skip it
            if (previousPosition.Equals(nextPosition))
            {
                continue;
            }
                
            Gizmos.DrawLine(transform.position.ToVector2() + previousPosition,
                transform.position.ToVector2() + nextPosition);
            previousPosition = nextPosition;
        }
    }
}
