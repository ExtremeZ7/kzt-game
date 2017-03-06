using UnityEngine;
using AssemblyCSharp;

public class triggerAnimationWithTimer : MonoBehaviour
{

    public string triggerName;

    [Space(10)]
    public Animator animator;
    public TimerSwitch binarySwitch;

    [Space(10)]
    public bool triggerWhenSwitchedOn = true;
    public bool triggerWhenSwitchedOff;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (binarySwitch == null)
            binarySwitch = GetComponent<TimerSwitch>();
    }

    void Update()
    {
        if (binarySwitch.isActiveAndEnabled)
        {
            if ((binarySwitch.ActivatedOnCurrentFrame && triggerWhenSwitchedOn) || (binarySwitch.DeactivatedOnCurrentFrame && triggerWhenSwitchedOff))
                animator.SetTrigger(triggerName);
        }
    }
}
