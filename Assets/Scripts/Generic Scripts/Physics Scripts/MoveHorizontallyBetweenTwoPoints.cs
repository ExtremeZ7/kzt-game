using UnityEngine;
using AssemblyCSharp;

public class MoveHorizontallyBetweenTwoPoints : MonoBehaviour
{

    public Transform leftPoint;
    public Transform rightPoint;

    [Space(10)]
    public float velocity;

    [Space(10)]
    public bool moveRightAtStart = true;

    private bool movingRight;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        movingRight = moveRightAtStart;
    }

    void Update()
    {
        rb2d.velocity = new Vector2(velocity * (movingRight ? 1 : -1), rb2d.velocity.y);

        if ((transform.position.x > rightPoint.position.x && movingRight) || (transform.position.x < leftPoint.position.x && !movingRight))
            movingRight = !movingRight;
    }

    void OnDrawGizmos()
    {
        Vector3 cubeDimension = new Vector3(0.25f, 0.25f, 0f);

        if (leftPoint.position.x > rightPoint.position.x)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;

        Gizmos.DrawLine(new Vector3(leftPoint.position.x, Helper.FindMiddleY(leftPoint, rightPoint)), new Vector3(rightPoint.position.x, Helper.FindMiddleY(leftPoint, rightPoint)));

        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(new Vector3(leftPoint.position.x, Helper.FindMiddleY(leftPoint, rightPoint)), cubeDimension);
        Gizmos.DrawCube(new Vector3(rightPoint.position.x, Helper.FindMiddleY(leftPoint, rightPoint)), cubeDimension);
    }
}
