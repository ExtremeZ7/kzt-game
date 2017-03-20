//———————————————————————–
// <copyright file=”iTweenRotateByOnTrigger.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using UnityEngine;
using System.ComponentModel;
using System.Collections;

public class iTweenRotateByOnTrigger : TriggerListener
{
    public enum ValueMode
    {
        [Description("time")]
        Time,

        [Description("speed")]
        Speed
    }

    [Header("iTween Parameters")]
    public Vector3 amount;

    [Space(10)]
    public float delay;
    public float value;
    public ValueMode useValueAs;

    [Space(10)]
    public Space space;
    public bool ignoreTimeScale;
    public bool resetOnStart = true;

    [Space(10)]
    public iTween.LoopType loopType;
    public iTween.EaseType easeType;

    float originalZ;
    bool runningAnimation;

    void Start()
    {
        if (resetOnStart)
        {
            originalZ = transform.localEulerAngles.z;
        }
    }

    public override void ManagedUpdate()
    {
        if (!Listener.IsActivated)
        {
            return;
        }

        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation()
    {
        if (resetOnStart && runningAnimation)
        {
            iTween.StopByName(gameObject, GetInstanceID().ToString());
            transform.localEulerAngles = new Vector3(0f, 0f, originalZ);
            yield return null;
        }

        runningAnimation = true;

        iTween.RotateBy(gameObject,
            iTween.Hash(
                "name", GetInstanceID().ToString(),
                "amount", amount,
                "space", space,
                useValueAs.GetDescription(), value,
                "delay", delay,
                "looptype", loopType,
                "easetype", easeType,
                "ignoretimescale", ignoreTimeScale,
                "oncomplete", "AlertStopped",
                "oncompletetarget", gameObject
            ));
    }

    void AlertStopped()
    {
        runningAnimation = false;
    }
}
