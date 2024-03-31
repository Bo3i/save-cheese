using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private Animator anim;

    private Vector2 dir = new Vector2(0,0);
    private bool jump = false;
    private bool dig = false;

    [SerializeField] private AudioClip jumpSound;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float crawlSpeed = 3f;


    private enum MovementState { idle, running, jumping, falling}

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    public void OnMove(InputAction.CallbackContext callback)
    {
        dir = callback.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext callback)
    {
        jump = callback.action.triggered;
    }

    public void OnDig(InputAction.CallbackContext callback)
    {
        dig = callback.action.triggered;
    }

    private void Update()
    {
        if (GameInfo.lost)
        {
            return;
        }
        if (!dig)
        {
            ClimbCheck();
            MovePlayer();
            UpdateAnimationState();
        }
        else
        {
            anim.SetInteger("state", 0);
            if (dir.x > 0f)
            {
                sprite.flipX = true;
            }
            else if (dir.x < 0f)
            {
                sprite.flipX = false;
            }
        } 
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);
        if (jump && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            SoundManager.instance.PlaySound(jumpSound, GameInfo.sFXVolume);
        }
    }

    private void ClimbCheck()
    {
        if (IsWalled())
        {
            if (jump && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                SoundManager.instance.PlaySound(jumpSound, GameInfo.sFXVolume);
            }
            else if (jump)
            {
                rb.velocity = new Vector2(rb.velocity.x, crawlSpeed);
            }
        }
    }

    

    private void UpdateAnimationState()
    {
        MovementState state;

        if (rb.velocity.x > 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else if (rb.velocity.x < 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > 0.1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -0.1f)
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
