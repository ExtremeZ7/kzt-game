using UnityEngine;
using AssemblyCSharp;

public class SwitchTagWithTimer : MonoBehaviour
{

    public TimerSwitch timer;
    public GameObject target;

    [Space(10)]
    public bool reverse;

    [Space(10)]
    public string activeTag;
    public string notActiveTag;

    void Start()
    {
        if (timer == null)
            timer = GetComponent<TimerSwitch>();
        if (target == null)
            target = gameObject;
    }

    void Update()
    {
        if (timer.IsActivated == !reverse)
            gameObject.tag = activeTag;
        else
            gameObject.tag = notActiveTag;
    }
}
