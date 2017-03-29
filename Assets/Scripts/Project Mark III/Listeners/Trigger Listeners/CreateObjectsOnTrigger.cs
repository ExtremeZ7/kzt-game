//———————————————————————–
// <copyright file=”CreateObjectsOnTrigger.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Common.Extensions;

#if UNITY_EDITOR
using CustomPropertyDrawers;
#endif

public class CreateObjectsOnTrigger : TriggerListener
{
    //enums
    public enum ActionOnComplete
    {
        Nothing,
        SetSelfOrParentToInactive,
        DestroySelfOrParent}
    ;

    //fields
    [Header("Main Fields")]
    public ObjectToCreate[] objectsToCreateOnTrigger;
    [Space(10)]
    public ObjectToCreate[] extraObjectsToCreateOnComplete;


    [Header("Main Behavior")]
    [Tooltip("This is the amount of times a trigger will be heard before"
        + " the completion event is triggered")]
    public float countsBeforeCompletion = Mathf.Infinity;

    #if UNITY_EDITOR
    [ToggleLeft]
    #endif
    [Tooltip("If this is enabled, the gameobjects on the main (OnTrigger)"
        + "GameObject list will not spawn on completion")]
    public bool onlyCreateExtrasOnComplete;

    [Tooltip("On a completion event, the behavior will wait for all"
        + " gameobjects to be created first before running the action on "
        + "self")]
    public SelfOnComplete selfActionOnComplete;

    GameObject objectToDestroy;
    Stack<Coroutine> coroutines = new Stack<Coroutine>();

    //properties
    ObjectToCreate[] ObjectsOnTrigger
    {
        get { return objectsToCreateOnTrigger; }
    }

    ObjectToCreate[] ObjectsOnComplete
    {
        get { return extraObjectsToCreateOnComplete; }
    }

    //methods
    void Start()
    {
        // Check if the self action is not "Nothing" then get the proper parent
        //
        if (selfActionOnComplete.actionOnComplete != ActionOnComplete.Nothing)
        {
            Transform transformToDestroy = gameObject.transform;

            int i = selfActionOnComplete.parentDepth;
            while (i > 0)
            {
                transformToDestroy = transformToDestroy.parent;
                i--;
            }

            objectToDestroy = transformToDestroy.gameObject;
        }
    }

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

            // If the Self Action is set to None, don't bother even starting the
            // coroutine
            if (selfActionOnComplete.actionOnComplete
                != ActionOnComplete.Nothing)
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
        for (int i = 0; i < ObjectsOnTrigger.Length + ObjectsOnComplete.Length
            ; i++)
        {
            ObjectToCreate nextObject;
            if (i < ObjectsOnTrigger.Length)
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
                nextObject = ObjectsOnComplete[i - ObjectsOnTrigger.Length];
            }
            //Next Object doesn't exist? Next!
            if (nextObject == null)
            {
                continue;
            }

            //Starts the coroutine that will create the object
            //Also pushes the coroutine unto a stack for reasons explained below
            coroutines.Push(StartCoroutine(
                    CreateObjectAfterDelay(nextObject)));
        }
    }

    /// <summary>
    /// Creates the object after a delay.
    /// </summary>
    /// <returns>The object after delay.</returns>
    /// <param name="objectToCreate">Object to create.</param>
    IEnumerator CreateObjectAfterDelay(ObjectToCreate objectToCreate)
    {  
        GameObject gameObject = objectToCreate.Object;
        GameObjectConfig customConfig = objectToCreate.customConfig;
        float delay = objectToCreate.Delay;

        if (objectToCreate.Delay > 0f)
        {
            //Waiting happens here
            yield return new WaitForSeconds(delay);
        }
        else if (objectToCreate.Delay < 0f)
        {
            throw new ArgumentException("Create delay should be larger than "
                + "zero!");
        }

        //Instantiate creates the object with default parameters then 
        //Configure() changes the appropriate attributes
        customConfig.Configure(Instantiate<GameObject>(
                gameObject, transform.position,
                Quaternion.Euler(transform.localEulerAngles)));

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
                switch (selfActionOnComplete.actionOnComplete)
                {
                    case ActionOnComplete.SetSelfOrParentToInactive:
                        objectToDestroy.SetActive(false);
                        break;

                    case ActionOnComplete.DestroySelfOrParent:
                        Destroy(objectToDestroy);
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
        ValidateListener();

        if (countsBeforeCompletion <= 0f)
        {
            countsBeforeCompletion = 1f;
        }
        countsBeforeCompletion = Mathf.Ceil(countsBeforeCompletion);

        foreach (ObjectToCreate objectToCreate in ObjectsOnTrigger)
        {
            objectToCreate.customConfig.Validate();
        }

        foreach (ObjectToCreate objectToCreate in ObjectsOnComplete)
        {
            objectToCreate.customConfig.Validate();
        }
    }

    //nested classes
    [Serializable]
    public class ObjectToCreate : System.Object
    {
        public DelayedObject delayedObject;
        public GameObjectConfig customConfig;

        public GameObject Object
        {
            get{ return delayedObject.gameObject; }
        }

        public float Delay
        {
            get{ return delayedObject.delay; }
        }
    }

    [Serializable]
    public class DelayedObject : System.Object
    {
        public GameObject gameObject;
        public float delay;
    }

    [Serializable]
    public class SelfOnComplete : System.Object
    {
        public ActionOnComplete actionOnComplete;
        public int parentDepth;
    }
}