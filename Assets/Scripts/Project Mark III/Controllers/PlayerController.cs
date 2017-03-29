using UnityEngine;
using Common.Extensions;
using Managers;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        Rigidbody2D rigidBody;
        public static bool gravityIsActive = true;

        [Header("Physics Values")]
        public float maxHorizontalSpeed = 5.0f;
        public float jumpForce = 15.0f;
        public float weakBounceForce = 12.0f;
        public float headBounceForce = -4.0f;
        public float strongBounceForce = 20.0f;
        public float airAcceleration = 0.33f;
        public float airFriction = 0.15f;
        public float slipperyFloorAcceleration = 0.15f;
        public float maxSlipperyFloorSpeed = 10f;
        public float fallJumpTime = 0.5f;
        public float groundCheckRadius = 0.22f;

        [Space(10)]
        [Header("No Gravity")]
        public float maxGravitySpeed;
        public float gravityAcceleration;
        public float gravityFriction;
        public float normalBounceForce = 4.0f;
        public float harderBounceForce = 6.0f;
        public float strongestBounceForce = 8.0f;

        [Space(10)]
        public bool startWithNoGravity;

        float moveSpeed;
        bool justFell;
        bool allowDelayedJump;

        Animator playerAnimator;

        Transform groundCheck;
        [HideInInspector]
        public Animator shieldAnimator;
        public LayerMask platformLayer;
        public LayerMask slipperyFloorLayer;

        [HideInInspector]
        public bool canMove = true;

        public Transform body;

        float xAxis;
        float yAxis;

        bool onPlatform;

        void OnEnable()
        {
            InputEventManager.upPressed += KeyJump;
            InputEventManager.horHeld += SetHorizontalAxis;
            InputEventManager.vertHeld += SetVerticalAxis;
        }

        void OnDisable()
        {
            InputEventManager.upPressed -= KeyJump;
            InputEventManager.horHeld -= SetHorizontalAxis;
            InputEventManager.vertHeld -= SetVerticalAxis;
        }

        void Start()
        {
            playerAnimator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody2D>();
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

        void SetHorizontalAxis(float horAxis)
        {
            xAxis = horAxis;
        }

        void SetVerticalAxis(float vertAxis)
        {
            yAxis = vertAxis;
        }

        void Update()
        {
            onPlatform =
                Physics2D.OverlapCircle(
                transform.position + new Vector3(0f, -1f),
                groundCheckRadius, platformLayer);
            
            bool onSlipperyFloor = 
                Physics2D.OverlapCircle(
                    transform.position + new Vector3(0f, -1f),
                    groundCheckRadius, slipperyFloorLayer);

            playerAnimator.SetBool("OnPlatform", onPlatform);
            playerAnimator.SetBool("Sliding", onSlipperyFloor);
            playerAnimator.SetBool("GravityIsActive", gravityIsActive);
            playerAnimator.SetFloat("xAxis", xAxis);

            if (onPlatform && !onSlipperyFloor) // On The Ground
            {
                rigidBody.velocity =
                    rigidBody.velocity.SetX(xAxis * maxHorizontalSpeed);
                return;
            }
            else if (onSlipperyFloor) //  On Some Slippery Floor
            {
                // Don't Move If The Speed If The Player Is Not Moving
                // And The Axis Is At Zero
                if (moveSpeed.IsNearZero() && xAxis.IsNearZero())
                {
                    return;
                }

                // Move Towards The Axis If It Is Not Zero
                // Otherwise Move Towards The Nearest Max Speed, Anyway
                moveSpeed = Mathf.MoveTowards(moveSpeed,
                    maxSlipperyFloorSpeed *
                    (!xAxis.IsNearZero() ?
                        Mathf.Sign(moveSpeed) : xAxis) * Time.deltaTime,
                    slipperyFloorAcceleration * Time.deltaTime);
            }
            else if (!gravityIsActive) // No Gravity! Weeeeee!!!
            {
                rigidBody.velocity = Vector2.MoveTowards(rigidBody.velocity, 
                    new Vector2(maxGravitySpeed * xAxis, maxGravitySpeed * yAxis),
                    xAxis.IsNearZero() && yAxis.IsNearZero() ?
                    gravityFriction : gravityAcceleration * Time.deltaTime);

                return;
            }
            else // In The Air With Gravity
            {
                moveSpeed = rigidBody.velocity.x;
                moveSpeed = Mathf.MoveTowards(moveSpeed,
                    maxHorizontalSpeed * xAxis,
                    xAxis.IsNearZero()
                    ? airFriction : airAcceleration);
            }

            rigidBody.velocity = rigidBody.velocity.SetX(moveSpeed);
        }

        /// <summary>
        /// The Jump The Occurs By Pressing The Jump Key
        /// </summary>
        void KeyJump()
        {
            if (!onPlatform || !gravityIsActive)
            {
                return;
            }

            rigidBody.velocity = rigidBody.velocity.SetY(jumpForce);

            CancelInvoke("EndJumpDelay");
            allowDelayedJump = false;
            justFell = true;
            playerAnimator.SetTrigger("Jump");
            playerAnimator.SetBool("OnPlatform", false);
        }

        /*public void JumpBounce()
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, !gravityIsActive ? jumpForce : harderBounceForce);
            InitJumpAnimation();
        }

        public void Bounce()
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, !gravityIsActive ? weakBounceForce : normalBounceForce);
            InitJumpAnimation();
        }

        public void HeadBounce()
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, !gravityIsActive ? headBounceForce : -normalBounceForce);
        }

        public void UpBounce()
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, !gravityIsActive ? strongBounceForce : strongestBounceForce);
            InitJumpAnimation();
        }*/

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

                /*if (!gravityIsActive)
                {
                    Bounce();
                }*/
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
}