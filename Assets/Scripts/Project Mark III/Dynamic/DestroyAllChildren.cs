using UnityEngine;

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
