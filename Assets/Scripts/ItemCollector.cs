using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;
    [SerializeField] private Text inventoryUI;
    [SerializeField] private Text playerPos;
    private Rigidbody2D rb;
    
    private int[] inventory = new int[2];
    private int inventorySize = 3;
    private enum ItemType { fuellCheese, materialCheese }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory[(int)ItemType.fuellCheese] = 0;
        inventory[(int)ItemType.materialCheese] = 0;
    }

    private void Update()
    {
        playerPos.text = "X: " + (int)transform.position.x + " Y: " + (int)transform.position.y + " Vel: " + rb.velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            cherries++;
            //cherriesText.text = "Cherries: " + cherries;
        }
    }

    public void UpdateInventory( TileBase tile)
    {
        if (!CheckInventory())
        {
            if (inventory[(int)ItemType.materialCheese] == 0 && tile.name == "fuellCheese")
            {
                inventory[(int)ItemType.fuellCheese]++;
            }

            else if (inventory[(int)ItemType.fuellCheese] == 0 && tile.name == "materialCheese")
            {
                inventory[(int)ItemType.materialCheese]++;
            }
        }
        inventoryUI.text = "FuellCheese: " + inventory[(int)ItemType.fuellCheese] + ", MaterialCheese: " + inventory[(int)ItemType.materialCheese];
    }

    private bool CheckInventory()
    {
        int total = 0;
        bool full = false;
        for(int i = 0; i < inventory.Length; i++)
        {
            total += inventory[i];
        }
        if(total == inventorySize)
        {
            full = true;
        }
        return full;
    }

    
}
