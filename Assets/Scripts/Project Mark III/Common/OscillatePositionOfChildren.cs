//———————————————————————–
// <copyright file=”OscillatePositionOfChildren.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using UnityEngine;
using System;
using System.Collections.Generic;

public class OscillatePositionOfChildren : MonoBehaviour
{
    public Oscillator mainXOscillator;
    public Oscillator mainYOscillator;
    public Vector2Oscillator[] childOffsets;

    List<Transform> children = new List<Transform>();

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!childOffsets[i].xOsc.isValid && !childOffsets[i].yOsc.isValid)
            {
                continue;
            }

            children.Add(transform.GetChild(i));
        }
    }

    void OnEnable()
    {
        mainXOscillator.Start();
        mainYOscillator.Start();

        for (int i = 0; i < childOffsets.Length; i++)
        {
            childOffsets[i].xOsc.Start();
            childOffsets[i].yOsc.Start();
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < childOffsets.Length; i++)
        {
            childOffsets[i].xOsc.Stop();
            childOffsets[i].yOsc.Stop();
        }
    }

    void Update()
    {
        // Calculate Main X Value
        //
        float mainXValue = 0f;
        if (mainXOscillator.isValid && mainXOscillator.isRunning)
        {
            mainXValue = mainXOscillator.Value;
        }

        // Calculate Main Y Value
        //
        float mainYValue = 0f;
        if (mainYOscillator.isValid && mainYOscillator.isRunning)
        {
            mainYValue = mainYOscillator.Value;
        }

        for (int i = 0; i < children.Count; i++)
        {
            // If the child currently doesn't exist. Remove it and get to
            // the next one.
            if (children[i] == null ||
                !children[i].gameObject.activeSelf)
            {
                children.Remove(children[i]);
                continue;
            }

            CalculateX(children[i], childOffsets[i], mainXValue);
            CalculateY(children[i], childOffsets[i], mainYValue);

        }
    }

    void CalculateX(Transform tf, Vector2Oscillator osc, float mainValue)
    {
        if (osc.skipX)
        {
            return;
        }

        // Start from the Main X Value
        //
        tf.localPosition = new Vector3(
            mainValue,
            tf.localPosition.y
        );

        // Offset X based on child parameters
        //
        if (osc.xOsc.isValid && osc.xOsc.isRunning)
        {
            tf.localPosition = new Vector3(
                tf.localPosition.x + osc.xOsc.Value,
                tf.localPosition.y
            );
        }
        else
        {
            tf.localPosition = new Vector3(
                tf.localPosition.x
                + mainXOscillator.ValueWithPhaseOffset(
                    osc.xOsc.phase.Value),
                tf.localPosition.y
            );
        }
    }

    void CalculateY(Transform tf, Vector2Oscillator osc, float mainValue)
    {
        if (osc.skipY)
        {
            return;
        }

        // Start from the Main Y Value
        //
        tf.localPosition = new Vector3(
            tf.localPosition.y,
            mainValue
        );

        // Offset Y based on child parameters
        //
        if (osc.yOsc.isValid && osc.yOsc.isRunning)
        {
            tf.localPosition = new Vector3(
                tf.localPosition.x,
                tf.localPosition.y + osc.yOsc.Value
            );
        }
        else
        {
            tf.localPosition = new Vector3(
                tf.localPosition.x,
                tf.localPosition.y
                + mainYOscillator.ValueWithPhaseOffset(
                    osc.yOsc.phase.Value)
            );
        }
    }


    [ContextMenu("Validate Twice")]
    void ValidateTwice()
    {
        OnValidate();
        OnValidate();
    }

    void OnValidate()
    {
        Array.Resize<Vector2Oscillator>(ref childOffsets, transform.childCount);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i) != null)
            {
                childOffsets[i].name = transform.GetChild(i).name;
            }

            childOffsets[i].Validate();
        }
    }
}

