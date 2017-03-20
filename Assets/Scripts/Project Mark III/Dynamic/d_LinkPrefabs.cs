﻿//———————————————————————–
// <copyright file=”d_LinkPrefabs.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

// 'd' prefix means 'dynamic' which means it automatically stops or destroys
// itself sometime after being started
[ExecuteInEditMode]
public class d_LinkPrefabs : MonoBehaviour
{
    public GameObject[] prefabs = new GameObject[0];

    void Awake()
    {
        if (prefabs.Length == 0)
        {
            return;
        }

        // Destroy all children
        foreach (Transform child in transform.GetAllChildren())
        {
            DestroyImmediate(child.gameObject);
        }

        foreach (GameObject prefab in prefabs)
        {
            // Instantiate the Objects
            GameObject newObject = Instantiate<GameObject>(
                                       prefab, transform, false);
            
            // Link them to their respective prefabs
            PrefabUtility.ConnectGameObjectToPrefab(newObject, prefab);
        }

        DestroyImmediate(this);
    }

    void Update()
    {
        Awake();
    }
}