using UnityEngine;
using System.Collections;

public class TimerSwitch : MonoBehaviour
{
    public float delay;

    [Space(10)]
    public float[] timerPattern;

    private float totalTime;
    private float timer;
    private float searchDistance;

    private int currentIndex = -1;

    private bool active;
    private bool activatedOnCurrentFrame;
    private bool deactivatedOnCurrentFrame;

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

    void Validate()
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
        Validate();
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

    //Use This Instead of Changing 'active' directly
    private void SetActiveState(bool active = true)
    {
        if (this.active != active)
        {
            StartCoroutine(SetSingleFrameSwitches(active));
        }
        this.active = active;
    }

    //Sets The Current Frame Variables To True Until The End Of Frame
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

    public void ResetTimer()
    {
        timer = 0f;
        searchDistance = 0f;
        currentIndex = -1;
        active = false;
        activatedOnCurrentFrame = false;
        deactivatedOnCurrentFrame = false;
    }
}