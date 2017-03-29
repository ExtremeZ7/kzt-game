//———————————————————————–
// <copyright file=”d_DestroyAllChildren.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using UnityEngine;
using Common.Extensions;

namespace Common.Dynamic
{
    [ExecuteInEditMode]
    public class DestroyAllChildren : MonoBehaviour
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
}