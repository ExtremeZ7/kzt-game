//———————————————————————–
// <copyright file=”AnimationEventHandler.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    public bool executeWhileInTransition = true;

    public void ChangeAnimatorSpeed(int speed)
    {
        GetComponent<Animator>().speed = speed;
    }
}