using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerStateMachine : MonoBehaviour
{
    public Transform cameraObject;
    public Transform[] _PlayerTrack;
    public Animator CameraFadeAnimator;
    public GameManagerStateMachine gameManager;
    public MenuManager menuManager;
    public SettingsMenu settingsMenu;
    public RebindUI rebindUI;
    [Header("Player Walk Variables")]
    public Vector3 moveDirection;
    public float movementSpeed = 10;

    [Header("Player Interact Variables")]
    //public TMP_Text InteractPromptText;
    //public Animator InteractPromptTextAnim;
    public float rayLength = 2;
    public LayerMask layerMaskInteract;
    public List<GameObject> _InteractablesInRubber;
    public List<GameObject> _InteractablesInRuler;
    public List<GameObject> _InteractablesInPencil;

    [Header("Character Values")]
    [Range(0, 2)]
    public int CharacterType;
    public bool _IsRubber;
    public bool _IsRuler;
    public bool _IsPencil;
    public bool ShouldChangeCharacter;
    public bool FinishTransform;
    public CharacterController[] characterControllers;
    public Sprite[] characterSprites;

    [Header("Rubber")]
    public CharacterController rubberController;

    [Header("Ruler")]
    public CharacterController rulerController;

    [Header("Pencil")]
    public CharacterController pencilController;


    [Header("Input Start Up")]
    public PlayerInputSystem PlayerInput;

    [Header("Movement Controls")]
    Vector2 movementInput;
    public InputActionReference walkActionReference;
    [Header("Camera Controls")]
    Vector2 cameraInput;
    [Header("Camera Values")]
    public float cameraInputX;
    public float cameraInputY;
    public float _SensitivityMultiplier;
    [Header("Interact Button")]
    public bool IsInteractPressed;
    [Header("Switch Button")]
    public bool IsSwitchPressed;
    [Header("Menu Open Close Button")]
    public bool IsMenuOpenClosePressed;

    [Header("Vert/Hor Input")]
    public float vertInput;
    public float horInput;

    PlayerBaseState currentState;
    PlayerStateFactory states;

    public PlayerBaseState CurrentState { get { return currentState;} set { currentState = value; } }

    private void Awake()
    {
        menuManager = FindObjectOfType<MenuManager>();
        settingsMenu = FindObjectOfType<SettingsMenu>();
        gameManager = FindObjectOfType<GameManagerStateMachine>();
        cameraObject = Camera.main.transform;
        states = new PlayerStateFactory(this);
        currentState = states.Idle();
        currentState.EnterState();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        currentState.UpdateStates();
        CheckCurrentCharacter();
        IsInteractPressed = false;
        TurnSpritesToCamera();
    }

    private void FixedUpdate()
    {
        MoveInputHandler();
    }

    public string ReturnCharacterName()
    {
        switch (CharacterType)
        {
            case 0:
                return "rubber";
            case 1:
                return "ruler";
            case 2:
                return "pencil";
        }
        return "null";
    }

    private void CheckCurrentCharacter()
    {
        CheckForSwitch();
    }
    private void CheckForSwitch()
    {
        if (IsSwitchPressed)
        {
            IsSwitchPressed = false;
            ShouldChangeCharacter = true;
        }
        if (ShouldChangeCharacter)
        {
            CameraFadeAnimator.SetBool("Switch", true);
            if (FinishTransform)
            {
                CameraFadeAnimator.SetBool("Switch", false);
                ChangeCharacter();
            }
        }
    }

    private void ChangeCharacter()
    {
        if (CharacterType == 2)
        {
            CharacterType = 0;
        }
        else
        {
            CharacterType++;
        }
        int camIndex = -1;
        foreach(CinemachineVirtualCamera camera in gameManager.Cameras)
        {
            camera.Priority = 10;
            if (camIndex == CharacterType)
            {
                camera.Priority = 11;
            }
        }
        AdjustValues();
    }

    private void AdjustValues()
    {
        _IsRubber = false;
        _IsRuler = false;
        _IsPencil = false;
        switch (CharacterType)
        {
            case 0:
                _IsRubber = true;
                break;
            case 1:
                _IsRuler = true;
                break;
            case 2:
                _IsPencil = true;
                break;
        }
    }

    private void TurnSpritesToCamera()
    {
        foreach(Sprite sprite in characterSprites)
        {
            GameObject spriteObj = sprite.GetComponent<GameObject>();
            spriteObj.transform.LookAt(Camera.main.transform);
        }
    }


    private void OnEnable()
    {
        PlayerInput = InputManager.PlayerInput;
        PlayerInput.Enable();
        PlayerInput.Main.Movement.performed += OnMove;
        PlayerInput.Main.Movement.canceled += OnMove;
        PlayerInput.Main.Turn.performed += OnTurn;
        PlayerInput.Main.Turn.canceled += OnTurn;
        PlayerInput.Main.Interact.started += OnInteract;
        PlayerInput.Main.Switch.started += OnSwitch;
        PlayerInput.Main.MenuOpenClose.started += OnMenuOpenClose;
    }

    private void OnDisable()
    {
        PlayerInput = InputManager.PlayerInput;
        PlayerInput.Disable();
        PlayerInput.Main.Movement.performed -= OnMove;
        PlayerInput.Main.Movement.canceled -= OnMove;
        PlayerInput.Main.Turn.performed -= OnTurn;
        PlayerInput.Main.Turn.canceled -= OnTurn;
        PlayerInput.Main.Interact.started -= OnInteract;
        PlayerInput.Main.Switch.started -= OnSwitch;
        PlayerInput.Main.MenuOpenClose.started += OnMenuOpenClose;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }

    private void OnTurn(InputAction.CallbackContext ctx)
    {
        cameraInput = ctx.ReadValue<Vector2>();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        IsInteractPressed = ctx.ReadValueAsButton();
    }
    private void OnSwitch(InputAction.CallbackContext ctx)
    {
        IsSwitchPressed = ctx.ReadValueAsButton();
    }
    private void OnMenuOpenClose(InputAction.CallbackContext ctx)
    {
        IsMenuOpenClosePressed = ctx.ReadValueAsButton();
    }

    private void MoveInputHandler()
    {
        vertInput = movementInput.y;
        horInput = movementInput.x;
        cameraInputY = cameraInput.y * settingsMenu._SensitivityMultiplier;
        cameraInputX = cameraInput.x * settingsMenu._SensitivityMultiplier;
    }
}
