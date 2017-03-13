using System.Collections.Generic;

public class TimerListenerManager : Singleton<TimerListenerManager>,
    IUpdateManager<TimerListener>
{
    readonly List<TimerListener> managedScripts = new List<TimerListener>();

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
