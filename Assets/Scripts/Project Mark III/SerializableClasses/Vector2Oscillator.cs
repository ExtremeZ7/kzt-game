using UnityEngine;
using System;

[Serializable]
public class Vector2Oscillator
{
    [HideInInspector]
    public string name;
    public bool xEnabled = true;
    public Oscillator xOsc;
    public bool yEnabled = true;
    public Oscillator yOsc;

    public bool Enabled
    {
        get{ return xEnabled && yEnabled; }
    }

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