/*using UnityEngine;
using Controllers;

public class StopPlayerWhenNotWalking : MonoBehaviour
{

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Player") && coll.gameObject.GetComponent<PlayerController>().getMovementState() != PlayerController.MovementState.NoGravity)
        {
            GameObject player = coll.gameObject;

            if (player.GetComponent<PlayerController>().GetDirection() == 0)
            {
                player.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            else
            {
                player.GetComponent<Rigidbody2D>().gravityScale = 4;
            }
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag.Equals("Player") && coll.gameObject.GetComponent<PlayerController>().getMovementState() != PlayerController.MovementState.NoGravity)
        {
            GameObject player = coll.gameObject;
            player.GetComponent<Rigidbody2D>().gravityScale = 4;
        }
    }
}
*/