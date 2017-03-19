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

    public void EnableAlliTweenBehaviours()
    {
        iTween[] behaviours = GetComponents<iTween>();
        for (int i = 0; i < behaviours.Length; i++)
        {
            behaviours[i].enabled = true;
        }
    }

    public void DisableAlliTweenBehaviours()
    {
        iTween[] behaviours = GetComponents<iTween>();
        for (int i = 0; i < behaviours.Length; i++)
        {
            behaviours[i].enabled = false;
        }
    }
}