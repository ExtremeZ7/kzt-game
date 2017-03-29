using UnityEngine;
using Controllers;

public class DeactivateRailWhenPlayerDies : MonoBehaviour
{

    public travelAcrossARail rail;

    private PlayerController playerControl;

    void Start()
    {
        if (rail == null)
            rail = GetComponent<travelAcrossARail>();
    }

    void Update()
    {
        if (Help.WaitForPlayer(ref playerControl))
        {
            if (!playerControl.gameObject.activeSelf)
                rail.enabled = false;
        }
    }
}
