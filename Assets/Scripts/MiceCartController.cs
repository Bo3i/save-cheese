using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiceCartController : MonoBehaviour
{

    private bool isFull;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        isFull = false;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Mouse"))
        {
            if (!isFull)
            {
                isFull = true;
                sprite.color = Color.red;
                Destroy(collision.gameObject);
            }
        }
    }

    public void Move(Vector2 vel)
    {
        rb.velocity = vel;
    }
}
