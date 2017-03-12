using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SetAnimatorBoolParamsWithTimer : TimerListener
{
    [Space(10)]
    [Header("Main Fields")]
    public Animator animator;
    public List<string> parameters;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        parameters.Where(i => i != "").ToList();
    }

    public override void ManagedUpdate()
    {
        foreach (string parameter in parameters)
        {
            animator.SetBool(parameter, Listener.IsActivated);
        }
    }
}
