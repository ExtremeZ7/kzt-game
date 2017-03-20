//———————————————————————–
// <copyright file=”iShakePositionOnTrigger.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using UnityEngine;
using System.Collections;

public class iTweenShakePositionOnTrigger : TriggerListener
{
    [Header("iTween Parameters")]
    public Vector3 amount;

    [Space(10)]
    public float delay;
    public float time;

    [Space(10)]
    public bool isLocal;
    public bool orientToPath;
    public bool ignoreTimeScale;
    public bool resetOnStart = true;

    [Space(10)]
    public iTween.LoopType loopType;
    public iTween.EaseType easeType;

    Vector3 originalPos;
    bool runningAnimation;

    void Start()
    {
        originalPos = transform.localPosition;
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
            transform.localPosition = originalPos;
            yield return null;
        }

        runningAnimation = true;

        iTween.ShakePosition(gameObject,
            iTween.Hash(
                "name", GetInstanceID().ToString(),
                "amount", amount,
                "islocal", isLocal,
                "orienttopath", orientToPath,
                "time", time,
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
