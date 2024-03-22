using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    private ItemCollector itemCollectorP1;
    private ItemCollector itemCollectorP2;

    private Image[] fuelCheesesP1;
    private Image[] materialCheesesP1;
    private Image[] miceP1;

    private Image[] fuelCheesesP2;
    private Image[] materialCheesesP2;
    private Image[] miceP2;

    private Image[][] player1UI;
    private Image[][] player2UI;

    private enum ItemType { fuellCheese, materialCheese, mice }


    private void Start()
    {
        OnStartUI();
        for (int i = 0; i < fuelCheesesP1.Length; i++)
        {
            fuelCheesesP1[i].enabled = false;
            materialCheesesP1[i].enabled = false;
            miceP1[i].enabled = false;
            fuelCheesesP2[i].enabled = false;
            materialCheesesP2[i].enabled = false;
            miceP2[i].enabled = false;
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (itemCollectorP1 != null)
        {
            UpdateInventoryUI(itemCollectorP1, player1UI);
        }
        else
        {
            OnPlayerSpawn();
        }
        if(itemCollectorP2 != null)
        {
            UpdateInventoryUI(itemCollectorP2, player2UI);
        }
        else
        {
            OnPlayerSpawn();
        }
        
    }

    private void OnPlayerSpawn()
    {
        if(itemCollectorP1 == null)
        {
            GameObject p1 = GameObject.Find("Player1");
            if(p1 != null)
            {
                itemCollectorP1 = p1.GetComponent<ItemCollector>();
            }
        }
        if(itemCollectorP2 == null)
        {
            GameObject p2 = GameObject.Find("Player2");
            if(p2 != null)
            {
                itemCollectorP2 = p2.GetComponent<ItemCollector>();
            }
        }
    }


    private Image[] GetImagesWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        List<Image> imageList = new List<Image>();
        foreach (GameObject obj in objectsWithTag)
        {
            imageList.AddRange(obj.GetComponentsInChildren<Image>());
        }
        return imageList.ToArray();
    }

    private void OnStartUI()
    {
        fuelCheesesP1 = GetImagesWithTag("FuelP1");
        materialCheesesP1 = GetImagesWithTag("ResourceP1");
        miceP1 = GetImagesWithTag("MouseP1");

        fuelCheesesP2 = GetImagesWithTag("FuelP2");
        materialCheesesP2 = GetImagesWithTag("ResourceP2");
        miceP2 = GetImagesWithTag("MouseP2");

        player1UI = new Image[][] { fuelCheesesP1, materialCheesesP1, miceP1 };
        player2UI = new Image[][] { fuelCheesesP2, materialCheesesP2, miceP2 };
    }

    public void UpdateInventoryUI(ItemCollector player, Image[][] playerUI)
    {
        for (int i = 0; i < playerUI.Length; i++)
        {
            EnableImage(playerUI[i], player.inventory[i]);
        }
    }

    private void EnableImage(Image[] images, int amount)
    {
        for(int i = 0; i < images.Length; i++)
        {
            if(i < amount)
            {
                images[i].enabled = true;
            }
            else
            {
                images[i].enabled = false;
            }
        }
    }
}
