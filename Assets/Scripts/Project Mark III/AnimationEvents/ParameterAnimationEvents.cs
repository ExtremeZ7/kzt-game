//———————————————————————–
// <copyright file=”ParameterAnimationEvents.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using UnityEngine;

public class ParameterAnimationEvents : AnimationEventHandler
{

    public void SetBoolParameterToTrue(string parameter)
    {
        GetComponent<Animator>().SetBool(parameter, true);
    }

    public void SetBoolParameterToFalse(string parameter)
    {
        GetComponent<Animator>().SetBool(parameter, true);
    }

    public void FlipBoolParameter(string parameter)
    {
        GetComponent<Animator>().SetBool(parameter,
            !GetComponent<Animator>().GetBool(parameter));
    }

    public void SetTriggerParameter(string parameter)
    {
        GetComponent<Animator>().SetTrigger(parameter);
    }
}
