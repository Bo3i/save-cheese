using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text inventoryUI;
    [SerializeField] private Text playerPos;
    [SerializeField] private Image[] fuelCheeses;
    [SerializeField] private Image[] materialCheeses;
    [SerializeField] private Image[] mice;

    private Rigidbody2D rb;
    
    public int[] inventory = new int[3];
    private int inventorySize = 3;
    private enum ItemType { fuellCheese, materialCheese, mice }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory[(int)ItemType.fuellCheese] = 0;
        inventory[(int)ItemType.materialCheese] = 0;
        inventory[(int)ItemType.mice] = 0;
        for (int i = 0; i < fuelCheeses.Length; i++)
        {
            fuelCheeses[i].enabled = false;
            materialCheeses[i].enabled = false;
            mice[i].enabled = false;
        }
    }

    private void Update()
    {
        playerPos.text = "X: " + (int)transform.position.x + " Y: " + (int)transform.position.y + " Vel: " + rb.velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!CheckInventory() && inventory[(int)ItemType.fuellCheese] == 0 && inventory[(int)ItemType.fuellCheese] == 0 && collision.gameObject.CompareTag("Mouse"))
        {
                Destroy(collision.gameObject);
                inventory[(int)ItemType.mice]++;
                UpdateInventoryUI();
        }
        
    }

    public void UpdateInventory(TileBase tile)
    {
        if (!CheckInventory())
        {
            if (inventory[(int)ItemType.materialCheese] == 0 && tile.name == "fuellCheese")
            {
                inventory[(int)ItemType.fuellCheese]++;
                UpdateInventoryUI();
            }
            else if (inventory[(int)ItemType.fuellCheese] == 0 && tile.name == "materialCheese")
            {
                inventory[(int)ItemType.materialCheese]++;
                UpdateInventoryUI();
            }
            else if ((tile.name == "fuellCheese" && inventory[(int)ItemType.materialCheese] != 0) || (tile.name == "materialCheese" && inventory[(int)ItemType.fuellCheese] != 0))
            {
                inventoryUI.text = "Can't pick up two different resources!";
            }
        }
        else if(inventoryUI.text != "Can't pick up two different resources!")
        {
            inventoryUI.text = "Inventory Full!";
        }
    }

   

    public void UpdateInventoryUI()
    {
        if (inventory[(int)ItemType.fuellCheese] > 0)
        {
            for (int i = 0; i < inventory[(int)ItemType.fuellCheese]; i++)
            {
                fuelCheeses[i].enabled = true;
            }
        }
        else if (inventory[(int)ItemType.materialCheese] > 0)
        {
            for (int i = 0; i < inventory[(int)ItemType.materialCheese]; i++)
            {
                materialCheeses[i].enabled = true;
            }
        }
        else if (inventory[(int)ItemType.mice] > 0)
        {
            for (int i = 0; i < inventory[(int)ItemType.mice]; i++)
            {
                mice[i].enabled = true;
            }
        }
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
