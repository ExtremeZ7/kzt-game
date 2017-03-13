using UnityEngine;
using System.Collections.Generic;

public class TriggerListenerManager : Singleton<TriggerListenerManager>,
    IUpdateManager<TriggerListener>
{
    readonly List<TriggerListener> managedScripts = new List<TriggerListener>();

    void Update()
    {
        for (int i = 0; i < managedScripts.Count; i++)
        {
            managedScripts[i].ManagedUpdate();
        }
    }

    public void Register(TriggerListener script)
    {
        managedScripts.Add(script);
    }

    public void Unregister(TriggerListener script)
    {
        managedScripts.Remove(script);
    }
}
