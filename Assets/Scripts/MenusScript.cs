using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenusScript : MonoBehaviour
{
    public string gameScene;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    private bool settingsActive;

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
}
