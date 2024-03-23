using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] private AudioSource sFXObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        AudioSource aS = Instantiate(sFXObject, Camera.main.transform.position, Quaternion.identity);
        aS.clip = clip;
        aS.volume = volume; 
        aS.Play();
        Destroy(aS.gameObject, clip.length);
    }

    public void PlayMusic(AudioClip clip, float volume)
    {
        AudioSource aS = Instantiate(sFXObject, Camera.main.transform.position, Quaternion.identity);
        aS.clip = clip;
        aS.volume = volume;
        aS.loop = true;
        aS.time = GameInfo.musicTime;
        aS.Play();
    }
}
