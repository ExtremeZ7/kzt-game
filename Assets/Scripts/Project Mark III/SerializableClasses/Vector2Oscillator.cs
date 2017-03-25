using UnityEngine;
using System;

[Serializable]
public class Vector2Oscillator
{
    [HideInInspector]
    public string name;
    public bool skipX;
    public Oscillator xOsc;
    public bool skipY;
    public Oscillator yOsc;

    public void Validate()
    {
        if (xOsc.curve.keys.Length == 1)
        {
            xOsc.curve.RemoveKey(0);
        }

        if (yOsc.curve.keys.Length == 1)
        {
            yOsc.curve.RemoveKey(0);
        }
    }
}