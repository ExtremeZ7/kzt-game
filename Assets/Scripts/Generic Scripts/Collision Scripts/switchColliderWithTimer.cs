using UnityEngine;
using AssemblyCSharp;

public class switchColliderWithTimer : MonoBehaviour
{

    public Collider2D collider2d;
    public TimerSwitch binarySwitch;

    [Space(10)]
    public bool reverse = false;

    [Space(10)]

    public bool enableWhenSwitchedOn = true;
    public bool disableWhenSwitchedOff = true;

    void Start()
    {
        if (binarySwitch == null)
            binarySwitch = GetComponent<TimerSwitch>();
        if (collider2d == null)
            collider2d = GetComponent<Collider2D>();
    }

    void Update()
    {
        if ((binarySwitch.IsActivated && enableWhenSwitchedOn) || (!binarySwitch.IsActivated && disableWhenSwitchedOff))
            collider2d.enabled = binarySwitch.IsActivated != reverse;
    }
}
