using UnityEngine;
using System;

[Serializable]
public class Oscillator
{
    public AnimationCurve curve;

    public FloatWithVariation cycleTime = new FloatWithVariation(1f);
    public FloatWithVariation delay;
    public FloatWithVariation phase;
    public FloatWithVariation magnitude = new FloatWithVariation(1f);
    public FloatWithVariation valueOffset;

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

    public float TimeZeroValue
    {
        get
        { 
            return (curve.Evaluate(phase.VariedValue) * magnitude.VariedValue)
            + valueOffset.VariedValue;
        }
    }

    public bool isRunning
    {
        get{ return registered; }
    }

    public bool isValid
    {
        get{ return curve.keys.Length > 1; }
    }

    public float ValueWithExtraPhase(float phase)
    {
        return (curve.Evaluate(time
            + this.phase.Value +
            phase)
        * magnitude.Value) +
        valueOffset.Value;
    }

    public float TimeZeroValueWithExtraPhase(float phase)
    {
        return (curve.Evaluate(this.phase.VariedValue + phase) * magnitude.VariedValue)
        + valueOffset.VariedValue;
    }

    public void Start()
    {
        cycleTime.VaryValue();
        delay.VaryValue();
        phase.VaryValue();
        magnitude.VaryValue();
        valueOffset.VaryValue();

        registered = true;
        delayTimer = delay.Value;
        OscillatorManager.Instance.Register(this);
    }

    public void Stop()
    {
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
        time = (time + (Time.deltaTime / cycleTime.Value)) % 2.0f;
    }
}
