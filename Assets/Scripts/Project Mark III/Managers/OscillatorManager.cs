using UnityEngine;
using System.Collections.Generic;

public class OscillatorManager : MonoBehaviour, IUpdateManager<Oscillator>
{
    public static OscillatorManager Instance{ get; private set; }

    readonly List<Oscillator> managedScripts = new List<Oscillator>();

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

    public void Register(Oscillator script)
    {
        managedScripts.Add(script);
    }

    public void Unregister(Oscillator script)
    {
        managedScripts.Remove(script);
    }
}
