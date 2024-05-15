using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManagerStateMachine : MonoBehaviour
{
    GameManagerBaseState currentState;
    GameManagerStateFactory states;
    public GameManagerBaseState CurrentState { get { return currentState; } set { currentState = value; } }

    [Header("Global Values")]
    public MenuManager menuManager;
    public GameObject _CameraTrack;
    //public AudioManager audioManager;
    public GameObject Player;
    public bool _Paused; //checks if the game is paused
    public bool _PlayingGame; //checks if the game is being played or not (will be in menu if not being played)
    public bool _CanMove;
    public Vector3 playerPosition; //*

    public TMP_Text TaskText;

    public float _GameplayTimer;
    public float _MaxTimer = 150f;
    public Image TimerImage;
    public bool CopSummoned;
    public bool CharacterSacrificed;

    [Header("Cinemachine")]
    public CinemachineVirtualCamera menuCamera;
    public CinemachineVirtualCamera rubberCamera;
    public CinemachineVirtualCamera rulerCamera;
    public CinemachineVirtualCamera pencilCamera;
    public List<CinemachineVirtualCamera> Cameras;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        states = new GameManagerStateFactory(this);
        //_Paused = true;
    }

    private void Start()
    {
        _PlayingGame = true;
        _CanMove = true;
        menuManager = GameObject.FindObjectOfType<MenuManager>();
        //audioManager = GameObject.FindObjectOfType<AudioManager>();
        menuCamera = GameObject.FindGameObjectWithTag("MenuCamera").GetComponent<CinemachineVirtualCamera>();
        rubberCamera = GameObject.FindGameObjectWithTag("RubberCamera").GetComponent<CinemachineVirtualCamera>();
        rulerCamera = GameObject.FindGameObjectWithTag("RulerCamera").GetComponent<CinemachineVirtualCamera>();
        pencilCamera = GameObject.FindGameObjectWithTag("PencilCamera").GetComponent<CinemachineVirtualCamera>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Cameras.Add(menuCamera);
        Cameras.Add(rubberCamera);
        Cameras.Add(rulerCamera);
        Cameras.Add(pencilCamera);
        currentState = states.Menu();
        currentState.EnterState();
        _GameplayTimer = 0;
    }
    private void Update()
    {
        currentState.UpdateStates();
        //if (_Paused || !_PlayingGame)
        //{
        //    _CanMove = false;
        //}
        //if (!_CanMove ) { }
        GameplayTimer();
    }

    private void GameplayTimer()
    {
        if (_GameplayTimer == 0)
        {
            TaskText.text = "Lock doors in the bank before the Cops arrive!";
        }
        CopSpawned();
        PauseTimerForSacrifice();
        TimerEndReached();
        TimerImage.fillAmount = _GameplayTimer / _MaxTimer;
    }

    private void CopSpawned()
    {
        if (_GameplayTimer >= 30 && !CopSummoned)
        {
            CopSummoned = true;
            TaskText.text = "Defend the vault from the cop!";
            //summon cop
        }
    }
    private void PauseTimerForSacrifice()
    {
        if (_GameplayTimer >= 120 && !CharacterSacrificed)
        {
            var VaultScript = FindObjectOfType<VaultScript>();
            if (VaultScript.SacrificeCollider.enabled == false)
            {
                TaskText.text = "Make one character collect treasure!";
                VaultScript.TimerReached();
            }
        }
        else
        {
            _GameplayTimer += Time.deltaTime*10;
        }
    }
    private void TimerEndReached()
    {
        if (_GameplayTimer >= _MaxTimer)
        {
            TaskText.text = "Get out!";
            _GameplayTimer = _MaxTimer;
        }
    }
}
