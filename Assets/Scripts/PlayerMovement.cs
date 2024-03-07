using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Animator anim;


    private float dirX = 0;
    private float dirY = 0;


    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float crawlSpeed = 3f;


    private enum MovementState { idle, running, jumping, falling}

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");

        if (!Input.GetKey(KeyCode.E))
        {
            ClimbCheck();
            MovePlayer();
            UpdateAnimationState();
        }
        else
        {
            anim.SetInteger("state", 0);
            if (dirX > 0f)
            {
                sprite.flipX = true;
            }
            else if (dirX < 0f)
            {
                sprite.flipX = false;
            }
        }

        
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (Input.GetButton("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void ClimbCheck()
    {
        if (IsWalled())
        {
            if (Input.GetButton("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (Input.GetButton("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, crawlSpeed);
            }
        }
    }

    

    private void UpdateAnimationState()
    {
        MovementState state;
        dirY = rb.velocity.y;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else
        {
            state = MovementState.idle;
        }

        if (dirY > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (dirY < -0.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool IsWalled()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, .1f, jumpableGround) || Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, .1f, jumpableGround);
    }
}
