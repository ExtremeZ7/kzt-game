/*using UnityEngine;
using System.Collections.Generic;

public class SwitchListenerManager : Singleton<SwitchListenerManager>
{
    readonly List<SwitchListener<T>> managedScripts = new List<SwitchListener<T>>();

    void Update()
    {
        for (int i = 0; i < managedScripts.Count; i++)
        {
            managedScripts[i].ManagedUpdate();
        }
    }

    public void Register(SwitchListener<T> script)
    {
        managedScripts.Add(script);
    }

    public void Unregister(SwitchListener<T> script)
    {
        managedScripts.Remove(script);
    }
}*/