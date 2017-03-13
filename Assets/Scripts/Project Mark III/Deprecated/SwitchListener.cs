/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The S type parameter is the type of switch to listen to
public class SwitchListener<S> : MonoBehaviour
{
    //enums
    public enum ListenToActivation
    {
        Default,
        FirstFrameOnly}
    ;

    //fields
    [Header("Listener")]
    #if UNITY_EDITOR
    [DisplayScriptName]
    #endif
    [Tooltip("The switches to listen to")]
    public S[] switches;

    [Space(10)]
    [Tooltip("Default: Listen to any activation\n First Frame Only: Listen "
        + "only to the first frame of activation")]
    public ListenToActivation listenToActivation;

    #if UNITY_EDITOR
    [ToggleLeft]
    #endif
    [Tooltip("Toggle this to also listen to the first frame of deactivation")]
    public bool listenToFirstFrameOnDeactivate;

    [Tooltip("This will reverse what the listener will tell the derived class"
        + " about the switch (i.e. On becomes Off, Off becomes On)")]
    #if UNITY_EDITOR
    [ToggleLeft]
    #endif
    public bool flipActivation;

    //properties
    protected bool IsPaused { get; set; }

    protected Coroutine ListenerCoroutine { get; set; }

    /// <summary>This is normally called by the derived class on its OnValidate() MonoBehavior method</summary>
    ///
    protected void ValidateListener()
    {
        if (switches.Length <= 0)
        {
            switches = new S[1];
        }

    }
}*/