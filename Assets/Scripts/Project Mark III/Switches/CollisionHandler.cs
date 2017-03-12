//———————————————————————–
// <copyright file=”CollisionHandler.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using CustomPropertyDrawers;
#endif

public class CollisionHandler : MonoBehaviour
{
    //enums
    public enum ActivationMode
    {
        Default,
        OnlyActivateOnce,
        FlipActivation}
    ;

    //fields
    [Tooltip("This is an optional attribute used for easier seeking from Listeners")]
    [FormerlySerializedAs("switchName")]
    public string scriptName;

    [Space(10)]
    public List<Tag> tagFilter;

    [Space(10)]
    [Tooltip("Defualt: Normal activation on trigger\n"
        + "Only Activate Once: Prevents from being triggered more than once\n"
        + "Flip Activation: Flip active bit when triggered")]
    public ActivationMode activationMode;

    [Space(10)]
    [Tooltip("When enabled, activation will last only for one frame!")]
    #if UNITY_EDITOR
    [ToggleLeft]
    #endif
    public bool deactivateAfterOneFrame;
    [Tooltip("If set, the trigger will only activate if and only if other "
        + "triggers in the scene that also have exclusive activation have not "
        + "been activated on the same frame. Triggers with this bit not set "
        + "will not be affected. (This is mainly used in the crate triggers so "
        + "that only one crate is destroyed per frame)")]
    #if UNITY_EDITOR
    [ToggleLeft]
    #endif
    public bool exclusiveActivation;
    [Tooltip("This enables the 'OnStay2D' trigger. Disabled by default.")]
    #if UNITY_EDITOR
    [ToggleLeft]
    #endif
    public bool activateOnStay;
    [Tooltip("This enables the 'OnExit2D' trigger. Enabled by default.")]
    #if UNITY_EDITOR
    [ToggleLeft]
    #endif
    public bool deactivateOnExit = true;

    bool triggeredOnce;
    GameObject triggerObj;
    bool active;
    bool activatedOnCurrentFrame;
    bool deactivatedOnCurrentFrame;

    //static fields
    static bool exclusiveSwitch;

    //properties
    public GameObject TriggeringObject
    {
        get{ return triggerObj; }
    }

    protected List<Tag> TagFilter
    {
        get{ return tagFilter; }
    }

    public bool ActivatedOnCurrentFrame
    {
        get{ return activatedOnCurrentFrame; }
    }

    public bool DeactivatedOnCurrentFrame
    {
        get{ return deactivatedOnCurrentFrame; }
    }

    public bool IsActivated
    {
        get{ return active || activatedOnCurrentFrame; }
    }

    //methods
    /// <summary>
    /// Called by a derived class whenever its trigger is called
    /// </summary>
    /// <param name="Obj">The object that caused the trigger</param>
    ///
    protected void Trigger(GameObject Obj)
    {
        //Return if the object tag is not in the filter
        if (!TagFilter.Contains(Obj.tag))
        {
            return;
        }

        //Return if exclusive activation and exclusive bit has been set
        if (exclusiveActivation && exclusiveSwitch)
        {
            return;
        }

        triggerObj = Obj;
        switch (activationMode)
        {
            case ActivationMode.Default:
                {
                    SetActiveState();
                    exclusiveSwitch |= exclusiveActivation;
                }
                break;

            case ActivationMode.OnlyActivateOnce:
                {
                    if (triggeredOnce)
                    {
                        break;
                    }
                    triggeredOnce = true;
                    goto case (int)ActivationMode.Default;
                }

            case ActivationMode.FlipActivation:
                SetActiveState(!active);
                break;
        }
    }

    /// <summary>
    /// Usually called by a derived class whenever its trigger is exited
    /// </summary>
    /// <param name="Obj">The object that caused the trigger</param>
    ///
    protected void ExitTrigger(GameObject Obj)
    {
        //Return if this is not the same object that caused the latest trigger
        if (Obj != triggerObj)
        {
            return;
        }

        //Return if the 'deactivateOnExit' bit is not set
        if (!deactivateOnExit)
        {
            return;
        }

        //Return if exclusive activation and exclusive bit has been set
        if (exclusiveActivation && exclusiveSwitch)
        {
            return;
        }

        switch (activationMode)
        {
            case ActivationMode.Default:
            case ActivationMode.OnlyActivateOnce:
                SetActiveState(false);
                break;
        }
        triggerObj = null;
    }

    /// <summary>Sets the active bit then starts other operations</summary>
    /// <param name="active">Whether or not the active bit should be set</param>
    ///
    protected void SetActiveState(bool active = true)
    {
        //Start the below coroutine only if the active bit was flipped
        if (this.active != active)
        {
            StartCoroutine(SetSingleFrameBits(active));
        }

        this.active = active;

        //Start the below coroutine only if the 'deactivateAfterOneFrame' bit is
        //set and the active bit is set
        if (deactivateAfterOneFrame && this.active)
        {
            StartCoroutine(DeactivateWhenFrameEnds());
        }
    }

    /// <summary>A coroutine that sets some bits to true then waits for the end of the frame before resetting them</summary>
    /// <param name="active">Tells which bit to set</param>
    ///
    IEnumerator SetSingleFrameBits(bool active = true)
    {
        if (active)
        {
            activatedOnCurrentFrame = true;
        }
        else
        {
            deactivatedOnCurrentFrame = true;
        }

        yield return new WaitForEndOfFrame();

        if (active)
        {
            activatedOnCurrentFrame = false;
        }
        else
        {
            deactivatedOnCurrentFrame = false;
        }

        //Reset the exclusive bit so that the next frame can use it again
        exclusiveSwitch = false;
    }

    /// <summary>A coroutine that resets the active bit at the end of a frame</summary>
    ///
    IEnumerator DeactivateWhenFrameEnds()
    {
        yield return new WaitForEndOfFrame();
        active = false;
    }

    void OnValidate()
    {
        scriptName = scriptName.Trim();

        if (scriptName.Length == 0)
        {
            scriptName = name + GetHashCode();
        }
    }
}