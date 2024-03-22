using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinPlayers : MonoBehaviour
{
    [SerializeField] private GameObject player1;

    public void OnButtonPress() 
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            
            GameObject p1 = Instantiate(player1, new Vector3(5, 0, 0), Quaternion.identity);
            p1.GetComponent<PlayerInput>().defaultControlScheme = "Player1";
            p1.name = "Player1";
            p1.GetComponent<SpriteRenderer>().color = new Color32(0x00, 0x00, 0xFF, 0xAF);
            Debug.Log("Player 1 joined");
        }
        else
        {
            GameObject p2 = Instantiate(player1, new Vector3(5, 0, 0), Quaternion.identity);
            p2.GetComponent<SpriteRenderer>().color = new Color32(0xCC, 0xFF, 0xAF, 0xFF);
            p2.name = "Player2";
            p2.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Player2", Keyboard.current);
            Debug.Log("Player 2 joined");
            GameObject.Find("JoinGameButton").SetActive(false);
        }
    }
}
