using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    ItemCollector pl;
    int[] inventory;
    private enum ItemType { fuellCheese, materialCheese, mice }

    private void Start()
    {
        pl = GameObject.Find("Player").GetComponent<ItemCollector>();
        inventory = pl.inventory;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!pl.CheckInventory() && inventory[(int)ItemType.materialCheese] == 0 && inventory[(int)ItemType.fuellCheese] == 0 && collision.gameObject.CompareTag("Mouse"))
        {
            Destroy(collision.gameObject);
            inventory[(int)ItemType.mice]++;
            pl.UpdateInventoryUI();
        }
        else if (!pl.CheckInventory() && inventory[(int)ItemType.materialCheese] == 0 && inventory[(int)ItemType.mice] == 0 && collision.gameObject.CompareTag("Fuell"))
        {
            Destroy(collision.gameObject);
            inventory[(int)ItemType.fuellCheese]++;
            pl.UpdateInventoryUI();
        }
        else if (!pl.CheckInventory() && inventory[(int)ItemType.mice] == 0 && inventory[(int)ItemType.fuellCheese] == 0 && collision.gameObject.CompareTag("Resource"))
        {
            Destroy(collision.gameObject);
            inventory[(int)ItemType.materialCheese]++;
            pl.UpdateInventoryUI();
        }

    }
    
}