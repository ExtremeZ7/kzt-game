//———————————————————————–
// <copyright file=”d_iTweenInit.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–
using UnityEngine;

namespace Common.Dynamic
{
    public class iTweenInit : MonoBehaviour
    {
        void Awake()
        {
            iTween.Init(gameObject);
            Destroy(this);
        }
    }
}