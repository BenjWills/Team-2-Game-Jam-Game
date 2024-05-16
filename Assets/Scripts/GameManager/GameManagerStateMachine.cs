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
    PlayerStateMachine playerStateMachine;
    //public AudioManager audioManager;
    public GameObject Player;
    public bool _Paused; //checks if the game is paused
    public bool _PlayingGame; //checks if the game is being played or not (will be in menu if not being played)
    public bool _CanMove;
    public Vector3 playerPosition; //*
    public GameObject CopPrefab;
    public Transform CopSummonSpot;

    public int CharactersEscaped;
    public int CharactersCaught;
    public int CharactersRemaining;



    public TMP_Text TaskText;

    public float _GameplayTimer;
    public float _MaxTimer = 150f;
    public Image TimerImage;
    public bool CopSummoned;
    public bool CharacterSacrificed;

    public GameObject Treasure;
    public Animator VaultDoor;

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
        menuManager = GameObject.FindObjectOfType<MenuManager>();
        
        //audioManager = GameObject.FindObjectOfType<AudioManager>();
        
        
        currentState = states.Menu();
        currentState.EnterState();
        _GameplayTimer = 0;
    }

    public void LoadIntoGame()
    {
        playerStateMachine = GameObject.FindObjectOfType<PlayerStateMachine>();
        menuCamera = GameObject.FindGameObjectWithTag("MenuCamera").GetComponent<CinemachineVirtualCamera>();
        rubberCamera = GameObject.FindGameObjectWithTag("RubberCamera").GetComponent<CinemachineVirtualCamera>();
        rulerCamera = GameObject.FindGameObjectWithTag("RulerCamera").GetComponent<CinemachineVirtualCamera>();
        pencilCamera = GameObject.FindGameObjectWithTag("PencilCamera").GetComponent<CinemachineVirtualCamera>();
        Cameras.Add(menuCamera);
        Cameras.Add(rubberCamera);
        Cameras.Add(rulerCamera);
        Cameras.Add(pencilCamera);
        CopSummonSpot = GameObject.FindGameObjectWithTag("CopSummonPoint").transform;
        TaskText = GameObject.FindGameObjectWithTag("TimerText").GetComponent<TMP_Text>();
        TimerImage = GameObject.FindGameObjectWithTag("TimerImage").GetComponent<Image>();
        Player = GameObject.FindGameObjectWithTag("Player");
        VaultDoor = GameObject.FindGameObjectWithTag("Vault").GetComponent<Animator>();
        playerStateMachine.characterControllers[0].transform.position = new Vector3(0, .57f, -2);
        playerStateMachine.characterControllers[1].transform.position = new Vector3(-2.5f, .57f, -2);
        playerStateMachine.characterControllers[2].transform.position = new Vector3(2.5f, .57f, -2);
        Treasure = GameObject.FindGameObjectWithTag("Treasure");
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

    public void GameplayTimer()
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
            Vector3 pos = new Vector3(0f, 0f, 14.5f);
            Vector3 rot = new Vector3(0f, 0f, 180f);
            Instantiate(CopPrefab,pos,Quaternion.Euler(rot));
            CopSummoned = true;
            TaskText.text = "Defend the vault from the cop!";
            //summon cop
            playerStateMachine.aIScript = FindObjectOfType<AIScript>();
        }
    }
    private void PauseTimerForSacrifice()
    {
        if (_GameplayTimer >= 120 && !CharacterSacrificed)
        {
            var VaultScript = FindObjectOfType<VaultScript>();
            if (VaultScript.SacrificeCollider.enabled == false)
            {
                VaultDoor.SetTrigger("vaultOpening");
                TaskText.text = "Make one character collect treasure!";
                VaultScript.TimerReached();
            }
        }
        else
        {
            _GameplayTimer += Time.deltaTime;
        }
    }
    private void TimerEndReached()
    {
        if (_GameplayTimer >= _MaxTimer)
        {
            if (Treasure!=null)
            {
                playerStateMachine._RubberTaken = false;
                playerStateMachine._RulerTaken = false;
                playerStateMachine._PencilTaken = false;
                Destroy(Treasure);
            }
            var VentScript = FindObjectOfType<VentScript>();
            VentScript.VentActivated();
            TaskText.text = "Get out!";
            _GameplayTimer = _MaxTimer;
        }
    }
}
