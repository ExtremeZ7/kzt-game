using UnityEngine;
using System.Collections.Generic;

public class PathObjectManager : MonoBehaviour, IUpdateManager<PathObject>
{
    public static PathObjectManager Instance{ get; private set; }

    readonly List<PathObject> managedScripts = new List<PathObject>();

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        for (int i = 0; i < managedScripts.Count; i++)
        {
            managedScripts[i].ManagedUpdate();
        }
    }

    public void Register(PathObject script)
    {
        managedScripts.Add(script);
    }

    public void Unregister(PathObject script)
    {
        managedScripts.Remove(script);
    }
}
