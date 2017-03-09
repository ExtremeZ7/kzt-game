using UnityEngine;
using AssemblyCSharp;

public class BlowGoldenBubble : MonoBehaviour
{

    public string[] bubbleTags;

    [Space(10)]
    public float pushStrength;

    private PlayerControl playerControl;

    public void OnTriggerStay2D(Collider2D coll)
    {
        if (bubbleTags.Contains(coll.gameObject.tag) && Help.WaitForPlayer(ref playerControl))
        {
            Vector2 pushVector = Trigo.GetRotatedVector(transform.rotation.eulerAngles.z + 90f, pushStrength * Time.fixedDeltaTime);
            pushVector = new Vector2(pushVector.x * 4f, pushVector.y);
            Rigidbody2D rb2d = playerControl.gameObject.GetComponent<Rigidbody2D>();
            rb2d.AddForce(pushVector);
        }
    }
}
