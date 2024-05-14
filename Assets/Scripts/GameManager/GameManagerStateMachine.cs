using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

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
    }
    private void Update()
    {
        currentState.UpdateStates();
        //if (_Paused || !_PlayingGame)
        //{
        //    _CanMove = false;
        //}
        //if (!_CanMove ) { }
    }
}
