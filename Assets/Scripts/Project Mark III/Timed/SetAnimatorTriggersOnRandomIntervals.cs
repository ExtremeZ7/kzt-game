using System.Collections;
using UnityEngine;

public class SetAnimatorTriggersOnRandomIntervals : SetAnimatorTriggers
{
    [Space(10)]
    public float waitTime;
    [Range(0f, 5000f)]
    public float randomVariation = 1f;

    void Start()
    {
        StartCoroutine(SetTriggerAfterWait());
    }

    IEnumerator SetTriggerAfterWait()
    {
        for (;;)
        {
            yield return new WaitForSeconds(
                waitTime.Variation(randomVariation, true));
            SetTriggers();
        }
    }
}
