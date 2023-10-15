using UnityEngine;

/*** SCRIPT CONTROLS THE PLAYER's BEHAVIOUR ***/
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask jumpableWall;

    public float moveSpeed = 7f;
    public float jumpForce = 14f;
    private float dirX = 0f;

    [Header("Wall Jump System")]
    private bool isWallJumping = true;
    private bool hasWallJumpedRight = false;
    private bool hasWallJumpedLeft = false;
    private bool isSliding;
    private int wallSlidingSpeed = 3;
    private float wallJumpDuration = 0.1f;

    [SerializeField] private Vector2 wallJumpForce;

    private enum MovementState { idle, running, jumping, falling, wallJump };
    MovementState state;


    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb.gravityScale = 3;
    }

    // Called once per frame
    private void Update()
    {
        //perform action for walking back and forth
        dirX = Input.GetAxisRaw("Horizontal");

        // if player is wall jumping, don't allow horizontal movement
        if (!isWallJumping)
        {
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        }

        // Always first check the if the player is grounded or not         
        bool isGrounded = IsGrounded();

        // reset jump states if the player is on the ground
        if (isGrounded)
        {
            isWallJumping = false;
            hasWallJumpedLeft = false;
            hasWallJumpedRight = false;
        }

        //if the player is grounded and jumps, then perform the jump ability
        if ((Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.UpArrow)) && (isSliding || isGrounded))
        {
            // Check if it's a wall slide jump vs a normal jump
            if (isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpSoundEffect.Play();
            }
            else if (isSliding)
            {
                doWallJump(dirX);
            }
        }

        WallSlide();
        UpdateAnimationState();
    }

    private void WallSlide()
    {
        //checks if the player is touching the wall, while player is not touching the ground and is pressing down the left/right keys
        if ((IsWalledRight() || IsWalledLeft()) && !IsGrounded() && dirX != 0)
        {
            isSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isSliding = false;
        }
    }

    //method to control the animations of the character 
    private void UpdateAnimationState()
    {
        //running animation
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        //jumping to falling animation
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        //wall jumping animation
        if ((IsWalledRight() || IsWalledLeft()) && !IsGrounded() && dirX != 0)
        {
            state = MovementState.wallJump;

            if (IsWalledRight())
            {
                sprite.flipX = false;
            }
            else if (IsWalledLeft())
            {
                sprite.flipX = true;
            }
        }

        //change animation states
        anim.SetInteger("state", (int)state);
    }

    // Handles the walljumping code
    private void doWallJump(float jumpDirection)
    {
        bool canWallJump = false;

        // If the parameter is negative then the player has recently tried to jump from a wall on his left
        if (jumpDirection < 0)
        {
            // Now check if has already done a left wall jump. If no, then allow them to do a wall jump
            // TODO: Make this one if-statement instead?
            if (!hasWallJumpedLeft)
            {
                canWallJump = true;
                hasWallJumpedLeft = true;
                hasWallJumpedRight = false;
            }
        }
        // otherwise, if the parameter is positive, that means the player has recently tried to jump from a wall on his right
        else if (jumpDirection > 0)
        {
            // Same logic as above, except on the right side
            if (!hasWallJumpedRight)
            {
                canWallJump = true;
                hasWallJumpedRight = true;
                hasWallJumpedLeft = false;
            }
        }

        if (canWallJump)
        {
            rb.velocity = new Vector2(-jumpDirection * wallJumpForce.x, wallJumpForce.y);
            jumpSoundEffect.Play();
            isWallJumping = true;
        }
    }

    // Returns true if the player is either grounded or is sliding along a wall.
    private bool CanJump()
    {
        return IsGrounded() || isSliding;
    }

    //detects if the player is touching the ground
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    //checks if the player is touching the wall on the wall
    private bool IsWalledRight()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, .1f, jumpableWall);
    }

    //checks if the player is touching the wall on the left
    private bool IsWalledLeft()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, .1f, jumpableWall);
    }
}
