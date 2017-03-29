using UnityEngine;
using Controllers;

public class addJewelToCollector : MonoBehaviour
{

    GameObject jewelCollector;
    showJewelCount sjc;

    void Start()
    {
        jewelCollector = GameObject.FindGameObjectWithTag("JewelCollector");
        sjc = jewelCollector.GetComponent<showJewelCount>();
        GameController.Instance.items.crystalCount++;
        sjc.hideDelay = sjc.guiHideDelay;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, jewelCollector.transform.position) <= 0.25f)
        {
            GameController.Instance.items.crystalsInCollection++;
            Object.Destroy(gameObject);
        }
    }
}
