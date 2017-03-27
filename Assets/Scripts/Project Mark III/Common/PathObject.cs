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
        if (transform.childCount == 0)
        {
            Destroy(this);
        }
    }


    void OnValidate()
    {
        // Validate Properties
        //
        if (path.x.keys.Length == 0)
        {
            path.x = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
            //path.x.keys[1].tangentMode = AnimationUtility;
        }

        if (path.y.keys.Length == 0)
        {
            path.y = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
        }
    }
}