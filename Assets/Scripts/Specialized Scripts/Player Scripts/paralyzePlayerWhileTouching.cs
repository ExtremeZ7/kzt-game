using UnityEngine;
using AssemblyCSharp;

public class paralyzePlayerWhileTouching : MonoBehaviour
{

    private PlayerController playerControl;

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
