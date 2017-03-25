using UnityEngine;
using System.Collections.Generic;

public class OscillatorCalculatorManager : MonoBehaviour,
IUpdateManager<OscillatorCalculator>
{
    public static OscillatorCalculatorManager Instance{ get; private set; }

    readonly List<OscillatorCalculator> managedScripts = new List<OscillatorCalculator>();

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

    public void Register(OscillatorCalculator script)
    {
        managedScripts.Add(script);
    }

    public void Unregister(OscillatorCalculator script)
    {
        managedScripts.Remove(script);
    }
}

