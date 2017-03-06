using UnityEngine;
using System.Collections;

public class PlayerHeadSprite : MonoBehaviour {
	/*
	public Sprite regularHead;
	public Sprite paranoidHead;
	public Sprite focusedHead;
	public Sprite upJumpHead;
	public Sprite downJumpHead;

	public GameObject player;
	
	private SpriteRenderer spriteRenderer;
	private PlayerControl pc;
	private Rigidbody2D rb2d;
	[HideInInspector] public int SpriteSelect = 0;
	private GameObject crosshair;

	private int lastSelection = 0;

	void Start () {
		pc = player.GetComponent<PlayerControl>();
		rb2d = player.GetComponent<Rigidbody2D>();
		spriteRenderer = GameObject.FindGameObjectWithTag("Player Head").GetComponent<SpriteRenderer>();
		crosshair = GameObject.FindGameObjectWithTag("Crosshair");
	}

	void Update () {

		if (pc.canMove) {
			if (!pc.ground){
				if(rb2d.velocity.y > pc.upSpriteSpeed)
					SpriteSelect = 3;
				else if(rb2d.velocity.y < pc.downSpriteSpeed)
					SpriteSelect = 4;
				else
					SpriteSelect = 1;
			}else{
				if(pc.chase)
					SpriteSelect = 1;
				else
					SpriteSelect = 0;
			}
		}

		if(Input.GetMouseButton(0) && pc.ground && crosshair.activeInHierarchy){
			SpriteSelect = 2;
		}

       if (lastSelection != SpriteSelect) {
			switch (SpriteSelect) {
			case 0:
				{
					spriteRenderer.sprite = regularHead;
					break;
				}
			case 1:
				{
					spriteRenderer.sprite = paranoidHead;
					break;
				}
			case 2:
				{
					spriteRenderer.sprite = focusedHead;
					break;
				}
			case 3:
				{
				spriteRenderer.sprite = upJumpHead;
				break;
				}
			case 4:
				{
				spriteRenderer.sprite = downJumpHead;
				break;
				}
			}
			lastSelection = SpriteSelect;
		}
	}*/
}
