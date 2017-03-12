using UnityEngine;
using System.Collections.Generic;

public class TriggerListenerManager : MonoBehaviour, IUpdateManager<TriggerListener>
{
    public static TriggerListenerManager Instance { get; private set; }

    readonly List<TriggerListener> managedScripts = new List<TriggerListener>();

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

    public void Register(TriggerListener script)
    {
        managedScripts.Add(script);
    }

    public void Unregister(TriggerListener script)
    {
        managedScripts.Remove(script);
    }
}
