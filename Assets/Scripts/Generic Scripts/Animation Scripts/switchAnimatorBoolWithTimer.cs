using UnityEngine;
using AssemblyCSharp;

public class switchAnimatorBoolWithTimer : MonoBehaviour
{

    public Animator animator;
    public TimerSwitch binarySwitch;

    [Space(10)]
    public string boolParameterName;

    public bool reverse;

    [Space(10)]
    public bool switchWhenSwitchedOn = true;
    public bool switchWhenSwitchedOff = true;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (binarySwitch == null)
            binarySwitch = GetComponent<TimerSwitch>();
    }

    void Update()
    {
        if (binarySwitch != null)
        {
            if ((binarySwitch.ActivatedOnCurrentFrame && switchWhenSwitchedOn) || (binarySwitch.DeactivatedOnCurrentFrame && switchWhenSwitchedOff))
                animator.SetBool(boolParameterName, binarySwitch.IsActivated != reverse);
        }
        else
        {
            binarySwitch = GetComponent<TimerSwitch>();
        }
    }
}
