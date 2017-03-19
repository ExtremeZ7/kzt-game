//———————————————————————–
// <copyright file=”d_DestroyAllChildren.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using UnityEngine;

// 'd' prefix means 'dynamic' which means it automatically stops or destroys
// itself sometime after being started
[ExecuteInEditMode]
public class d_DestroyAllChildren : MonoBehaviour
{
    void Awake()
    {
        // Destroy all children
        foreach (Transform child in transform.GetAllChildren())
        {
            DestroyImmediate(child.gameObject);
        }

        DestroyImmediate(this);
    }

    void Update()
    {
        Awake();
    }
}
