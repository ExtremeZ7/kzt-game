//———————————————————————–
// <copyright file=”GameObjectConfig.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–
using UnityEngine;
using System;

//enums
public enum ActionOnComplete
{
    Nothing,
    SetToInactive,
    Destroy}
;

public enum AttributeConfig
{
    None,
    OffsetWithCustom,
    CopyNewTransformThenOffsetWithCustom,
    UseCustom}
;

[Serializable]
public class GameObjectConfig : System.Object
{
    //fields
    [Tooltip("This won't be used, this is just shown on the editor to "
        + "prevent mistakes. However, it may be used for debugging.")]
    public GameObject expectedObject;
    
    [Header("Configuration")]
    [Tooltip("Optional. Leave this empty if you don't want to give the "
        + "object a new name")]
    public string newName;
    public Transform newTransform;
    public Transform newParent;
    public CustomScale customScale;
    public CustomRotation customRotation;

    [Header("Other Behavior(s)")]
    [Tooltip("What happens to the new Transform after the object is"
        + " configured?")]
    public ActionOnComplete transformOnCreate;

    /// <summary>Configures a given GameObject based on the configuration fields</summary>
    /// <param name="obj">The object to configure</param>
    ///
    public void Configure(GameObject obj)
    {
        //Keep the name if no new one was given
        if (newName != "")
        {
            obj.name = newName;
        }

        //Keep the position if no new Transform was given
        if (newTransform != null)
        {
            obj.transform.position = newTransform.position;
        }

        //Configure the rotation
        float customR = customRotation.custom;
        switch (customRotation.rotationConfig)
        {
            case AttributeConfig.None:
                obj.transform.rotation = Quaternion.identity;
                break;
            case AttributeConfig.OffsetWithCustom:
                obj.transform.localEulerAngles = new Vector3(
                    0, 0, obj.transform.localEulerAngles.z + customR);
                break;

            case AttributeConfig.CopyNewTransformThenOffsetWithCustom:
                obj.transform.localEulerAngles = new Vector3(
                    0, 0, newTransform.localEulerAngles.z + customR);
                break;

            case AttributeConfig.UseCustom:
                obj.transform.localEulerAngles = new Vector3(
                    0, 0, customR);
                break;
        }

        //Configure the scale
        Vector2 customS = customScale.custom;
        switch (customScale.scaleConfig)
        {
            case AttributeConfig.OffsetWithCustom:
                {
                    Vector2 orig = obj.transform.localScale;
                    obj.transform.localScale = new Vector3(
                        orig.x * customS.x, orig.y * customS.y, 1f);
                }
                break;

            case AttributeConfig.CopyNewTransformThenOffsetWithCustom:
                Vector2 newScale = newTransform.localScale;
                obj.transform.localScale = new Vector3(
                    newScale.x * customS.x, newScale.y * customS.y, 1f);
                break;

            case AttributeConfig.UseCustom:
                obj.transform.localScale = new Vector3(
                    customS.x, customS.y, 1f);
                break;
        }

        if (newParent == null)
        {
            newParent = GameObject.FindGameObjectWithTag(
                Tags.DynamicObjects.GetDescription()).transform;
        }

        obj.transform.parent = newParent;

        if (newTransform == null)
        {
            return;
        }

        //Perform the new transform action
        switch (transformOnCreate)
        {
            case ActionOnComplete.SetToInactive:
                newTransform.gameObject.SetActive(false);
                break;

            case ActionOnComplete.Destroy:
                UnityEngine.Object.Destroy(newTransform.gameObject);
                break;
        }
    }

    public void Validate()
    {
        if (newTransform == null)
        {
            transformOnCreate = ActionOnComplete.Nothing;

            switch (customScale.scaleConfig)
            {
                case AttributeConfig.CopyNewTransformThenOffsetWithCustom:
                    customScale.scaleConfig = AttributeConfig.OffsetWithCustom;
                    break;
            }

            switch (customRotation.rotationConfig)
            {
                case AttributeConfig.CopyNewTransformThenOffsetWithCustom:
                    customRotation.rotationConfig = AttributeConfig.OffsetWithCustom;
                    break;
            }
        }
    }

    [Serializable]
    public class CustomScale : System.Object
    {
        [Tooltip("None: Does Nothing\n\n" +
            "Offset With Custom: Multiplies the original scale with the custom"
            + " scale\n\n Copy New Transform Then Offset With Custom: Copies "
            + "the scale of the new Transform then multiplies it with the"
            + "custom \n\n Use Custom: Just outright overwrites the scale with "
            + "the custom")]
        public AttributeConfig scaleConfig;
        public Vector2 custom;
    }

    [Serializable]
    public class CustomRotation : System.Object
    {
        [Tooltip("None: Does Nothing\n\n" +
            "Offset With Custom: Adds the original rotation with the custom "
            + "rotation\n\n Copy New Transform Then Offset With Custom: Copies"
            + "the rotation of the new Transform then adds it with the custom"
            + "\n\n Use Custom: Just outright overwrites the rotation with "
            + "the custom")]
        public AttributeConfig rotationConfig;
        [Range(0f, 360f)]
        public float custom;
    }
}