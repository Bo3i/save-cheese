using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic;

    private ItemCollector itemCollectorP1;
    private ItemCollector itemCollectorP2;

    private TextMeshProUGUI lostText;

    private TextMeshProUGUI P1Name;
    private TextMeshProUGUI P2Name;

    private GameObject restartButton;
    private GameObject mainMenuButton;

    public GameObject pauseText;
    public GameObject PauseButtonBack;
    public GameObject PauseButtonSettings;
    public GameObject PauseButtonRestart;
    public GameObject PauseButtonResume;
    public GameObject PauseButton;

    public GameObject PauseButtonSettingsBack;

    private GameObject musicVol;
    private GameObject musicVolumeSlider;
    private GameObject FXVol;
    private GameObject SFXVolumeSlider;


    private Image[] fuelCheesesP1;
    private Image[] materialCheesesP1;
    private Image[] miceP1;

    private Image[] fuelCheesesP2;
    private Image[] materialCheesesP2;
    private Image[] miceP2;

    private Image[][] player1UI;
    private Image[][] player2UI;

    private enum ItemType { fuellCheese, materialCheese, mice }




    private void Start()
    {
        Time.timeScale = 1;
        OnStartUI();
        for (int i = 0; i < fuelCheesesP1.Length; i++)
        {
            fuelCheesesP1[i].enabled = false;
            materialCheesesP1[i].enabled = false;
            miceP1[i].enabled = false;
            fuelCheesesP2[i].enabled = false;
            materialCheesesP2[i].enabled = false;
            miceP2[i].enabled = false;
        }
        SoundManager.instance.PlayMusic(backgroundMusic, GameInfo.musicVolume);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (itemCollectorP1 != null)
        {
            UpdateInventoryUI(itemCollectorP1, player1UI);
        }
        else
        {
            OnPlayerSpawn();
        }
        if(itemCollectorP2 != null)
        {
            UpdateInventoryUI(itemCollectorP2, player2UI);
        }
        else
        {
            OnPlayerSpawn();
        }
        if(GameInfo.lost)
        {
            lostText.enabled = true;
            restartButton.SetActive(true);
            mainMenuButton.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                Resume();
                musicVol.SetActive(false);
                musicVolumeSlider.SetActive(false);
                FXVol.SetActive(false);
                SFXVolumeSlider.SetActive(false);
                PauseButtonSettingsBack.SetActive(false);
            }
            else
            {
                Pause();
            }
        }
        
    }

    private void OnPlayerSpawn()
    {
        if(itemCollectorP1 == null)
        {
            GameObject p1 = GameObject.Find("Player1");
            if(p1 != null)
            {
                itemCollectorP1 = p1.GetComponent<ItemCollector>();
                P1Name.text = GameInfo.player1Name;
            }
        }
        if(itemCollectorP2 == null)
        {
            GameObject p2 = GameObject.Find("Player2");
            if(p2 != null)
            {
                itemCollectorP2 = p2.GetComponent<ItemCollector>();
                P2Name.text = GameInfo.player2Name;
            }
        }
    }


    private Image[] GetImagesWithTag(string tag)
    {
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
        List<Image> imageList = new List<Image>();
        foreach (GameObject obj in objectsWithTag)
        {
            imageList.AddRange(obj.GetComponentsInChildren<Image>());
        }
        return imageList.ToArray();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseText.SetActive(true);
        PauseButtonBack.SetActive(true);
        PauseButtonSettings.SetActive(true);
        PauseButtonRestart.SetActive(true);
        PauseButtonResume.SetActive(true);
        PauseButton.SetActive(false);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseText.SetActive(false);
        PauseButtonBack.SetActive(false);
        PauseButtonSettings.SetActive(false);
        PauseButtonRestart.SetActive(false);
        PauseButtonResume.SetActive(false);
        PauseButton.SetActive(true);
    }

    public void Settings()
    {
        musicVol.SetActive(true);
        musicVolumeSlider.SetActive(true);
        FXVol.SetActive(true);
        SFXVolumeSlider.SetActive(true);
        PauseButtonSettingsBack.SetActive(true);

        pauseText.SetActive(false);
        PauseButtonBack.SetActive(false);
        PauseButtonSettings.SetActive(false);
        PauseButtonRestart.SetActive(false);
        PauseButtonResume.SetActive(false);
    }

    public void SettingsBack()
    {
        musicVol.SetActive(false);
        musicVolumeSlider.SetActive(false);
        FXVol.SetActive(false);
        SFXVolumeSlider.SetActive(false);
        PauseButtonSettingsBack.SetActive(false);

        pauseText.SetActive(true);
        PauseButtonBack.SetActive(true);
        PauseButtonSettings.SetActive(true);
        PauseButtonRestart.SetActive(true);
        PauseButtonResume.SetActive(true);
    }

    private void OnStartUI()
    {
        fuelCheesesP1 = GetImagesWithTag("FuelP1");
        materialCheesesP1 = GetImagesWithTag("ResourceP1");
        miceP1 = GetImagesWithTag("MouseP1");

        fuelCheesesP2 = GetImagesWithTag("FuelP2");
        materialCheesesP2 = GetImagesWithTag("ResourceP2");
        miceP2 = GetImagesWithTag("MouseP2");

        P1Name = GameObject.Find("Player1Name").GetComponent<TextMeshProUGUI>();
        P2Name = GameObject.Find("Player2Name").GetComponent<TextMeshProUGUI>();

        lostText = GameObject.Find("LostText").GetComponent<TextMeshProUGUI>();
        lostText.enabled = false;

        restartButton = GameObject.Find("Play");
        restartButton.SetActive(false);

        mainMenuButton = GameObject.Find("Back");
        mainMenuButton.SetActive(false);

        pauseText = GameObject.Find("PauseText");
        pauseText.SetActive(false);

        PauseButtonBack = GameObject.Find("PauseButtonBack");
        PauseButtonBack.SetActive(false);

        PauseButtonSettings = GameObject.Find("PauseButtonSettings");
        PauseButtonSettings.SetActive(false);

        PauseButtonRestart = GameObject.Find("PauseButtonRestart");
        PauseButtonRestart.SetActive(false);

        PauseButtonResume = GameObject.Find("PauseButtonResume");
        PauseButtonResume.SetActive(false);

        PauseButton = GameObject.Find("Pause");

        musicVol = GameObject.Find("MusicVol");
        musicVol.SetActive(false);

        musicVolumeSlider = GameObject.Find("MusicVolumeSlider");
        musicVolumeSlider.GetComponent<Slider>().value = GameInfo.musicVolume;
        musicVolumeSlider.SetActive(false);

        PauseButtonSettingsBack = GameObject.Find("PauseButtonSettngsBack");
        PauseButtonSettingsBack.SetActive(false);

        FXVol = GameObject.Find("FXVol");
        SFXVolumeSlider = GameObject.Find("SFXVolumeSlider");
        SFXVolumeSlider.GetComponent<Slider>().value = GameInfo.sFXVolume;
        SFXVolumeSlider.SetActive(false);
        FXVol.SetActive(false);

        player1UI = new Image[][] { fuelCheesesP1, materialCheesesP1, miceP1 };
        player2UI = new Image[][] { fuelCheesesP2, materialCheesesP2, miceP2 };
    }

    public void UpdateInventoryUI(ItemCollector player, Image[][] playerUI)
    {
        for (int i = 0; i < playerUI.Length; i++)
        {
            EnableImage(playerUI[i], player.inventory[i]);
        }
    }

    private void EnableImage(Image[] images, int amount)
    {
        for(int i = 0; i < images.Length; i++)
        {
            if(i < amount)
            {
                images[i].enabled = true;
            }
            else
            {
                images[i].enabled = false;
            }
        }
    }

    public void MusicVolume()
    {
        GameInfo.musicVolume = GameObject.Find("MusicVolumeSlider").GetComponent<Slider>().value;
    }

    public void SFXVolume()
    {
        GameInfo.sFXVolume = GameObject.Find("SFXVolumeSlider").GetComponent<Slider>().value;
    }

    public void Restart()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        GameInfo.isPlayer2Playing = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
