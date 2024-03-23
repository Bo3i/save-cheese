using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInfo
{
    public static string player1Name = "Player 1";
    public static string player2Name = "Player 2";

    public static bool isPlayer2Playing = false;

    public static Color player1Color = new Color32(0xFF, 0x7E, 0x70, 0xFF);
    public static Color player2Color = new Color32(0xE4, 0xFF, 0x70, 0xFF);

    public static bool lost = false;
}
