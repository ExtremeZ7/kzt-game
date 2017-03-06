using UnityEngine;
using System.Collections;

public class fireAnElectricBolt : MonoBehaviour {

	/*private GameObject player;
	private GameObject crosshair;

	public GameObject electricBolt;
	private bool boltReady = false;

	private GameObject newBolt;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		crosshair = GameObject.FindGameObjectWithTag("Crosshair");
	}

	void Update () {
		if (crosshair.activeInHierarchy) {
			Animator ani = crosshair.GetComponent<Animator> ();
			if(!boltReady && ani.GetBool("Charge Up")){
				newBolt = Instantiate(electricBolt,transform.position,Quaternion.identity) as GameObject;
				newBolt.name = "Electric Bolt";
				newBolt.SetActive(false);
				boltReady = true;
			}
			else if(boltReady && ani.GetBool("Charge Up")){
				Transform bolt = newBolt.transform.GetChild(0);
				Transform rail = newBolt.transform.GetChild(1);
				bolt.position = new Vector3(transform.position.x,transform.position.y,0);
				rail.position = new Vector3(crosshair.transform.position.x,crosshair.transform.position.y,0);
			}
			else if(boltReady && !ani.GetBool("Charge Up")){
				newBolt.SetActive(true);
				boltReady = false;
			}
		}

		if(boltReady && !player.activeInHierarchy){
			newBolt.SetActive(false);
		}
	}*/
}
