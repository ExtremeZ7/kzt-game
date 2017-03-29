using UnityEngine;
using Common.Extensions;

public class PlayerController : MonoBehaviour
{

    public enum MovementState
    {
        Normal,
        SlipperyFloor,
        NoGravity}

    ;

    MovementState movementState;

    Rigidbody2D rigidbody2d;

    public float walkSpeed = 5.0f;
    public float jumpForce = 15.0f;
    public float bounceForce = 12.0f;
    public float headBounceForce = -4.0f;
    public float upBounceForce = 20.0f;
    public float jumpMomentum = 0.33f;
    public float groundCheckRadius = 0.22f;
    public float upSpriteSpeed = 5.0f;
    public float downSpriteSpeed = -5.0f;
    public float fallJumpTime = 0.5f;

    [Space(10)]
    [Header("No Gravity")]
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    public float normalBounceForce = 4.0f;
    public float harderBounceForce = 6.0f;
    public float strongestBounceForce = 8.0f;

    [Space(10)]
    public bool startWithNoGravity;

    float moveSpeed;
    bool justFell;
    bool allowDelayedJump;

    Animator animator;

    Transform groundCheck;
    [HideInInspector]
    public Animator shieldAnimator;
    public LayerMask collideWithLayer;

    [HideInInspector]
    public bool canMove = true;

    int direction;
    float groundIgnoreTimer;
    int xDirection;
    int yDirection;

    [Space(10)]
    public GameObject playerJumpAudioSource;

    Transform body;
    SpriteRenderer playerHead;

    [Space(10)]
    public Sprite normalLook;
    public Sprite focusedLook;
    public Sprite anxiousLookForward;
    public Sprite anxiousLookUp;
    public Sprite anxiousLookDown;

    void OnEnable()
    {
        InputEventManager.upPressed += Jump;
        InputEventManager.horHeld += Move;
    }

    void OnDisable()
    {
        InputEventManager.upPressed -= Jump;
        InputEventManager.horHeld -= Move;
    }

    /*void Start()
    {
        groundCheck = transform.GetChild(2);
        moveSpeed = 0;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        groundIgnoreTimer = 30f / 60f;

        body = transform.GetChild(0);
        playerHead = transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<SpriteRenderer>();

        if (startWithNoGravity)
            ChangeMovementState(MovementState.NoGravity);

        shieldAnimator = transform.GetChild(5).GetComponent<Animator>();
    }

    void Update()
    {
        // Leave if the game is paused
        if (GameControl.control.paused)
        {
            return;
        }

        bool leftMovementHeld = false;
        bool rightMovementHeld = false;
        bool onGround = animator.GetBool("On Ground");

        animator.SetBool("No Grav", movementState == MovementState.NoGravity);

        //Sets Animator "Aiming" Parameter to true only if the player holds down the left mouse button while on the ground and standing still

        if (movementState != MovementState.NoGravity)
        {
            if (onGround)
            {
                if (Input.GetAxis("Aim").IsNear(1) && !animator.GetBool("Moving"))
                {
                    if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Can Aim"))
                    {	
                        canMove = false;
                        animator.SetBool("Aiming", true);
                    }
                }
                else
                {
                    canMove = true;
                }
            }
        }
        else
        {
            animator.SetBool("Aiming", Input.GetAxis("Aim").IsNear(1));
        }

        //bool allowDelayedJump = !Helper.useAsTimer (ref fallJumpTimer);

        if (Help.UseAsTimer(ref groundIgnoreTimer) && movementState != MovementState.NoGravity)
        {
            animator.SetBool("On Ground", Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collideWithLayer));
            if (!Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collideWithLayer) && !justFell)
            {
                justFell = true;
                allowDelayedJump = true;
                Invoke("EndJumpDelay", fallJumpTime);
            }
            else
            {
                justFell &= !Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collideWithLayer);
            }
        }

        //Allows the player to use the movement controls only when he is not aiming
        if (movementState != MovementState.NoGravity)
        {
            if (canMove && !animator.GetBool("Aiming"))
            {
                leftMovementHeld |= Input.GetAxis("Horizontal").IsNear(-1.0f);
                rightMovementHeld |= Input.GetAxis("Horizontal").IsNear(1.0f);
                if (Input.GetAxis("Vertical").IsNear(1f) && (onGround || allowDelayedJump))
                {
                    Jump();
                }
            }
        }
        else
        {
            xDirection = (int)Input.GetAxis("Horizontal");
            yDirection = (int)Input.GetAxis("Vertical");
        }

        //Sets the direction of movement based on which keys the player is currently holding
        direction = (leftMovementHeld ? -1 : 0) + (rightMovementHeld ? 1 : 0);

        animator.SetBool("Moving", direction != 0 ? true : false);
        animator.SetBool("In Jump State", animator.GetCurrentAnimatorStateInfo(0).IsName("Jump Triggered"));

        FlipBody();
        ChangePlayerHeadSprite();

        if (transform.parent == null)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
            transform.localScale = Vector3.one;
        }
				
    }

    void FixedUpdate()
    {

        switch (movementState)
        {
            case MovementState.Normal:
                if (animator.GetBool("On Ground"))
                {
                    moveSpeed = direction * walkSpeed;
                }
                else
                {
                    moveSpeed = Mathf.MoveTowards(moveSpeed, walkSpeed * direction, jumpMomentum * (direction == 0 ? 0.5f : 1f));
                }

                animator.SetBool("Sliding", false);
                rigidbody2d.velocity = new Vector2(moveSpeed, rigidbody2d.velocity.y);
                break;

            case MovementState.SlipperyFloor:
                float speed = jumpMomentum / 2;
                moveSpeed = Mathf.MoveTowards(moveSpeed, moveSpeed != 0 || direction != 0 ? walkSpeed * (direction == 0 ? Mathf.Sign(moveSpeed) : direction) * 2f : moveSpeed, speed);
			//moveSpeed = Mathf.MoveTowards(moveSpeed, direction != 0 ? walkSpeed * direction * 1.5f : moveSpeed, speed);
                movementState = MovementState.Normal;

                animator.SetBool("Sliding", moveSpeed != 0 || direction != 0);
                rigidbody2d.velocity = new Vector2(moveSpeed, rigidbody2d.velocity.y);
                break;

            case MovementState.NoGravity:
                rigidbody2d.velocity = Vector2.MoveTowards(rigidbody2d.velocity, 
                    xDirection == 0 || yDirection == 0 ? new Vector2(maxSpeed * xDirection, maxSpeed * yDirection) : new Vector2(maxSpeed / Mathf.Sqrt(2) * xDirection, maxSpeed / Mathf.Sqrt(2) * yDirection),
                    (xDirection == 0 && yDirection == 0 ? deceleration : acceleration) * Time.deltaTime);

                moveSpeed = rigidbody2d.velocity.x;
                break;
        }
    }*/

