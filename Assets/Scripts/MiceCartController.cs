using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiceCartController : MonoBehaviour
{

    private bool isFull;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    private GameObject mouse;
    private Rigidbody2D mrb; // mouse rb 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        isFull = false;
        mouse = null;
    }

    void Update()
    {
        UpdateAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D irb = collision.GetComponent<Rigidbody2D>();
        if (irb != null)
        {
            if (irb.velocity.x != 0 || irb.velocity.y != 0)
            {
                if (collision.gameObject.CompareTag("Mouse"))
                {
                    if (!isFull)
                    {
                        isFull = true;
                        sprite.color = Color.red;
                        AddMouse(collision.gameObject);
                    }
                }
            }
        }
    }

    public void UpdateAnimation()
    {
        if(rb.velocity.x >= 0)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }
    }

    public void Move(Vector2 vel)
    {
        rb.velocity = vel;
        if (mouse != null)
        {
            mrb.velocity = vel;
            mrb.position = new Vector2(mrb.position.x, rb.position.y + 0.26f);
        }
    }

    public void AddMouse(GameObject mouse)
    {
        this.mouse = mouse;
        mrb = mouse.GetComponent<Rigidbody2D>();
        mrb.position = rb.position;
        Collider2D mcoll = mouse.GetComponent<Collider2D>();
        mcoll.enabled = false;

    }
}
