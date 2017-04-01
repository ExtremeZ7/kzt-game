using UnityEngine;
using System.Collections.Generic;

namespace Managers
{
    public class CollisionListenerManager : MonoBehaviour, IUpdateManager<CollisionListener>
    {
        public static CollisionListenerManager Instance{ get; private set; }

        readonly List<CollisionListener> managedScripts = new List<CollisionListener>();

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

        public void Register(CollisionListener script)
        {
            managedScripts.Add(script);
        }

        public void Unregister(CollisionListener script)
        {
            managedScripts.Remove(script);
        }
    }
}