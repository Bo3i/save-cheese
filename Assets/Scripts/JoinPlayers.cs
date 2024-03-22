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
            GameObject p2 = Instantiate(player1, new Vector3(5, 0, 0), Quaternion.identity);
            p2.GetComponent<SpriteRenderer>().color = new Color32(0xCC, 0xFF, 0xAF, 0xFF);
            p2.GetComponent<PlayerInput>().defaultActionMap = "Player1";
            p2.tag = "Player1";
            Debug.Log("Player 2 joined");
            GameObject.Find("JoinGameButton").SetActive(false);
            //Instantiate(player1, new Vector3(-5, 0, 0), Quaternion.identity);
            Debug.Log("Player 1 joined");
        }
        else
        {
            
        }
    }
}