    void Move(float direction)
    {
        bool onPlatform =
            Physics2D.OverlapCircle(transform.position + new Vector3(0f, 1.0f),
                groundCheckRadius, collideWithLayer);

        switch (movementState)
        {
            case MovementState.Normal:
                if (onPlatform)
                {
                    moveSpeed = direction * walkSpeed;
                }
                else
                {
                    moveSpeed = Mathf.MoveTowards(moveSpeed, walkSpeed * direction, jumpMomentum * (direction.IsNearZero() ? 0.5f : 1f));
                }

                animator.SetBool("Sliding", false);
                rigidbody2d.velocity = new Vector2(moveSpeed, rigidbody2d.velocity.y);
                break;

            case MovementState.SlipperyFloor:
                float speed = jumpMomentum / 2;
                moveSpeed = Mathf.MoveTowards(moveSpeed, moveSpeed != 0 || direction != 0 ? walkSpeed * (direction == 0 ? Mathf.Sign(moveSpeed) : direction) * 2f : moveSpeed, speed);
                //moveSpeed = Mathf.MoveTowards(moveSpeed, direction != 0 ? walkSpeed * direction * 1.5f : moveSpeed, speed);
                movementState = MovementState.Normal;

                animator.SetBool("Sliding", moveSpeed != 0 || direction != 0);
                rigidbody2d.velocity = new Vector2(moveSpeed, rigidbody2d.velocity.y);
                break;

            case MovementState.NoGravity:
                rigidbody2d.velocity = Vector2.MoveTowards(rigidbody2d.velocity, 
                    xDirection == 0 || yDirection == 0 ? new Vector2(maxSpeed * xDirection, maxSpeed * yDirection) : new Vector2(maxSpeed / Mathf.Sqrt(2) * xDirection, maxSpeed / Mathf.Sqrt(2) * yDirection),
                    (xDirection == 0 && yDirection == 0 ? deceleration : acceleration) * Time.deltaTime);

                moveSpeed = rigidbody2d.velocity.x;
                break;
        }
    }

