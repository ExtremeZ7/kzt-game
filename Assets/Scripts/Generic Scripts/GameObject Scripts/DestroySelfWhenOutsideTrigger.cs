using UnityEngine;
using Common.Extensions;

public class DestroySelfWhenOutsideTrigger : MonoBehaviour
{

    public string[] tags;
    private float timer;
    private float delay = 3f / 60f;

    void Start()
    {
        timer = delay;
    }

    void FixedUpdate()
    {
        if (Help.UseAsTimer(ref timer))
        {
            Object.Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (tags.Contains(coll.gameObject.tag))
        {
            timer = delay;
        }
    }
}
