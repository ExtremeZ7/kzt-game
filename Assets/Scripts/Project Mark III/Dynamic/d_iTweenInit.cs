//———————————————————————–
// <copyright file=”d_iTweenInit.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–
using UnityEngine;

// 'd' prefix means 'dynamic' which means it automatically stops or destroys
// itself sometime after being started
public class d_iTweenInit : MonoBehaviour
{
    void Awake()
    {
        iTween.Init(gameObject);
        Destroy(this);
    }
}
