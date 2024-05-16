using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenusScript : MonoBehaviour
{
    private static MenusScript _instance;
    public static MenusScript Instance { get { return _instance; } }

    public string gameScene;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    private bool settingsActive;
    private bool creditsActive;

    public AudioMixer mainMixer;
    public Slider volumeSlider;

    public Toggle fullscreenToggle;

    private Resolution[] screenRes;
    private Resolution currentRes;
    public Dropdown resDropdown;
    private float resIndexDebug;

    public float sensitivity;
    public Slider sensitivitySlider;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        screenRes = Screen.resolutions;

        resDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < screenRes.Length; i++)
        {
            string option = screenRes[i].width + " x " + screenRes[i].height;
            options.Add(option);

            if (screenRes[i].width == Screen.currentResolution.width && screenRes[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resDropdown.AddOptions(options);
        if (PlayerPrefs.HasKey("Resolution"))
        {
            resDropdown.value = PlayerPrefs.GetInt("Resolution");
        }
        else
        {
            resDropdown.value = currentResIndex;
        }
        resDropdown.RefreshShownValue();

        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        fullscreenToggle.isOn = (PlayerPrefs.GetInt("Fullscreen")!= 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("MainVolume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", (isFullscreen ? 1 : 0));
    }

    public void SetResolution(int resIndex)
    {
        currentRes = screenRes[resIndex];
        Screen.SetResolution(currentRes.width, currentRes.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resIndex);
        Debug.Log(PlayerPrefs.GetInt("Resolution"));
    }

    public void SetSensitivity(float tempSensitivity)
    {
        sensitivity = tempSensitivity;
        Debug.Log(tempSensitivity);
        PlayerPrefs.SetFloat("Sensitivity", tempSensitivity);
    }

    public void CreditsAndBackButton()
    {
        creditsActive = !creditsActive;
        if (creditsActive == true)
        {
            mainMenu.SetActive(false);
            creditsMenu.SetActive(true);
        }
        if (creditsActive == false)
        {
            mainMenu.SetActive(true);
            creditsMenu.SetActive(false);
        }
    }

    public void ConnorButton()
    {
        Application.OpenURL("https://connroo.itch.io/");
    }
    public void BenjaminButton()
    {
        Application.OpenURL("https://benjamin-game-design.itch.io/");
    }
    public void MartonButton()
    {
        Application.OpenURL("https://phearo01.itch.io/");
    }
    public void GuilhermeButton()
    {
        Application.OpenURL("https://itch.io/profile/gr-124");
    }
    public void RenaeButton()
    {
        Application.OpenURL("https://sergioisntreal.itch.io/");
    }
    public void JanaButton()
    {
        Application.OpenURL("https://janaradulaski.itch.io/");
    }

    public void QuitButton()
    {
        Debug.LogWarning("Application has been closed!");
        Application.Quit();
    }
}
