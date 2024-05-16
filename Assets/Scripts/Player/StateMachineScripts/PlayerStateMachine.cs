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
    public AIScript aIScript;
    public MenuManager menuManager;
    public MenusScript settingsMenu;
    public RebindUI rebindUI;
    public Animator[] CharacterAnimators;
    [Header("Player Walk Variables")]
    public Vector3 moveDirection;
    public float movementSpeed = 10;
    public bool _Walking;

    [Header("Player Interact Variables")]
    //public TMP_Text InteractPromptText;
    //public Animator InteractPromptTextAnim;
    public float rayLength = 2;
    public LayerMask layerMaskInteract;
    public List<GameObject> _InteractablesInRubber;
    public List<GameObject> _InteractablesInRuler;
    public List<GameObject> _InteractablesInPencil;
    public bool _Actioning;

    [Header("Character Values")]
    [Range(0, 2)]
    public int CharacterType;
    public bool _IsRubber;
    public bool _IsRuler;
    public bool _IsPencil;
    public bool ShouldChangeCharacter;
    public bool FinishTransform;
    public List<CharacterController> characterControllers;
    public SpriteRenderer[] characterSprites;



    public int amountOfArrested;

    [Header("Rubber")]
    public CharacterController rubberController;
    public bool _RubberTaken;
    public bool _RubberArrested;

    [Header("Ruler")]
    public CharacterController rulerController;
    public bool _RulerTaken;
    public bool _RulerArrested;

    [Header("Pencil")]
    public CharacterController pencilController;
    public bool _PencilTaken;
    public bool _PencilArrested;


    [Header("Input Start Up")]
    public PlayerInputSystem PlayerInput;

    [Header("Movement Controls")]
    Vector2 movementInput;
    [Header("Camera Controls")]
    Vector2 cameraInput;
    [Header("Camera Values")]
    public float cameraInputX;
    public float cameraInputY;
    public float _SensitivityMultiplier;
    [Header("Ability Button")]
    public bool IsAbilityPressed;
    [Header("Switch Button")]
    public bool IsSwitchPressed;
    [Header("Interact Button")]
    public bool IsInteractPressed;
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
        settingsMenu = FindObjectOfType<MenusScript>();
        gameManager = FindObjectOfType<GameManagerStateMachine>();
        cameraObject = Camera.main.transform;
        states = new PlayerStateFactory(this);
        currentState = states.Idle();
        currentState.EnterState();
        characterControllers.Add(rubberController);
        characterControllers.Add(rulerController);
        characterControllers.Add(pencilController);
        CameraFadeAnimator = GameObject.FindGameObjectWithTag("CameraFade").GetComponent<Animator>();
    }

    private void Start()
    {
        AdjustValues();
    }

    private void Update()
    {
        currentState.UpdateStates();
        CheckCurrentCharacter();
        IsInteractPressed = false;
        IsAbilityPressed = false;
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
            ForceSwitch();
        }
    }

    public void ForceSwitch()
    {
        CameraFadeAnimator.SetBool("Switch", true);
        if (FinishTransform)
        {
            ShouldChangeCharacter = false;
            FinishTransform = false;
            CameraFadeAnimator.SetBool("Switch", false);
            ChangeCharacter();
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
        HasCharacterBeenSacrificed();
        HasCharacterBeenArrested();
        int camIndex = -1;
        foreach(CinemachineVirtualCamera camera in gameManager.Cameras)
        {
            camera.Priority = 10;
            if (camIndex == CharacterType)
            {
                camera.Priority = 11;
            }
            camIndex++;
        }
        camIndex = -1;
        AdjustValues();
    }

    private void HasCharacterBeenSacrificed()
    {
        if (_RubberTaken&&CharacterType==0)
        {
            CharacterType++;
        }
        if (_RulerTaken && CharacterType == 1)
        {
            CharacterType++;
        }
        if (_PencilTaken && CharacterType == 2)
        {
            CharacterType = 0;
        }
    }

    private void HasCharacterBeenArrested()
    {
        if (_RubberArrested && CharacterType == 0)
        {
            CharacterType++;
        }
        if (_RulerArrested && CharacterType == 1)
        {
            CharacterType++;
        }
        if (_PencilArrested && CharacterType == 2)
        {
            if (_RubberArrested)
            {
                if (_RulerArrested)
                {
                    CharacterType = 0;
                }
                else
                {
                    CharacterType = 1;
                }
            }
            else
            {
                CharacterType = 0;
            }
        }
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
        foreach(SpriteRenderer sprite in characterSprites)
        {
            //GameObject spriteObj = sprite.GetComponent<GameObject>();
            sprite.transform.LookAt(Camera.main.transform);
            var newRot = sprite.transform.rotation.eulerAngles;
            sprite.transform.rotation = Quaternion.Euler(0,newRot.y,0);
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
        PlayerInput.Main.Ability.started += OnAbility;
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
        PlayerInput.Main.Ability.started -= OnAbility;
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

    private void OnAbility(InputAction.CallbackContext ctx)
    {
        IsAbilityPressed = ctx.ReadValueAsButton();
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

        //cameraInputY = cameraInput.y;
        //cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y * settingsMenu.sensitivity;
        cameraInputX = cameraInput.x * settingsMenu.sensitivity;
    }
}
