using UnityEngine;

public class PathObject : MonoBehaviour
{
    public Vector2Path path;

    //methods
    protected void OnEnable()
    {
        PathObjectManager.Instance.Register(this);
    }

    protected void OnDisable()
    {
        PathObjectManager.Instance.Unregister(this);
    }

    public virtual void ManagedUpdate()
    {
    }


    void OnValidate()
    {
        // Validate Properties
        //
        if (path.x.keys.Length == 0)
        {
            path.x = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        }

        if (path.y.keys.Length == 0)
        {
            path.y = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        }

        /*
        //Remove all keys that are set before zero time
        for (int i = 0; i < path.x.keys.Length; i++)
        {
            if (path.x.keys[i].time < 0f)
            {
                path.x.RemoveKey(i);
            }
        }

        if (!path.x.keys[0].time.IsNearZero())
        {
            path.x.keys[0].time = 0f;
        }

        for (int i = 0; i < path.y.keys.Length; i++)
        {
            if (path.y.keys[i].time < 0f)
            {
                path.y.RemoveKey(i);
            }
        }

        if (!path.y.keys[0].time.IsNearZero())
        {
            path.y.keys[0].time = 0f;
        }

        Keyframe lastXKey = path.x.keys[path.x.keys.Length - 1];
        Keyframe lastYKey = path.y.keys[path.y.keys.Length - 1];

        if (lastXKey.time > lastYKey.time)
        {
            path.y.AddKey(
                lastXKey.time, lastYKey.value);
        }
        else
        {
            path.x.AddKey(
                lastYKey.time, lastXKey.value);
        }
        */
    }
}