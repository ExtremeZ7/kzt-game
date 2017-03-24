using System.Collections;
using UnityEngine;
using System;

[Serializable]
public class Oscillator
{
    public AnimationCurve curve;

    [Tooltip("How much time (in seconds) does it take to complete one"
        + "oscillation")]
    public float cycleTime = 1f;
    public float delay;
    [Range(0f, 1f)]
    public float phase;

    public float valueOffset;
    public float valueVariation;

    float time;
    float delayTimer;
    public bool registered;

    public float Value
    {
        get
        {
            return (curve.Evaluate(time + phase) +
            valueOffset).Variation(valueVariation);
        }
    }

    public bool Running
    {
        get{ return registered && delayTimer.IsNearZero(); }
    }

    public void Start()
    {
        OscillatorManager.Instance.Register(this);
        registered = true;
        delayTimer = delay;
    }

    public void Stop()
    {
        OscillatorManager.Instance.Unregister(this);
        registered = false;

    }

    public void ManagedUpdate()
    {
        // Start the delay timer and keep returning until it reaches zero
        //
        if ((delayTimer = Mathf.MoveTowards(delayTimer, 0f, Time.deltaTime))
            > 0f)
        {
            return;
        }

        // Calculate the time for this frame
        //
        time = (time + (Time.deltaTime / cycleTime)) % 1.0f;
    }

    public void ValidateKeys()
    {
        for (int i = 0; i < curve.keys.Length; i++)
        {
            if (curve[i].time < 0f)
            {
                curve[i].time = 0f;
            }
            else if (curve[i].time > 1f)
            {
                curve[i].time = 1f;
            }

            if (curve[i].value < -1f)
            {
                curve[i].value = -1f;
            }
            else if (curve[i].value > 1f)
            {
                curve[i].value = 1f;
            }
        }
    }
}
