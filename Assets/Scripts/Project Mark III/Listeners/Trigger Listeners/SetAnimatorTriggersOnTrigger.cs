using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Animator))]
public class SetAnimatorTriggersOnTrigger : TriggerListener
{
    [Header("Main Fields")]
    public string[] parameters;

    void Start()
    {
        parameters = parameters.Where(i => i != "").ToArray();
    }

    public override void ManagedUpdate()
    {
        if (!Listener.IsActivated)
        {
            return;
        }

        for (int i = 0; i < parameters.Length; i++)
        {
            GetComponent<Animator>().SetTrigger(parameters[i]);
        }
    }
}