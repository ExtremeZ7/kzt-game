//———————————————————————–
// <copyright file=”CreateObjectsOnTrigger.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using CustomPropertyDrawers;
#endif

public class CreateObjectsOnTrigger : TriggerListener
{
    //fields
    [Header("Main Parameters")]
    [ContextMenuItem("Copy To OnComplete List", "CopyTriggerToComplete")]
    public List<GameObject> objectsToCreateOnTrigger;
    public List<GameObject> extraObjectsToCreateOnComplete;

    [Tooltip("If this is enabled, the gameobjects on the main (OnTrigger)"
        + "GameObject list will not spawn on completion")]
    #if UNITY_EDITOR
    [ToggleLeft]
    #endif
    public bool onlyCreateExtrasOnComplete;

    [Space(10)]
    [Tooltip("This will manipulate the GameObjects after creation.\n"
        + "(Most of the fields are optional)")]
    public List<GameObjectConfig> objectConfigs;

    [Tooltip("These will create delays from the time the trigger was heard"
        + " to the time the GameObject will be initialized")]
    public List<float> createDelays;

    [Header("Main Behavior")]
    [Tooltip("This is the amount of times a trigger will be heard before"
        + " the completion event is triggered")]
    
    public float countsBeforeCompletion = Mathf.Infinity;

    [Tooltip("On a completion event, the behavior will wait for all"
        + " gameobjects to be created first before running the action on "
        + "self")]
    public ActionOnComplete selfActionOnComplete;

    Stack<Coroutine> coroutines = new Stack<Coroutine>();

    //properties
    List<GameObject> ObjectsOnTrigger
    {
        get { return objectsToCreateOnTrigger; }
    }

    List<GameObject> ObjectsOnComplete
    {
        get { return extraObjectsToCreateOnComplete; }
    }

    //methods
    public override void ManagedUpdate()
    {
        //Checks if the completion event is ready
        if (countsBeforeCompletion.IsNearZero())
        {
            if (Listening)
            {
                //For performance reasons, the listener is stopped during the
                //completion event. Wooo Economics!
                Listener.Stop();
            }

            //If the Self Action is set to None, don't bother even starting the coroutine
            if (selfActionOnComplete != ActionOnComplete.Nothing)
            {
                StartCoroutine(DoSelfActionAfterAllObjectsCreated());
            }
            return;
        }

        //If the listener is not activated, better wait for the next frame
        if (!Listener.IsActivated)
        {
            return;
        }

        //That's one down, unless the count was set to be infinity
        countsBeforeCompletion -= 1;

        //Note that it loops through both the trigger objects and completion event
        //objects in one control statement. Efficiency!
        var i = 0;
        while (i < ObjectsOnTrigger.Count + ObjectsOnComplete.Count)
        {
            GameObject nextObject;
            if (i < ObjectsOnTrigger.Count)
            {
                //Prevents objects on the Trigger list from being created on
                //the completion event if asked not to
                if (countsBeforeCompletion.IsNearZero())
                {
                    if (onlyCreateExtrasOnComplete)
                    {
                        continue;
                    }
                }
                nextObject = ObjectsOnTrigger[i];
            }
            else
            {
                //Prevents extras from being created if it is not yet the
                //completion event
                if (countsBeforeCompletion > 0)
                {
                    break;
                }
                nextObject = ObjectsOnComplete[i - ObjectsOnTrigger.Count];
            }
            //Next Object doesn't exist? Next!
            if (nextObject == null)
            {
                continue;
            }

            //Starts the coroutine that will create the object
            //Also pushes the coroutine unto a stack for reasons explained below
            coroutines.Push(StartCoroutine(
                    CreateObjectAfterDelay(createDelays[i], nextObject, i)));
            i++;
        }
    }

    /// <summary>A coroutine that creates an object after a delay time</summary>
    /// <param name="delay">The delay time (in seconds)</param>
    /// <param name="obj">The object to create</param>
    /// <param name="i">The index of the object configuration</param>
    ///
    IEnumerator CreateObjectAfterDelay(float delay, GameObject obj, int i)
    {
        if (delay > 0f)
        {
            //Waiting happens here
            yield return new WaitForSeconds(delay);
        }

        //Instantiate creates the object with default parameters then 
        //Configure() changes the appropriate attributes
        objectConfigs[i].Configure(Instantiate<GameObject>(
                obj, transform.position, Quaternion.Euler(obj.transform.localEulerAngles)));

        //If the stack is empty, wait until there something in the stack to 
        //pop before continuing
        while (coroutines.Count == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        coroutines.Pop();
    }

    /// <summary>A coroutine that does the Self Action when all objects have been created</summary>
    ///
    IEnumerator DoSelfActionAfterAllObjectsCreated()
    {
        bool waiting = true;
        while (waiting)
        {
            //This is the main use of the coroutine stack. If the stack size is
            //not zero, that must mean that there are still some running object
            //creation coroutines
            if (coroutines.Count == 0)
            {
                waiting = false;
                switch (selfActionOnComplete)
                {
                    case ActionOnComplete.SetToInactive:
                        gameObject.SetActive(false);
                        break;

                    case ActionOnComplete.Destroy:
                        Debug.Log("Sup");
                        UnityEngine.Object.Destroy(gameObject);
                        break;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    void OnValidate()
    {
        ValidateTriggerListener();

        objectConfigs = objectConfigs.Resize(
            ObjectsOnTrigger.Count + ObjectsOnComplete.Count);

        for (int i = 0; i < ObjectsOnTrigger.Count + ObjectsOnComplete.Count; i++)
        {
            GameObject nextObject;

            nextObject = i < ObjectsOnTrigger.Count ? ObjectsOnTrigger[i]
                               : ObjectsOnComplete[i - ObjectsOnTrigger.Count];

            if (nextObject != null)
            {
                objectConfigs[i].expectedObject = nextObject;
            }
        }

        createDelays = createDelays.Resize(
            ObjectsOnTrigger.Count + ObjectsOnComplete.Count);

        for (int i = 0; i < createDelays.Count; i++)
        {
            if (createDelays[i] < 0f)
            {
                createDelays[i] = 0f;
            }
        }

        if (countsBeforeCompletion <= 0f)
        {
            countsBeforeCompletion = 1f;
        }
        countsBeforeCompletion = Mathf.Ceil(countsBeforeCompletion);

        foreach (GameObjectConfig config in objectConfigs)
        {
            if (config != null)
            {
                config.Validate();
            }
        }
    }

    void CopyTriggerToComplete()
    {
        var gameObjectsToCopy = new GameObject[ObjectsOnTrigger.Count];
        ObjectsOnTrigger.CopyTo(gameObjectsToCopy);
        extraObjectsToCreateOnComplete = gameObjectsToCopy.ToList();
    }
}