using System.Collections;
using UnityEngine;

public class SetAnimatorTriggersOnRandomIntervals : SetAnimatorTriggers
{
    [Space(10)]
    public float baseDuration;
    public float variation = 1f;

    void Start()
    {
        StartCoroutine(SetTriggerAfterWait());
    }

    IEnumerator SetTriggerAfterWait()
    {
        for (;;)
        {
            yield return new WaitForSeconds(
                baseDuration.Variation(variation, true));
            SetTriggers();
        }
    }
}
