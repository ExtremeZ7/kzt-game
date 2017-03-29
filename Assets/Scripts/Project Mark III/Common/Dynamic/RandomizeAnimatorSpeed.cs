//———————————————————————–
// <copyright file=”d_RandomizeAnimatorSpeed.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–
using UnityEngine;
using Common.Extensions;

namespace Common.Dynamic
{
    [RequireComponent(typeof(Animator))]
    public class RandomizeAnimatorSpeed : MonoBehaviour
    {

        public float baseSpeed = 1f;
        public float variation;

        void Awake()
        {
            GetComponent<Animator>().speed = baseSpeed.Variation(variation);
            Destroy(this);
        }
    }
}