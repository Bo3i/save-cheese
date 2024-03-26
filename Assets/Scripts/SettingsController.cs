using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public GameObject sliderVolMusic;
    public GameObject sliderVolSFX;

    public void Awake()
    {
        sliderVolMusic = GameObject.Find("MusicVolumeSlider");
        sliderVolSFX = GameObject.Find("SFXVolumeSlider");
    }

    void Start()
    {
        sliderVolMusic.GetComponent<Slider>().value = GameInfo.musicVolume;
        sliderVolSFX.GetComponent<Slider>().value = GameInfo.sFXVolume;
    }
  
    public void MusicVolume()
    {
        GameInfo.musicVolume = sliderVolMusic.GetComponent<Slider>().value;
    }

    public void SFXVolume()
    {
        GameInfo.sFXVolume = sliderVolSFX.GetComponent<Slider>().value;
    }

    public void SetMusicTime()
    {
        GameInfo.musicTime = GameObject.Find("SFXObject(Clone)").GetComponent<AudioSource>().time;
    }
}
