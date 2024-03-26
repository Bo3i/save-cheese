using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] public AudioClip backgroundMusic;

    public void Start()
    {
        SoundManager.instance.PlayMusic(backgroundMusic, GameInfo.musicVolume);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
