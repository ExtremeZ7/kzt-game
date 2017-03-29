//———————————————————————–
// <copyright file=”GameObjectAnimationEvents.cs” game="KzzzZZZzzT!">
//     Copyright (c) Extreme Z7.  All rights reserved.
// </copyright>
//———————————————————————–

using UnityEngine;
using Common.Math;

public class GameObjectAnimationEvents : AnimationEventHandler
{
    [Space(10)]
    public Transform spawner;

    [Header("RigidBody Force")]
    public float rotationOffset;
    public float strength;

    public void DestroySelfOrParent(int parentDepth = 0)
    {
        Destroy(GetParent(parentDepth).gameObject);
    }

    public void SetSelfOrParentToActive(int parentDepth = 0)
    {
        GetParent(parentDepth).gameObject.SetActive(true);
    }

    public void SetSelfOrParentToInactive(int parentDepth = 0)
    {
        GetParent(parentDepth).gameObject.SetActive(false);
    }

    public void InstantiateObject(GameObject objectToCreate)
    {
        if (executeWhileInTransition || !GetComponent<Animator>().IsInTransition(0))
        {
            Instantiate(objectToCreate, transform.position,
                Quaternion.identity);
        }
    }

    public void InstantiateObjectAtSpawner(GameObject objectToCreate)
    {
        Instantiate(objectToCreate, spawner.transform.position,
            Quaternion.identity);
    }

    public void InstantiateObjectAtSpawnerAndCopyItsScaleAndRotation(
        GameObject objectToCreate)
    {
        // Create the new object
        GameObject newObject = Instantiate<GameObject>(
                                   objectToCreate,
                                   spawner.transform.position,
                                   Quaternion.identity);

        // Copy Scale and Rotation
        newObject.transform.localScale = spawner.transform.localScale;
        newObject.transform.rotation = spawner.transform.rotation;
    }

    public void InstantiateObjectAtSpawnerAndSetRigidbodyVelocity(
        GameObject objectToCreate)
    {
        GameObject newObject = Instantiate<GameObject>(
                                   objectToCreate,
                                   spawner.transform.position,
                                   Quaternion.identity);
        
        newObject.GetComponent<Rigidbody2D>().velocity = Trigo.GetRotatedVector(
            transform.rotation.eulerAngles.z + rotationOffset, strength);
    }

    Transform GetParent(int depth)
    {
        Transform target = transform;

        while (depth > 0)
        {
            target = target.parent;
            depth--;
        }

        return target;
    }
}
