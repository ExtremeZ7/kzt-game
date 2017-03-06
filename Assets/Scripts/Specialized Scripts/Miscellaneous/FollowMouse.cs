using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour {
/*	
	public Vector3 mouse_Pos;
	public Vector3 orig_Pos;
	public RotateToMouse R;
	public float crosshairSpeed;
	public bool chargedUp = false;
    public Transform cam_pos;
    public Camera cam;
    private float height;
    private float width;
	private Animator ani;
    //private AnimatorStateInfo anis;

    // Use this for initialization
	void Start () {
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        R = FindObjectOfType<RotateToMouse> ();
		mouse_Pos = transform.position;
		orig_Pos = transform.position;
		ani = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (R.chargeUp)
        {
            mouse_Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse_Pos.z = transform.position.z;
            if (mouse_Pos.x > cam_pos.position.x - width / 2 && mouse_Pos.x < cam_pos.position.x + width / 2)
            {
                if (mouse_Pos.y > cam_pos.position.y - height / 2 && mouse_Pos.y < cam_pos.position.y + height / 2)
                {
                    transform.position = Vector3.MoveTowards(transform.position, mouse_Pos, crosshairSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, orig_Pos, crosshairSpeed * Time.deltaTime);
        }
     
        ani.SetBool("Charge Up",R.chargeUp);
	}
	*/
}