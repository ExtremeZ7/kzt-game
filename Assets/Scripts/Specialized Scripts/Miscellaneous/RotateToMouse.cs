using UnityEngine;
using System.Collections;

public class RotateToMouse : MonoBehaviour {
/*
	public Vector3 mouse_pos;
	public Vector3 object_pos;
	public float angle;

	public Animator ani;

	public Transform player;
	public Transform l_arm;
	public Transform r_arm;
	public Transform head;
    public Transform ll_leg;
    public Transform ul_leg;
    public Transform l_foot;
    public Transform lr_leg;
    public Transform ur_leg;
    public Transform r_foot;

    public PlayerControl P;
	public BoltSpawner B;
	private bool justEntered = false;

	[HideInInspector] public bool chargeUp = false;

    private LevelManager lm;

	private PlayerHeadSprite phs;


	// Use this for initialization
	void Start () {
		ani = GetComponent<Animator>();
		phs = GetComponent<PlayerHeadSprite>();
		B = FindObjectOfType<BoltSpawner>();
        lm = FindObjectOfType<LevelManager>();
        ani.SetBool("Ground",true);
        ani.SetFloat("Speed",0);
	}
	
	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButtonDown (0) && P.ground && P.dir == 0)
			chargeUp = true;
		if (Input.GetMouseButtonUp(0))
			chargeUp = false;
		
		if (chargeUp && !lm.playerDeath) {
            ani.SetBool("Ground", true);
            ani.SetFloat("Speed", 0);
            P.canMove = false;
			ani.enabled = false;
			phs.SpriteSelect = 2;
			//rotation
			Vector3 mousePos = Input.mousePosition;
			mousePos.z = 5.23f;

			Vector3 objectPos = Camera.main.WorldToScreenPoint(player.transform.position);
			mousePos.x = mousePos.x - objectPos.x;
			mousePos.y = mousePos.y - objectPos.y;
			
			float angle = Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg;

		if(!P.facingRight)
				angle = 180 - angle;

			l_arm.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle + 90));
			r_arm.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle + 90));
			head.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
            ll_leg.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            ul_leg.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            l_foot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            lr_leg.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            ur_leg.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            r_foot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            if ((mousePos.x > 0 && !P.facingRight) || (mousePos.x < 0 && P.facingRight))
				P.Flip ();
			justEntered = true;
		} 
		else {
			ani.enabled = true;
			P.canMove = true;
			if(justEntered){
			  justEntered = false;  
			  B.activated = true;
			}
		}
	}
	*/
}
