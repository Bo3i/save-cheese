using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    private ItemCollector itemCollectorP1;
    private ItemCollector itemCollectorP2;

    private TextMeshProUGUI lostText;

    private TextMeshProUGUI P1Name;
    private TextMeshProUGUI P2Name;

    private GameObject restartButton;
    private GameObject mainMenuButton;

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
        if(GameInfo.lost)
        {
            lostText.enabled = true;
            restartButton.SetActive(true);
            mainMenuButton.SetActive(true);
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
                P1Name.text = GameInfo.player1Name;
            }
        }
        if(itemCollectorP2 == null)
        {
            GameObject p2 = GameObject.Find("Player2");
            if(p2 != null)
            {
                itemCollectorP2 = p2.GetComponent<ItemCollector>();
                P2Name.text = GameInfo.player2Name;
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

        P1Name = GameObject.Find("Player1Name").GetComponent<TextMeshProUGUI>();
        P2Name = GameObject.Find("Player2Name").GetComponent<TextMeshProUGUI>();

        lostText = GameObject.Find("LostText").GetComponent<TextMeshProUGUI>();
        lostText.enabled = false;

        restartButton = GameObject.Find("Play");
        restartButton.SetActive(false);

        mainMenuButton = GameObject.Find("Back");
        mainMenuButton.SetActive(false);

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

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
