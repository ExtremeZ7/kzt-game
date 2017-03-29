using UnityEngine;
using Controllers;

public class paralyzePlayerWhileTouching : MonoBehaviour
{

    PlayerController playerControl;

    void Update()
    {
        Help.WaitForPlayer(ref playerControl);
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
            playerControl.canMove = false;
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
            playerControl.canMove = true;
    }
}
