/*using UnityEngine;
using Common.Extensions;
using Controllers;

public class SlideIfOnSlipperyFloor : MonoBehaviour
{
    PlayerController playerControl;

    public string[] tags;

    void Start()
    {
        playerControl = GetComponent<PlayerController>();
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if (tags.Contains(coll.gameObject.tag))
        {
            playerControl.ChangeMovementState(PlayerController.MovementState.SlipperyFloor);
        }
    }
}*/
