//———————————————————————–
// <copyright file=”TimerListener.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–
using System.Collections;
using Managers;
using UnityEngine;
using System;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using CustomPropertyDrawers;
#endif

public class TimerListener : MonoBehaviour
{
    //enums
    public enum ListeningMode
    {
        Default,
        FirstFrameOnly}
    ;

    //fields
    [Header("Timer Listener")]
	#if UNITY_EDITOR
    [DisplayScriptName]
    #endif
    [Tooltip("The Timer Switches to listen to")]
    public TimerSwitch[] switches;

    [Space(10)]
    [Tooltip("Default: Listen to any activation\n First Frame Only: Listen "
        + "only to the first frame of activation")]
    [FormerlySerializedAs("listeningMode")]
    public ListeningMode listeningMode;

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
    ListenerRunner listener;

    //properties
    protected bool IsPaused { get; set; }

    protected Coroutine ListenerCoroutine { get; set; }

    protected bool Listening
    {
        get{ return listener != null && !IsPaused; }
    }

    protected ListenerRunner Listener
    {
        get
        {
            //If the listener has not been initialized, create a new instance
            if (listener == null)
            {
                listener = new ListenerRunner(this);
            }
            return listener;
        }
    }

    //methods
    void OnEnable()
    {
        TimerListenerManager.Instance.Register(this);
    }

    void OnDisable()
    {
        TimerListenerManager.Instance.Unregister(this);

        // I have to remove the instance of the listener here because disabling
        // the gameobject normally stops all coroutines. Because the listener
        // starts a coroutine in its constructor, a new instance needs to be 
        // created when the Listener property is called again.
        listener = null;
    }

    public virtual void ManagedUpdate()
    {
    }

    /// <summary>
    /// This is normally called by the derived class on its OnValidate() MonoBehavior method
    /// </summary>
    ///
    protected void ValidateListener()
    {
        if (switches.Length <= 0)
        {
            switches = new TimerSwitch[1];
        }
    }

    //nested types
    protected class ListenerRunner
    {
        //fields
        bool active;
        bool activatedOnCurrentFrame;
        bool deactivatedOnCurrentFrame;
        readonly TimerListener listener;
        float pauseTime;

        //constructor
        public ListenerRunner(TimerListener listener)
        {
            this.listener = listener;

            //Starts the coroutine that listens to the switches
            listener.ListenerCoroutine = (listener.StartCoroutine(CheckSwitches()));
        }

        //properties
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
            get{ return (active || activatedOnCurrentFrame) != listener.flipActivation; }
        }

        /// <summary>Stops the running listener coroutine then drops the listener instance</summary>
        ///
        public void Stop()
        { 
            listener.StopCoroutine(listener.ListenerCoroutine);
            listener.listener = null;
        }

        /// <summary>Pauses the listener instance for a certain number of seconds</summary>
        ///
        public void Pause(float seconds)
        {
            if (seconds <= 0f)
            {
                throw new ArgumentException("Seconds should be greater than 0!");
            }
            pauseTime = seconds;
        }

        /// <summary>Sets the bits that are set during the first frame of activation or deactivation</summary>
        /// <param name="active">Tells which bit to set</param>
        /// 
        void SetSingleFrameBits(bool active = true)
        {
            if (active)
            {
                activatedOnCurrentFrame = true;
            }
            else
            {
                deactivatedOnCurrentFrame = true;
            }
        }

        /// <summary>The coroutine that listens to the switches</summary>
        ///
        IEnumerator CheckSwitches()
        {
            for (;;)
            {
                for (int i = 0; i < listener.switches.Length; i++)
                {
                    TimerSwitch timerSwitch = listener.switches[i];

                    //Skip the elements that have not been set
                    if (timerSwitch == null)
                    {
                        continue;
                    }

                    //Ask if the listener has been asked to listen any activation
                    if (listener.listeningMode == ListeningMode.Default)
                    {
                        active |= timerSwitch.IsActivated;
                    }

                    //Ask if the listener has been asked to the first frame of activation
                    if (listener.listeningMode == ListeningMode.FirstFrameOnly)
                    {
                        if (timerSwitch.ActivatedOnCurrentFrame)
                        {
                            SetSingleFrameBits(!listener.flipActivation);
                        }
                    }

                    //Ask if the listener has been asked to the first frame of deactivation
                    if (listener.listenToFirstFrameOnDeactivate)
                    {
                        if (timerSwitch.DeactivatedOnCurrentFrame)
                        {
                            SetSingleFrameBits(listener.flipActivation);
                        }
                    }
                }

                //Pause time happens here
                if (pauseTime > 0f)
                {
                    listener.IsPaused = true;
                    yield return new WaitForSeconds(pauseTime);
                    listener.IsPaused = false;
                    pauseTime = 0f;
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }

                //Reset all the bits before restarting the loop
                active = false;
                activatedOnCurrentFrame = false;
                deactivatedOnCurrentFrame = false;
            }
        }
    }
}
