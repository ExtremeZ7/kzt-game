using UnityEngine;
using AssemblyCSharp;

public class AccelerateForward : MonoBehaviour
{

    public float accelerationSpeed;
    public float maxVelocity;
    public float delay;

    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Helper.UseAsTimer(ref delay))
        {
            rb2d.velocity = new Vector2(Mathf.MoveTowards(rb2d.velocity.x, maxVelocity * Mathf.Sign(transform.localScale.x), accelerationSpeed),
                rb2d.velocity.y);
        }
    }
}
