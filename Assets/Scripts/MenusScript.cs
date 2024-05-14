using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusScript : MonoBehaviour
{
    public string gameScene;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject creditsMenu;
    private bool settingsActive;
    private bool creditsActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Application.OpenURL("");
    }

    public void QuitButton()
    {
        Debug.LogWarning("Application has been closed!");
        Application.Quit();
    }
}