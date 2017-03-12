using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnableBehavioursWithTimer : TimerListener
{
    [Header("Main Fields")]
    public List<MonoBehaviour> behaviours;

    void OnStart()
    {
        behaviours.Where(i => i != null).ToList();

        // If the behaviours list turns out to be empty, disable this behaviour
        enabled &= behaviours.Count != 0;
    }

    public override void ManagedUpdate()
    {
        foreach (MonoBehaviour behaviour in behaviours)
        {
            behaviour.enabled = Listener.IsActivated;  
        }
    }
}
