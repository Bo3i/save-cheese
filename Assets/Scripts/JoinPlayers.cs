using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinPlayers : MonoBehaviour
{
    [SerializeField] private GameObject player1;

    public void Start() 
    {
        GameObject p1 = Instantiate(player1, new Vector3(5, 0, 0), Quaternion.identity);
        p1.GetComponent<PlayerInput>().defaultControlScheme = "Player1";
        p1.name = "Player1";
        p1.GetComponent<SpriteRenderer>().color = GameInfo.player1Color;
        if (GameInfo.isPlayer2Playing)
        {
            GameObject p2 = Instantiate(player1, new Vector3(5, 0, 0), Quaternion.identity);
            p2.GetComponent<SpriteRenderer>().color = GameInfo.player2Color;
            p2.name = "Player2";
            p2.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Player2", Keyboard.current);
        }
        GameInfo.lost = false;
    }
}
