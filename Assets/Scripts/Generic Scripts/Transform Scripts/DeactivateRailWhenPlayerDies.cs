using UnityEngine;
using AssemblyCSharp;

public class DeactivateRailWhenPlayerDies : MonoBehaviour
{

    public travelAcrossARail rail;

    private PlayerControl playerControl;

    void Start()
    {
        if (rail == null)
            rail = GetComponent<travelAcrossARail>();
    }

    void Update()
    {
        if (Helper.WaitForPlayer(ref playerControl))
        {
            if (!playerControl.gameObject.activeSelf)
                rail.enabled = false;
        }
    }
}
