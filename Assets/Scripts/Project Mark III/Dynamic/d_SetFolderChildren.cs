//———————————————————————–
// <copyright file=”d_InitializeFolderChildren.cs” game="KzzzZZZzzT!">
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
public class d_SetFolderChildren : MonoBehaviour
{
    public GameObject prefab;

    /// <summary>
    /// Create a new gameObject as a child if this transform has less than 2
    /// children
    /// </summary>
    void Awake()
    {
        if (prefab == null)
        {
            return;
        }

        if (transform.childCount > 1)
        {
            return;
        }

        // Destroy all children
        foreach (Transform child in transform.GetAllChildren())
        {
            DestroyImmediate(child.gameObject);
        }

        // Instantiate the Object as child
        GameObject newObject = Instantiate<GameObject>(
                                   prefab, transform, false);

        // Link them to their respective prefabs
        PrefabUtility.ConnectGameObjectToPrefab(newObject, prefab);

        DestroyImmediate(this);
    }

    void Update()
    {
        Awake();
    }
}
