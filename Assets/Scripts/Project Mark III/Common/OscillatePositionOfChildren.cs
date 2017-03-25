//———————————————————————–
// <copyright file=”OscillatePositionOfChildren.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using UnityEngine;
using System.Collections.Generic;

public class OscillatePositionOfChildren : OscillatorCalculator
{
    // fields
    public Vector2Oscillator mainOscillators;

    [Space(10)]
    public List<Vector2Oscillator> childOffsets =
        new List<Vector2Oscillator>();
    List<Transform> children = new List<Transform>();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i));
        }
    }

    new void OnEnable()
    {
        base.OnEnable();

        if (mainOscillators.xEnabled)
        {
            mainOscillators.xOsc.Start();
        }

        if (mainOscillators.yEnabled)
        {
            mainOscillators.yOsc.Start();
        }

        for (int i = 0; i < childOffsets.Count; i++)
        {
            if (childOffsets[i].xEnabled)
            {
                childOffsets[i].xOsc.Start();
            }

            if (childOffsets[i].yEnabled)
            {
                childOffsets[i].yOsc.Start();
            }
        }
    }

    new void OnDisable()
    {
        base.OnDisable();

        for (int i = 0; i < childOffsets.Count; i++)
        {
            childOffsets[i].xOsc.Stop();
            childOffsets[i].yOsc.Stop();
        }
    }

    public override void ManagedUpdate()
    {
        // Calculate Main X Value
        //
        float mainXValue = 0f;
        if (mainOscillators.xOsc.isValid && mainOscillators.xOsc.isRunning)
        {
            mainXValue = mainOscillators.xOsc.Value;
        }

        // Calculate Main Y Value
        //
        float mainYValue = 0f;
        if (mainOscillators.yOsc.isValid && mainOscillators.yOsc.isRunning)
        {
            mainYValue = mainOscillators.yOsc.Value;
        }

        for (int i = 0; i < children.Count; i++)
        {
            // If the child currently doesn't exist. Remove it and get to
            // the next one.
            if (children[i] == null)
            {
                children.Remove(children[i]);
                childOffsets.Remove(childOffsets[i]);
                i--;
                continue;
            }

            //Stop and Run The Oscillators Depending On Their Active State
            if (!children[i].gameObject.activeSelf)
            {
                if (childOffsets[i].xEnabled && childOffsets[i].xOsc.isRunning)
                {
                    childOffsets[i].xOsc.Stop();
                }

                if (childOffsets[i].yEnabled && childOffsets[i].yOsc.isRunning)
                {
                    childOffsets[i].yOsc.Stop();
                }
            }
            else
            {
                if (childOffsets[i].xEnabled && !childOffsets[i].xOsc.isRunning)
                {
                    childOffsets[i].xOsc.Start();
                }

                if (childOffsets[i].yEnabled && !childOffsets[i].yOsc.isRunning)
                {
                    childOffsets[i].yOsc.Start();
                }
            }

            if (mainOscillators.xEnabled && childOffsets[i].xEnabled)
            {
                CalculateX(children[i], childOffsets[i], mainXValue);
            }

            if (mainOscillators.yEnabled && childOffsets[i].yEnabled)
            {
                CalculateY(children[i], childOffsets[i], mainYValue);
            }
        }
    }

    void CalculateX(Transform tf, Vector2Oscillator osc, float mainValue)
    {       
        // Offset X based on child parameters
        //
        if (osc.xOsc.isValid && osc.xOsc.isRunning)
        {
            tf.localPosition = new Vector3(
                mainValue + osc.xOsc.Value,
                tf.localPosition.y
            );
        }
        else
        {
            tf.localPosition = new Vector3(
                mainOscillators.xOsc.ValueWithExtraPhase(
                    osc.xOsc.phase.Value) + osc.xOsc.valueOffset.Value,
                tf.localPosition.y
            );
        }
    }

    void CalculateY(Transform tf, Vector2Oscillator osc, float mainValue)
    {
        // Offset Y based on child parameters
        //
        if (osc.yOsc.isValid && osc.yOsc.isRunning)
        {
            tf.localPosition = new Vector3(
                tf.localPosition.x,
                mainValue + osc.yOsc.Value
            );
        }
        else
        {
            tf.localPosition = new Vector3(
                tf.localPosition.x,
                mainOscillators.yOsc.ValueWithExtraPhase(
                    osc.yOsc.phase.Value) + +osc.yOsc.valueOffset.Value
            );
        }
    }

    [ContextMenu("Spread Phase Values")]
    void SpreadPhaseValues()
    {
        for (int i = 0; i < childOffsets.Count; i++)
        {
            childOffsets[i].xOsc.phase.Value = 1.0f / childOffsets.Count * i;
            childOffsets[i].yOsc.phase.Value = 1.0f / childOffsets.Count * i;
        }

        OnValidate();
    }

    [ContextMenu("Enable All X Oscillators")]
    void EnableAllXOsc()
    {
        mainOscillators.xEnabled = true;

        for (int i = 0; i < childOffsets.Count; i++)
        {
            childOffsets[i].xEnabled = true;
        }

        OnValidate();
    }

    [ContextMenu("Enable All Y Oscillators")]
    void EnableAllYOsc()
    {
        mainOscillators.yEnabled = true;

        for (int i = 0; i < childOffsets.Count; i++)
        {
            childOffsets[i].yEnabled = true;
        }

        OnValidate();
    }

    [ContextMenu("Enable All Oscillators")]
    void EnableAllOsc()
    {
        mainOscillators.xEnabled = true;
        mainOscillators.yEnabled = true;

        for (int i = 0; i < childOffsets.Count; i++)
        {
            childOffsets[i].xEnabled = true;
            childOffsets[i].yEnabled = true;
        }

        OnValidate();
    }

    [ContextMenu("Validate")]
    void OnValidate()
    {
        childOffsets = childOffsets
            .Resize<Vector2Oscillator>(transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) != null)
            {
                childOffsets[i].Validate();

                Transform child = transform.GetChild(i);

                childOffsets[i].name = child.name;
                childOffsets[i].xEnabled &= mainOscillators.xEnabled;
                childOffsets[i].yEnabled &= mainOscillators.yEnabled;

                if (mainOscillators.xEnabled && childOffsets[i].xEnabled)
                {
                    // Offset X based on child parameters
                    //
                    if (childOffsets[i].xOsc.isValid)
                    {
                        child.localPosition = new Vector3(
                            mainOscillators.xOsc.TimeZeroValue
                            + childOffsets[i].xOsc.TimeZeroValue,
                            child.localPosition.y);
                    }
                    else
                    {
                        child.localPosition = new Vector3(
                            mainOscillators.xOsc.TimeZeroValueWithExtraPhase(
                                childOffsets[i].xOsc.phase.VariedValue)
                            + childOffsets[i].xOsc.valueOffset.VariedValue,
                            child.localPosition.y);
                    }
                }

                if (mainOscillators.yEnabled && childOffsets[i].yEnabled)
                {
                    // Offset Y based on child parameters
                    //
                    if (childOffsets[i].yOsc.isValid)
                    {
                        child.localPosition = new Vector3(
                            child.localPosition.x,
                            mainOscillators.yOsc.TimeZeroValue
                            + childOffsets[i].yOsc.TimeZeroValue);
                    }
                    else
                    {
                        child.localPosition = new Vector3(
                            child.localPosition.x,
                            mainOscillators.yOsc.TimeZeroValueWithExtraPhase(
                                childOffsets[i].yOsc.phase.VariedValue)
                            + childOffsets[i].yOsc.valueOffset.VariedValue);
                    }
                }
            }


        }
    }
}

