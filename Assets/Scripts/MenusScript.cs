using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenusScript : MonoBehaviour
{
    public string gameScene;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    private bool settingsActive;
    private bool creditsActive;

    public AudioMixer mainMixer;

    private Resolution[] screenRes;
    public Dropdown resDropdown;

    // Start is called before the first frame update
    void Start()
    {
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
        resDropdown.value = currentResIndex;
        resDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("MainVolume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resIndex)
    {
        Resolution resolution = screenRes[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void StartButton()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void SettingsAndBackButton()
    {
        settingsActive = !settingsActive;
        if (settingsActive == true)
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }
        if (settingsActive == false)
        {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
        }
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
        Application.OpenURL("");
    }
    public void GuilhermeButton()
    {
        Application.OpenURL("https://itch.io/profile/gr-124");
    }
    public void RenaeButton()
    {
        Application.OpenURL("");
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
