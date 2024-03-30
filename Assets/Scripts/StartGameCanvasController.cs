using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameCanvasController : MonoBehaviour
{
    public GameObject player2Name;
    public GameObject removePlayer2Button;
    public GameObject playerButton;
    public GameObject player2Color;
    public GameObject player2Color1;
    public GameObject player2Color2;
    public GameObject player1Image;
    public GameObject player2Image;


    public UnityEngine.Color[] colors = { new Color32(0xFF, 0x7E, 0x70, 0xFF), new Color32(0xE4, 0xFF, 0x70, 0xFF), new Color32(0x70, 0xC3, 0xFF, 0xFF) };

    private void Awake()
    {
        player2Name = GameObject.Find("Player2Name");
        removePlayer2Button = GameObject.Find("RemovePlayer2Button");
        playerButton = GameObject.Find("Player2Button");
        player2Color = GameObject.Find("ColorP2");
        player2Color1 = GameObject.Find("ColorP2 (1)");
        player2Color2 = GameObject.Find("ColorP2 (2)");
        player1Image = GameObject.Find("Player1Image");
        player2Image = GameObject.Find("Player2Image");
    }
    void Start()
    {
        player1Image.GetComponent<Image>().color = GameInfo.player1Color;
        player2Image.GetComponent<Image>().color = GameInfo.player2Color;
        player2Name.SetActive(false);
        removePlayer2Button.SetActive(false);
        player2Color.SetActive(false);
        player2Color1.SetActive(false);
        player2Color2.SetActive(false);
    }

    public void Play()
    {
        try
        {
            UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Level");

        }
        catch
        {
            Debug.Log("No level scene to unload");
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level");
    }

    public void AddPlayer2()
    {
        GameInfo.isPlayer2Playing = true;
        GameObject.Find("Player2Image").GetComponent<Image>().enabled = true;
        if (GameInfo.player1Color != colors[0])
        {
            GameInfo.player2Color = colors[0];
            GameObject.Find("Player2Image").GetComponent<Image>().color = colors[0];
        }
        else if (GameInfo.player1Color != colors[1])
        {
            GameInfo.player2Color = colors[1];
            GameObject.Find("Player2Image").GetComponent<Image>().color = colors[1];
        }
        else if (GameInfo.player1Color != colors[2])
        {
            GameInfo.player2Color = colors[2];
            GameObject.Find("Player2Image").GetComponent<Image>().color = colors[2];
        }

        removePlayer2Button.SetActive(true);
        player2Name.SetActive(true);
        playerButton.SetActive(false);
        player2Color.SetActive(true);
        player2Color1.SetActive(true);
        player2Color2.SetActive(true);
    }

    public void RemovePlayer2()
    {
        GameInfo.isPlayer2Playing = false;
        GameInfo.player2Name = "Player 2";
        GameInfo.player2Color = new Color32(0, 0, 0, 0);
        GameObject.Find("Player2Image").GetComponent<Image>().enabled = false;
        player2Name.SetActive(false);
        removePlayer2Button.SetActive(false);
        playerButton.SetActive(true);
        player2Color.SetActive(false);
        player2Color1.SetActive(false);
        player2Color2.SetActive(false);
    }

    public void SetPlayer1Name(string name)
    {
        GameInfo.player1Name = name;
    }

    public void SetPlayer2Name(string name)
    {
        GameInfo.player2Name = name;
    }
    public void SetPlayer1Color(int color)
    {
        if (GameInfo.player2Color != colors[color])
        {
            GameObject.Find("Player1Image").GetComponent<Image>().color = colors[color];
            GameInfo.player1Color = colors[color];
        }
    }

    public void SetPlayer2Color(int color)
    {
        if (GameInfo.player1Color != colors[color])
        {
            GameObject.Find("Player2Image").GetComponent<Image>().color = colors[color];
            GameInfo.player2Color = colors[color];
        }
    }

    public void SetPlayer1Color1()
    {
        SetPlayer1Color(0);
    }
    public void SetPlayer1Color2()
    {
        SetPlayer1Color(1);

    }
    public void SetPlayer1Color3()
    {
        SetPlayer1Color(2);
    }

    public void SetPlayer2Color1()
    {
        SetPlayer2Color(0);
    }
    public void SetPlayer2Color2()
    {
        SetPlayer2Color(1);
    }
    public void SetPlayer2Color3()
    {
        SetPlayer2Color(2);
    }
}
