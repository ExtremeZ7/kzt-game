using UnityEngine;
using System.Collections;

public class TimerSwitch : MonoBehaviour
{
    // The name to displayed from 'DisplayScriptName' custom attribute
    public string scriptName;

    // How long (sec) will the switch stay off before the timer begins
    public float delay;

    [Space(10)]
    // This will determine the On/Off pattern
    public float[] timerPattern;

    float totalTime;
    float timer;
    float searchDistance;

    int currentIndex = -1;

    bool active;
    bool activatedOnCurrentFrame;
    bool deactivatedOnCurrentFrame;

    public bool IsActivated
    {
        get{ return active || activatedOnCurrentFrame; }
    }

    public bool ActivatedOnCurrentFrame
    {
        get{ return activatedOnCurrentFrame; }
    }

    public bool DeactivatedOnCurrentFrame
    {
        get { return deactivatedOnCurrentFrame; }
    }

    void OnValidate()
    {
        if (timerPattern.Length <= 0)
        {
            timerPattern = new float[1];
            timerPattern[0] = Mathf.Infinity;
        }
        else
        {
            for (int i = 0; i < timerPattern.Length; i++)
            {
                if (timerPattern[i] <= 0)
                {
                    timerPattern[i] = Mathf.Infinity;
                }
            }
        }
    }

    void Start()
    {
        OnValidate();
        totalTime = delay + timerPattern.SumTotal() * 2;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > totalTime)
        {
            timer -= totalTime - delay;
            currentIndex = -1;
            searchDistance = 0f;
        }

        if (timer >= delay)
        {
            while (searchDistance <= timer - delay)
            {
                SetActiveState(!active);
                searchDistance += 
                    timerPattern[(++currentIndex) % timerPattern.Length];
            }
        }
    }

    /// <summary>
    /// Resets the timer.
    /// </summary>
    public void ResetTimer()
    {
        timer = 0f;
        searchDistance = 0f;
        currentIndex = -1;
        active = false;
        activatedOnCurrentFrame = false;
        deactivatedOnCurrentFrame = false;
    }

    /// <summary>
    /// Sets the active state bit
    /// </summary>
    /// <param name="active">If set to <c>true</c> active.</param>
    void SetActiveState(bool active = true)
    {
        if (this.active != active)
        {
            StartCoroutine(SetSingleFrameSwitches(active));
        }
        this.active = active;
    }

    /// <summary>
    /// Sets some variables to true that turn off when the frame ends
    /// </summary>
    /// <returns>The single frame switches.</returns>
    /// <param name="triggerOn">If set to <c>true</c> trigger on.</param>
    IEnumerator SetSingleFrameSwitches(bool triggerOn = true)
    {
        if (triggerOn)
        {
            activatedOnCurrentFrame = true;
        }
        else
        {
            deactivatedOnCurrentFrame = true;
        }

        yield return new WaitForEndOfFrame();

        if (triggerOn)
        {
            activatedOnCurrentFrame = false;
        }
        else
        {
            deactivatedOnCurrentFrame = false;
        }
    }
}