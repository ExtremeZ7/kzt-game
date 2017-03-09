using UnityEngine;
using AssemblyCSharp;

public class assignNewParent : MonoBehaviour
{

    public string optionalParentTag;
    public Transform parent;

    [Space(10)]
    public float delay;

    void Start()
    {
        if (optionalParentTag != "")
            parent = GameObject.FindGameObjectWithTag(optionalParentTag).transform;
    }

    void Update()
    {
        if (Help.UseAsTimer(ref delay))
        {
            transform.parent = parent;
            enabled = false;
        }
    }
}
