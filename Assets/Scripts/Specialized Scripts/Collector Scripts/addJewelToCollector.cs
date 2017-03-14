using UnityEngine;
using System.Collections;

public class addJewelToCollector : MonoBehaviour
{

    private GameObject jewelCollector;
    private showJewelCount sjc;

    void Start()
    {
        jewelCollector = GameObject.FindGameObjectWithTag("JewelCollector");
        sjc = jewelCollector.GetComponent<showJewelCount>();
        GameControl.control.items.crystalCount++;
        sjc.hideDelay = sjc.guiHideDelay;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, jewelCollector.transform.position) <= 0.25f)
        {
            GameControl.control.items.crystalsInCollection++;
            Object.Destroy(gameObject);
        }
    }
}
