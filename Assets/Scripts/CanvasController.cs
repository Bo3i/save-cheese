using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{

    public GameObject mainMenuCanvas;
    public GameObject settingsCanvas;
    public GameObject startGameCanvas;


    void Start()
    {
        mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        settingsCanvas = GameObject.Find("SettingsCanvas");
        startGameCanvas = GameObject.Find("StartGameCanvas");
        DisableCanvas(settingsCanvas);
        DisableCanvas(startGameCanvas);
    }

    private void DisableCanvas(GameObject canvas)
    {
        Canvas canvas1 = canvas.GetComponent<Canvas>();
        canvas1.enabled = false;
    }

    private void EnableCanvas(GameObject canvas)
    {
        Canvas canvas1 = canvas.GetComponent<Canvas>();
        canvas1.enabled = true;
    }

    public void OnMainMenu()
    {
        DisableCanvas(settingsCanvas);
        DisableCanvas(startGameCanvas);
        EnableCanvas(mainMenuCanvas);
    }

    public void OnSettings()
    {
        DisableCanvas(mainMenuCanvas);
        DisableCanvas(startGameCanvas);
        EnableCanvas(settingsCanvas);
    }

    public void OnStartGame()
    {
        DisableCanvas(mainMenuCanvas);
        DisableCanvas(settingsCanvas);
        EnableCanvas(startGameCanvas);
    }
}
