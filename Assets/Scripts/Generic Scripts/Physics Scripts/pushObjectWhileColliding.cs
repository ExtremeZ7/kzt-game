using UnityEngine;
using System.Collections;

public class pushObjectWhileColliding : MonoBehaviour {

	public enum PushMode{AddForce, PushDiscretely};

	public string[] tags;

	[Space(10)]
	public float xForce = 0;
	public float yForce = 0;

	[Space(10)]
	public PushMode pushMode;

	[Space(10)]
	public PhysicsMaterial2D materialToIgnore;

	void OnTriggerStay2D(Collider2D coll){
		if(tags.Contains(coll.gameObject.tag) && !coll.sharedMaterial.Equals(materialToIgnore)){
			switch(pushMode){
			case PushMode.AddForce:
				Rigidbody2D rb2d = coll.gameObject.GetComponent<Rigidbody2D>();
				rb2d.AddForce(new Vector2(xForce * Mathf.Sign(transform.parent.localScale.x)
										,yForce * Mathf.Sign(transform.parent.localScale.y)));
				break;
			case PushMode.PushDiscretely:
				Transform objectTransform = coll.gameObject.transform;
				objectTransform.position = new Vector3(objectTransform.position.x + (xForce * Time.deltaTime), 
																					objectTransform.position.y + (yForce * Time.deltaTime), 0f);
				break;
			}
		}
	}
}
