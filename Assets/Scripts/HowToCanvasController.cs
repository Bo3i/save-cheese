using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToCanvasController : MonoBehaviour
{
    private GameObject HowToCanvas;
    private GameObject HelpCanvas;
    // Start is called before the first frame update
    void Start()
    {
        HelpCanvas = GameObject.Find("HelpCanvas");
        HelpCanvas.SetActive(false);
        HowToCanvas = GameObject.Find("HowToCanvas");
        HowToCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        HowToCanvas.SetActive(false);
        HelpCanvas.SetActive(true);
    }
}
