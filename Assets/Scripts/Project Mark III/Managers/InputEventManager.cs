//———————————————————————–
// <copyright file=”InputEventManager.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using System.Collections;
using UnityEngine;
using Common.Extensions;

public class InputEventManager : MonoBehaviour
{
    public delegate void AxisEvent(float axisValue);

    public delegate void PressEvent();

    public static event AxisEvent vertHeld;
    public static event PressEvent upPressed;
    public static event PressEvent downPressed;
    public static event AxisEvent horHeld;
    public static event PressEvent leftPressed;
    public static event PressEvent rightPressed;
    public static event PressEvent selectPressed;
    public static event PressEvent backPressed;


    const string horizontalAxis = "Horizontal";
    const string verticalAxis = "Vertical";
    const string selectAxis = "Select";

    float vertState;
    float horState;
    float selectState;

    void OnEnable()
    {
        StartCoroutine(InputListener());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// Listens To The Input
    /// </summary>
    /// <returns>The listener.</returns>
    IEnumerator InputListener()
    {
        for (;;)
        {
            // Update Input States
            horState = Input.GetAxis(horizontalAxis);
            vertState = Input.GetAxis(verticalAxis);
            selectState = Input.GetAxis(selectAxis);

            // Wait For The Next Frame
            yield return null;

            // Call The Necessary Events
            ProcessInputEventAction(vertState, verticalAxis, vertHeld, upPressed, downPressed);
            ProcessInputEventAction(horState, horizontalAxis, horHeld, rightPressed, leftPressed);
            ProcessInputEventAction(selectState, selectAxis, null, selectPressed, backPressed);
        }
    }

    /// <summary>
    /// Processes the input event action.
    /// </summary>
    /// <param name="origState">Original state.</param>
    /// <param name="axisName">Axis name.</param>
    /// <param name="held">Held.</param>
    /// <param name="posPressed">Positive pressed.</param>
    /// <param name="negPressed">Negative pressed.</param>
    static void ProcessInputEventAction(float origState, string axisName,
                                        AxisEvent held, PressEvent posPressed,
                                        PressEvent negPressed)
    {
        if (held != null)
        {
            held(Input.GetAxis(axisName));
        }

        if (origState.IsNear(Input.GetAxis(axisName)))
        {
            return;
        }
            
        if (origState.IsNear(1.0f) && posPressed != null)
        {
            posPressed();
        }
        else if (origState.IsNear(-1.0f) && negPressed != null)
        {
            negPressed();
        }
    }
}
