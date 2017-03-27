//———————————————————————–
// <copyright file=”FollowAPath.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using UnityEngine;

public class FollowAPath : PathObject
{
    public TaggedFloat movement = new TaggedFloat(1f, UseValueAs.Speed);
    public float startTime;

    float time;
    float totalTime;
    const int levelOfDetail = 1;

    void Start()
    {
        path.SetDefaultPosition(transform.localPosition);
        time = startTime;

        Keyframe lastXKey = path.x.keys[path.x.keys.Length - 1];
        Keyframe lastYKey = path.y.keys[path.y.keys.Length - 1];
        totalTime = Mathf.Max(lastXKey.time, lastYKey.time);
    }

    public override void ManagedUpdate()
    {
        base.ManagedUpdate();
        transform.localPosition = path.Default
        + Vector2.Scale(path.Evaluate(time), transform.localScale);

        switch (movement.useValueAs)
        {
            case UseValueAs.Time:
                // Time will be calculated based on how many seconds it will
                // take to go from zero time to the last key
                time += Time.deltaTime / movement.value * totalTime;
                break;

            case UseValueAs.Speed:
                // Time is calculated based on how many units are traveled
                // per second
                time += Time.deltaTime * movement.value;
                break;

        }

        if (time > 0f)
        {
            // totalTime is multiplied by 2 to account for Ping Pong
            time %= totalTime * 2f;
        }
    }

    void OnDrawGizmosSelected()
    {
        path.SetDefaultPosition(Vector2.zero);

        if (path.x.keys.Length == 0 || path.y.keys.Length == 0)
        {
            return;
        }
            
        float time = 0f;
        const float sphereSize = 0.1f;

        Keyframe lastXKey = path.x.keys[path.x.keys.Length - 1];
        Keyframe lastYKey = path.y.keys[path.y.keys.Length - 1];

        var previousPosition = path.Evaluate(time);
        Gizmos.color = Color.red;
        
        // Draw Lines
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
                
            Gizmos.DrawLine(
                transform.position.ToVector2() + Vector2.Scale(previousPosition, transform.localScale),
                transform.position.ToVector2() + Vector2.Scale(nextPosition, transform.localScale));
            previousPosition = nextPosition;
        }

        time = 0f;
        totalTime = Mathf.Max(lastXKey.time, lastYKey.time);
        Gizmos.color = Color.green;

        // Draw Spheres
        while (time <= lastXKey.time || time <= lastYKey.time)
        {
            Gizmos.DrawSphere(transform.position.ToVector2()
                + Vector2.Scale(path.Evaluate(time), transform.localScale)
                , sphereSize);

            switch (movement.useValueAs)
            {
                case UseValueAs.Time:
                    // Time will be calculated based on how many seconds it will
                    // take to go from zero time to the last key
                    time += totalTime / movement.value;
                    break;

                case UseValueAs.Speed:
                    time += 1.0f * movement.value;
                    break;

            }
        }
    }
}
