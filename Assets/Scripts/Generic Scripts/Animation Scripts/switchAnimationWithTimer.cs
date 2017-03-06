using UnityEngine;
using AssemblyCSharp;

public class switchAnimationWithTimer : MonoBehaviour
{

    public string parameterString;

    [Space(10)]
    public Animator animator;
    public TimerSwitch binarySwitch;

    [Space(10)]
    public bool reverse = false;

    [Space(10)]
    public bool enableWhenSwitchedOn = true;
    public bool disableWhenSwitchedOff = true;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (binarySwitch == null)
            binarySwitch = GetComponent<TimerSwitch>();
    }

    void Update()
    {
        if ((binarySwitch.IsActivated && enableWhenSwitchedOn) || (!binarySwitch.IsActivated && disableWhenSwitchedOff))
            animator.SetBool(parameterString, binarySwitch.enabled && binarySwitch.IsActivated != reverse);
    }
}
