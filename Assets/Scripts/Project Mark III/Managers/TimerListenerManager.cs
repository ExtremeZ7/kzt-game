using UnityEngine;
using System.Collections.Generic;

namespace Managers
{
    public class TimerListenerManager : MonoBehaviour, IUpdateManager<TimerListener>
    {
        public static TimerListenerManager Instance{ get; private set; }

        readonly List<TimerListener> managedScripts = new List<TimerListener>();

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

        public void Register(TimerListener script)
        {
            managedScripts.Add(script);
        }

        public void Unregister(TimerListener script)
        {
            managedScripts.Remove(script);
        }
    }
}