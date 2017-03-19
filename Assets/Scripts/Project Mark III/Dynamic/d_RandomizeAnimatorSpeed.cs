//———————————————————————–
// <copyright file=”d_RandomizeAnimatorSpeed.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–
using UnityEngine;

// 'd' prefix means 'dynamic' which means it automatically stops or destroys
// itself sometime after being started
[RequireComponent(typeof(Animator))]
public class d_RandomizeAnimatorSpeed : MonoBehaviour
{

    public float baseSpeed = 1f;
    public float variation;

    void Awake()
    {
        GetComponent<Animator>().speed = baseSpeed.Variation(variation);
        Destroy(this);
    }
}
