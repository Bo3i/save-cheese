using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HelpCanvasController : MonoBehaviour
{

    private GameObject HowToCanvas;
    private GameObject HelpCanvas;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Play()
    {
        try
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        catch
        {
            Debug.Log("No level scene to unload");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
