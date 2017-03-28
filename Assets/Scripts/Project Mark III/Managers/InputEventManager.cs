using System.Collections;
using UnityEngine;

public class InputEventManager : MonoBehaviour
{
    public delegate void InputEvent();

    public static event InputEvent upPressed;
    public static event InputEvent upHeld;
    public static event InputEvent downPressed;
    public static event InputEvent downHeld;

    float vertState;
    float horState;

    void OnEnable()
    {
        StartCoroutine(InputListener());
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator InputListener()
    {
        // Update Input States
        vertState = Input.GetAxis("Vertical");
        horState = Input.GetAxis("Horizontal");

        // Wait For The End Of Frame
        yield return new WaitForEndOfFrame();

        // Call The Necessary Events
        ProcessInputEventAction(vertState, "Vertical", 1.0f, upHeld, upPressed);
        ProcessInputEventAction(vertState, "Vertical", -1.0f, downHeld, downPressed);
    }

    static float ProcessInputEventAction(float origState, string axisName,
                                         float eventValue,
                                         InputEvent held, InputEvent pressed)
    {
        if (origState.IsNear(eventValue))
        {
            held();
            if (!origState.IsNear(Input.GetAxis(axisName)))
            {
                pressed();
            }
        }
        return Input.GetAxis(axisName);
    }
}
