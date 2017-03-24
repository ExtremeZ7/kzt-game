using System.Collections.Generic;
using UnityEngine;

public class OscillatorManager : MonoBehaviour
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

    void OnValidate()
    {
        for (int i = 0; i < managedScripts.Count; i++)
        {
            managedScripts[i].ValidateKeys();
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
