using UnityEngine;
using AssemblyCSharp;

[RequireComponent(typeof(Rigidbody2D))]
public class addRigidbodyForceAtStart : MonoBehaviour
{

    public Vector2 baseForce;
    public Vector2 forceVariation;

    [Space(10)]
    public float delay;

    void OnDrawGizmosSelected()
    {
        Vector3 baseForce = new Vector3(this.baseForce.x, this.baseForce.y);

        Gizmos.color = Color.cyan;

        Gizmos.DrawLine(transform.position, new Vector2((transform.position + baseForce).x - forceVariation.x, (transform.position + baseForce).y));
        Gizmos.DrawLine(transform.position, new Vector2((transform.position + baseForce).x + forceVariation.x, (transform.position + baseForce).y));
        Gizmos.DrawLine(transform.position, new Vector2((transform.position + baseForce).x, (transform.position + baseForce).y - forceVariation.y));
        Gizmos.DrawLine(transform.position, new Vector2((transform.position + baseForce).x, (transform.position + baseForce).y + forceVariation.y));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2((transform.position + baseForce).x - forceVariation.x, (transform.position + baseForce).y),
            new Vector2((transform.position + baseForce).x + forceVariation.x, (transform.position + baseForce).y));
        Gizmos.DrawLine(new Vector2((transform.position + baseForce).x, (transform.position + baseForce).y - forceVariation.y),
            new Vector2((transform.position + baseForce).x, (transform.position + baseForce).y + forceVariation.y));
    }

    void Update()
    {
        if (Help.UseAsTimer(ref delay))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(baseForce.x + Random.Range(-forceVariation.x, forceVariation.x), baseForce.y + Random.Range(-forceVariation.y, forceVariation.y));
            this.enabled = false;
        }
    }
}
