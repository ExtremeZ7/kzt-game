using UnityEngine;
using System.Collections.Generic;

public class OscillatorObjectManager : MonoBehaviour,
IUpdateManager<OscillatorObject>
{
    public static OscillatorObjectManager Instance{ get; private set; }

    readonly List<OscillatorObject> managedScripts = new List<OscillatorObject>();

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

    public void Register(OscillatorObject script)
    {
        managedScripts.Add(script);
    }

    public void Unregister(OscillatorObject script)
    {
        managedScripts.Remove(script);
    }
}

