//———————————————————————–
// <copyright file=”iShakePositionOnTrigger.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using UnityEngine;

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
    public bool resetOnStart;

    [Space(10)]
    public iTween.LoopType loopType;
    public iTween.EaseType easeType;

    void Awake()
    {
        iTween.Init(gameObject);
    }

    public override void ManagedUpdate()
    {
        if (!Listener.IsActivated)
        {
            return;
        }

        if (resetOnStart)
        {
            iTween.Stop(gameObject);
        }

        iTween.ShakePosition(gameObject,
            iTween.Hash(
                "amount", amount,
                "islocal", isLocal,
                "orienttopath", orientToPath,
                "time", time,
                "delay", delay,
                "looptype", loopType,
                "easetype", easeType,
                "ignoretimescale", ignoreTimeScale
            ));
    }
}