    public void Jump()
    {
        if (movementState == MovementState.NoGravity)
        {
            return;
        }

        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpForce);
        Instantiate(playerJumpAudioSource, transform.position, Quaternion.identity);
        InitJumpAnimation();
    }

    public void JumpBounce()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, movementState != MovementState.NoGravity ? jumpForce : harderBounceForce);
        InitJumpAnimation();
    }

    public void Bounce()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, movementState != MovementState.NoGravity ? bounceForce : normalBounceForce);
        InitJumpAnimation();
    }

    public void HeadBounce()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, movementState != MovementState.NoGravity ? headBounceForce : -normalBounceForce);
    }

    public void UpBounce()
    {
        rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, movementState != MovementState.NoGravity ? upBounceForce : strongestBounceForce);
        InitJumpAnimation();
    }

    void InitJumpAnimation()
    {
        groundIgnoreTimer = 3f / 60f;
        CancelInvoke("EndJumpDelay");
        allowDelayedJump = false;
        justFell = true;
        animator.SetTrigger("Jump");
        animator.SetBool("On Ground", false);
    }

    public void FlipBody()
    {
        if (movementState != MovementState.NoGravity)
        {
            if (!animator.GetBool("Aiming"))
            {
                if (direction != 0)
                {
                    Vector3 theScale = body.localScale;
                    theScale.x = Mathf.Abs(theScale.x) * direction;
                    body.localScale = theScale;
                }
            }
        }
        else
        {
            if (xDirection != 0)
            {
                Vector3 theScale = body.localScale;
                theScale.x = Mathf.Abs(theScale.x) * xDirection;
                body.localScale = theScale;
            }
        }
    }

    void ChangePlayerHeadSprite()
    {
        if (animator.GetBool("Aiming"))
        {
            playerHead.sprite = focusedLook;
        }
        else
        {
            if (movementState != MovementState.NoGravity)
            {
                if (animator.GetBool("On Ground"))
                {
                    if (animator.GetBool("Sliding"))
                        playerHead.sprite = anxiousLookDown;
                    else if (animator.GetBool("Being Chased"))
                        playerHead.sprite = anxiousLookForward;
                    else
                        playerHead.sprite = normalLook;
                }
                else
                {
                    if (rigidbody2d.velocity.y > downSpriteSpeed && rigidbody2d.velocity.y < upSpriteSpeed)
                        playerHead.sprite = anxiousLookForward;
                    else if (rigidbody2d.velocity.y <= downSpriteSpeed)
                        playerHead.sprite = anxiousLookDown;
                    else if (rigidbody2d.velocity.y >= upSpriteSpeed)
                        playerHead.sprite = anxiousLookUp;
                }
            }
            else
            {
                switch (yDirection)
                {
                    case 0:
                        switch (xDirection)
                        {
                            case 0:
                                playerHead.sprite = normalLook;
                                break;
                            case 1:
                                playerHead.sprite = anxiousLookForward;
                                break;
                            case -1:
                                goto case 1;
                            default:
                                goto case 0;
                        }
                        break;
                    case 1:
                        playerHead.sprite = anxiousLookUp;
                        break;
                    case -1:
                        playerHead.sprite = anxiousLookDown;
                        break;
                    default:
                        goto case 0;
                }
            }
        }
    }

    public int GetDirection()
    {
        return direction;
    }

    public void ChangeMovementState(PlayerController.MovementState newState)
    {
        if (newState != MovementState.SlipperyFloor || movementState != MovementState.NoGravity)
            movementState = newState;

        if (movementState == MovementState.NoGravity)
        {
            GameObject.FindGameObjectWithTag("GAMEOBJECTS").GetComponent<LevelManager>().noGravity = true;
            rigidbody2d.gravityScale = 0f;
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 2f);
        }
        else
        {
            GameObject.FindGameObjectWithTag("GAMEOBJECTS").GetComponent<LevelManager>().noGravity = false;
            rigidbody2d.gravityScale = 4f;
        }
    }

    public MovementState getMovementState()
    {
        return movementState;
    }

    public int shields()
    {
        return shieldAnimator.GetInteger("Shields");
    }

    public bool vulnerable()
    {
        return shieldAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Vulnerable");
    }

    public void loseShield()
    {
        if (shields() > 0 && vulnerable())
        {
            shieldAnimator.SetInteger("Shields", shields() - 1);

            if (movementState != MovementState.NoGravity)
                Bounce();
        }
    }

    public void GainShield()
    {
        if (shields() < 3)
            shieldAnimator.SetInteger("Shields", shields() + 1);
    }

    void EndJumpDelay()
    {
        allowDelayedJump = false;
    }
}
