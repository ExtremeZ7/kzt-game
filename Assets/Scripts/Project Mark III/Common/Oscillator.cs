using UnityEngine;
using System;

[Serializable]
public class Oscillator
{
    public AnimationCurve curve;

    [Tooltip("How much time (in seconds) does it take to complete one"
        + "oscillation")]
    public FloatWithVariation cycleTime = new FloatWithVariation(1f);
    public FloatWithVariation delay;
    public FloatWithVariation phase;
    public FloatWithVariation magnitude;
    public FloatWithVariation valueOffset = new FloatWithVariation(0f, true);

    float time;
    float delayTimer;
    public bool registered;

    public float Value
    {
        get
        {
            return (curve.Evaluate(time + phase.Value) * magnitude.Value) +
            valueOffset.Value;
        }
    }

    public bool isRunning
    {
        get{ return registered && delayTimer.IsNearZero(); }
    }

    public bool isValid
    {
        get{ return curve.keys.Length > 1; }
    }

    public float ValueWithPhaseOffset(float phase, float variation = 0f)
    {
        return (curve.Evaluate(time
            + this.phase
            + phase.Variation(variation))
        * magnitude.Value) +
        valueOffset.Value;
    }

    public void Start()
    {
        if (!isValid)
        {
            return;
        }

        registered = true;
        delayTimer = delay.Value;
        OscillatorManager.Instance.Register(this);
    }

    public void Stop()
    {
        if (!isValid)
        {
            return;
        }

        registered = false;
        OscillatorManager.Instance.Unregister(this);
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
        time = (time + (Time.deltaTime / cycleTime.Value)) % 1.0f;
    }
}
