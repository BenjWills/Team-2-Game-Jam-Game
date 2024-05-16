using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class MenuManager : MonoBehaviour
{
    GameManagerStateMachine gameManager;
    [Header("Don't Destroy On Loads")]
    [SerializeField] private GameObject SettingsManager;
    [SerializeField] private GameObject InputManager;
    [SerializeField] private GameObject AudioManager;
    [SerializeField] private GameObject CameraMain;
    [Header("UI Canvases")]
    public GameObject PlayerUI;
    [SerializeField] private GameObject MainMenuCanvas;
    [SerializeField] private GameObject SettingsCanvas;
    [SerializeField] private GameObject PauseCanvas;
    //[SerializeField] private GameObject KeyboardCanvas;
    //[SerializeField] private GameObject GamepadCanvas;
    [SerializeField] private GameObject CreditsCanvas;
    [SerializeField] private GameObject FinishCanvas;
    [SerializeField] private GameObject CaughtCanvas;
    [Header("First Selected Menu Objects")]
    [SerializeField] private GameObject FS_Menu;
    [SerializeField] private GameObject FS_Settings;
    [SerializeField] private GameObject FS_Pause;
    //[SerializeField] private GameObject FS_Keyboard;
    //[SerializeField] private GameObject FS_Gamepad;
    [SerializeField] private GameObject FS_Finish;
    [SerializeField] private GameObject FS_Caught;


    [SerializeField] private GameObject[] Stars;
    [SerializeField] private GameObject[] Scribbles;

    private PlayerStateMachine playerStateMachine;

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (CameraMain)
        DontDestroyOnLoad(CameraMain);
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = true;
        gameManager = FindObjectOfType<GameManagerStateMachine>();
    }

    private void Start()
    {
        //Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(FS_Menu);
    }

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(gameManager.playingGame);
        if (gameManager._PlayingGame)
        {
            if (playerStateMachine != null)
            {
                if (playerStateMachine.IsMenuOpenClosePressed)//pause button is pressed
                {
                    if (PauseCanvas.activeSelf)//if in pause menu, resume game
                    {
                        P_Resume();
                    }
                    else if (gameManager._PlayingGame && !PauseCanvas.activeSelf && !gameManager._Paused)//if in game, and pause menu isnt active, pause game
                    {
                        P_PauseGame();
                    }
                    PlayingGame();
                    DefaultCheckers();
                    playerStateMachine.IsMenuOpenClosePressed = false;
                }
            }
            else
            {
                playerStateMachine = FindObjectOfType<PlayerStateMachine>();
            }
        }
        
    }

    private void DefaultCheckers()
    {
        //if (GamepadCanvas.activeSelf) //if the player is in the gamepad rebinding canvas, go to the settings canvas
        //{
        //    GamepadCanvas.SetActive(false);
        //    SettingsCanvas.SetActive(true);
        //    EventSystem.current.SetSelectedGameObject(FS_Settings);
        //}
        //else if (KeyboardCanvas.activeSelf) //if the player is in the keyboard rebinding canvas, go to the settings canvas
        //{
        //    KeyboardCanvas.SetActive(false);
        //    SettingsCanvas.SetActive(true);
        //    EventSystem.current.SetSelectedGameObject(FS_Settings);
        //}
        if (CreditsCanvas.activeSelf) //if credits canvas is active, go to main menu
        {
            CreditsCanvas.SetActive(false);
            MainMenuCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(FS_Menu);
        }
    }

    private void PlayingGame()
    {
        if(gameManager._PlayingGame)
        {
            if (SettingsCanvas.activeSelf) //if in game and in settings, go to pause menu
            {
                SettingsCanvas.SetActive(false);
                PauseCanvas.SetActive(true);
                EventSystem.current.SetSelectedGameObject(FS_Pause);
            }
        }
        else
        {
            if (SettingsCanvas.activeSelf)
            {
                SettingsCanvas.SetActive(false);
                MainMenuCanvas.SetActive(true);
                EventSystem.current.SetSelectedGameObject(FS_Menu);
            }
        }
    }

    public void Settings()
    {
        //if in game, hide the pause menu and pull up settings
        //if in main menu, hide the menu and pull up settings
        if (PauseCanvas.activeSelf)
        {
            PauseCanvas.SetActive(false);
            SettingsCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(FS_Settings);
        }
        else if(MainMenuCanvas.activeSelf)
        {
            MainMenuCanvas.SetActive(false);
            SettingsCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(FS_Settings);
        }
    }

    public void GoToMenu()
    {
        PlayerUI.SetActive(false);
        playerStateMachine = null;
        SceneManager.LoadScene(0);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0f;
        gameManager._PlayingGame = false;
        gameManager._Paused = true;
        PauseCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(FS_Menu);
    }

    public void Back()
    {
        if (PauseCanvas.activeSelf) //if in game and in pause menu, go to main menu
        {
            //gameManager._PlayingGame = false;
        }
        PlayingGame();
        DefaultCheckers();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameManager._PlayingGame = true;
        gameManager._Paused = false;
        gameManager._CanMove = true;
        PlayerUI.SetActive(true);
        //PlayerUI.SetActive(true);
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        MainMenuCanvas.SetActive(false);
    }

    public void M_Quit()
    {
        Application.Quit();
    }

    //public void S_Keyboard()
    //{
    //    SettingsCanvas.SetActive(false);
    //    KeyboardCanvas.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(FS_Keyboard);
    //}

    //public void S_Gamepad() 
    //{
    //    SettingsCanvas.SetActive(false);
    //    GamepadCanvas.SetActive(true);
    //    EventSystem.current.SetSelectedGameObject(FS_Gamepad);
    //}

    public void P_PauseGame()
    {
        gameManager._Paused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        PlayerUI.SetActive(false);
        PauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(FS_Pause);
    }

    public void P_Resume()
    {
        gameManager._Paused = false;
        PlayerUI.SetActive(true);
        PauseCanvas.SetActive(false);
        gameManager._CanMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void P_Finish(bool escaped)
    {
        gameManager._Paused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        PlayerUI.SetActive(false);
        if (escaped)
        {
            FinishCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(FS_Finish);
            int index = 0;
            foreach(GameObject star in Stars)
            {
                index++;
                if (index<gameManager.CharactersEscaped)
                {
                    star.SetActive(true);
                }
                else
                {
                    star.SetActive(false);
                }
            }
            int scribindex = 0;
            foreach(GameObject scribble in Scribbles)
            {
                scribindex++;
                if (scribindex == 1)
                {
                    scribble.SetActive(playerStateMachine._RubberArrested);
                }
                else if (scribindex == 2)
                {
                    scribble.SetActive(playerStateMachine._RulerArrested);
                }
                else if (scribindex == 3)
                {
                    scribble.SetActive(playerStateMachine._PencilArrested);
                }

            }
        }
        else
        {
            CaughtCanvas.SetActive(true);
            EventSystem.current.SetSelectedGameObject(FS_Caught);
        }
        Time.timeScale = 0f;
    }

}
