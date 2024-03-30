using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using System.Globalization;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip dropSound;

    [SerializeField] private Object FuellItem;
    [SerializeField] private Object ResourceItem;
    [SerializeField] private Object Mouse;

    [SerializeField] private float throwStrengt = 5f;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private LayerMask jumpableGround;

    private bool drop = false;

    public int[] inventory = new int[3];
    private int inventorySize = 3;
    private enum ItemType { fuellCheese, materialCheese, mice }

    private void Start()
    {
        jumpableGround = LayerMask.GetMask("Ground");
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        inventory[(int)ItemType.fuellCheese] = 0;
        inventory[(int)ItemType.materialCheese] = 0;
        inventory[(int)ItemType.mice] = 0;
        
    }

    

    private void Update()
    {
        DropItem();
    }


    public void OnItemDrop(InputAction.CallbackContext context)
    {
        drop = context.action.triggered;
    }

    private void DropItem()
    {
        if (drop) 
        {
            drop = false;
            Object newObject = null;
            Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
            if (dir.x == 0 && dir.y == 0)
            {
                dir = new Vector2(0, 1);
            }
            if (!GetNeighbour(dir))
            {
                if (inventory[(int)ItemType.fuellCheese] > 0)
                {
                    inventory[(int)ItemType.fuellCheese]--;
                    newObject = Instantiate(FuellItem, new Vector3(transform.position.x + dir.x, transform.position.y + dir.y, 0), Quaternion.identity);
                }
                else if (inventory[(int)ItemType.materialCheese] > 0)
                {
                    inventory[(int)ItemType.materialCheese]--;
                    newObject = Instantiate(ResourceItem, new Vector3(transform.position.x + dir.x, transform.position.y + dir.y, 0), Quaternion.identity);
                }
                else if (inventory[(int)ItemType.mice] > 0)
                {
                    inventory[(int)ItemType.mice]--;
                    newObject = Instantiate(Mouse, new Vector3(transform.position.x + dir.x, transform.position.y + dir.y, 0), Quaternion.identity);
                }
                if (newObject != null)
                {
                    newObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir.x * throwStrengt + 0.5f * rb.velocity.x, dir.y * throwStrengt + 0.5f * rb.velocity.y), ForceMode2D.Impulse);
                }
                //SoundManager.instance.PlaySound(dropSound, GameInfo.sFXVolume);
            }
        }
    }

    private bool GetNeighbour(Vector2 dir)
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, dir, 1f, jumpableGround);
    }

    private bool IsWalled()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, .1f, jumpableGround) || Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, .1f, jumpableGround);
    }

    

    public bool CheckInventory()
    {
        int total = 0;
        bool full = false;
        for (int i = 0; i < inventory.Length; i++)
        {
            total += inventory[i];
        }
        if (total == inventorySize)
        {
            full = true;
        }
        return full;
    }

}
