using UnityEngine;
using System;

[Serializable]
public class Vector2Path
{
    public bool xEnabled;
    public AnimationCurve x = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public bool yEnabled;
    public AnimationCurve y = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public float scale = 1f;

    Vector2 defaultPosition = Vector2.zero;

    public Vector2 Default
    {
        get{ return defaultPosition; }
    }

    public void SetDefaultPosition(Vector2 defaultPosition)
    {
        this.defaultPosition = defaultPosition;
    }

    public Vector2 Evaluate(float time)
    {
        return new Vector2(xEnabled ? x.Evaluate(time) * scale : defaultPosition.x,
            yEnabled ? y.Evaluate(time) * scale : defaultPosition.y);
    }
}
