using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnimatorTriggers : MonoBehaviour
{
    public string[] parameters;

    protected void SetTriggers()
    {
        for (int i = 0; i < parameters.Length; i++)
        {
            GetComponent<Animator>().SetTrigger(parameters[i]);
        }
    }
}
