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
    private Tilemap tilemap;


    private float dirX = 0;
    private float dirY = 0;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;


    private enum MovementState { idle, running, jumping, falling}

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        tilemap = GameObject.Find("CaveWalls").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        dirY = rb.velocity.y;
       
        ClimbJumpCheck();
        UpdateAnimationState();
    }

    private void ClimbJumpCheck()
    {
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        TileBase tile = tilemap.GetTile(cell);
        cell.y += 1;
        TileBase tileUp = tilemap.GetTile(cell);
        if (tile != null && tileUp == null && IsGrounded())
        {
            // Debug.Log("just under");
            if (Input.GetButton("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
        else if (tile != null && tileUp == null)
        {
            //fDebug.Log("just under");
            if (Input.GetButton("Jump"))
            {
                //Debug.Log("just under and jumping " + rb.velocity);
                rb.AddForce(Vector2.up * 0.2f, ForceMode2D.Impulse);
            }
        }

        else if (tile != null)
        {
            if (Input.GetButton("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
            }
        }


        else if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        

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
}
