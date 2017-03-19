using UnityEngine;
using System.ComponentModel;

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

        iTween.RotateBy(gameObject,
            iTween.Hash(
                "amount", amount,
                "space", space,
                useValueAs.GetDescription(), value,
                "delay", delay,
                "looptype", loopType,
                "easetype", easeType,
                "ignoretimescale", ignoreTimeScale
            ));
    }
}
