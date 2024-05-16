using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorScript : MonoBehaviour
{
    InteractBaseScript interactBaseScript;
    [SerializeField] GameObject DoorSprite;
    Animator doorAnim;
    [SerializeField] GameObject DoorScale;
    [SerializeField] GameObject ScaleAid;
    public bool open;
    public bool displayUnlocked;
    bool finishedClosing;
    bool AbilityTriggered;
    public bool _Locked;
    public bool _RubberLocked;
    bool _RulerLocked;
    public bool _PencilLocked;

    public bool Unlocking;
    public float UnlockTimer;
    public float RulerUnlockTimer;

    [SerializeField] float PencilTimer = 5f;
    [SerializeField] float RubberTimer = 10f;
    [SerializeField] float RulerTimer = 20f;

    NavMeshObstacle obstacle;

    PlayerStateMachine playerStateMachine;
    GameManagerStateMachine gameManager;

    private void Start()
    {
        open = true;
        interactBaseScript = GetComponent<InteractBaseScript>();
        interactBaseScript._InteractType = "Door";
        finishedClosing = true;
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        gameManager = FindObjectOfType<GameManagerStateMachine>();
        doorAnim = DoorSprite.GetComponent<Animator>();
        obstacle = GetComponent<NavMeshObstacle>();
    }

    private void Update()
    {
        CheckIfOpen();
        CheckForAnimationChanges();
        CheckForPolice();
        CheckForLocked();
    }

    private void CheckIfOpen()
    {
        if (open)
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
        else
        {
            GetComponent<BoxCollider>().isTrigger = false;
        }
    }
    private void CheckForAnimationChanges()
    {
        doorAnim.SetBool("open", open);
        doorAnim.SetBool("unlocking", displayUnlocked);
        if (Unlocking)
        {
            doorAnim.SetBool("unlocking", Unlocking);
        }
        doorAnim.SetBool("rubberLocked", _RubberLocked);
        doorAnim.SetBool("pencilLocked", _PencilLocked);
        doorAnim.SetBool("override", !gameManager._PlayingGame);
    }

    public void CheckForPolice()
    {
        if (Unlocking)
        {
            float MaxTime = 0;
            UnlockTimer+=Time.deltaTime;
            if (_RubberLocked)
            {
                MaxTime = RubberTimer;
            }
            if (_PencilLocked)
            {
                MaxTime = PencilTimer;
            }
            if (UnlockTimer>=MaxTime)
            {
                AudioManager.Instance.Play("Police Door");
                UnlockDoor();
                OpenDoor();
                Unlocking = false;
            }
        }
        else
        {
            UnlockTimer = 0;
        }
    }

    private void CheckForLocked()
    {
        if (_RulerLocked)
        {
            obstacle.enabled = true;
            ScaleAid.SetActive(true);
            if (RulerUnlockTimer == 0)
            {
                DoorScale.transform.localScale = DoorScale.transform.localScale * .975f;
                if (DoorScale.transform.localScale.x <= .05f)
                {
                    RulerUnlockTimer = .1f;
                    FinishAbility();
                }
            }
            else
            {
                RulerUnlockTimer += Time.deltaTime;
                DoorScale.transform.localScale = new Vector3(.05f * RulerUnlockTimer, .05f * RulerUnlockTimer, .05f * RulerUnlockTimer);
                if (RulerUnlockTimer >= RulerTimer)
                {
                    DoorScale.transform.localScale = new Vector3(1, 1, 1);
                    UnlockDoor();
                    OpenDoor();
                }
            }
        }
        else
        {
            obstacle.enabled = false;
            ScaleAid.SetActive(false);
            RulerUnlockTimer = 0;
        }
    }

    public void OpenDoor()
    {
        open = true;
    }

    public void CloseDoor()
    {
        open = false;
        finishedClosing = false;
    }
    public void DoorAbilityCalled()
    {
        AbilityTriggered = true;
        gameManager._CanMove = false;
        if (finishedClosing)
        {
            ActivateAbility();
        }
    }

    public void FinishedClosing()
    {
        if (AbilityTriggered)
        {
            ActivateAbility();
        }
        finishedClosing = true;
    }

    private void ActivateAbility()
    {
        AbilityTriggered = false;
        switch(playerStateMachine.ReturnCharacterName())
        {
            case "rubber":
                AudioManager.Instance.Play("Rubber Ab");
                _RubberLocked = true;
                break;
            case "ruler":
                AudioManager.Instance.Play("Ruler Ab");
                _RulerLocked = true;
                break;
            case "pencil":
                AudioManager.Instance.Play("Pencil Ab");
                _PencilLocked = true;
                break;
        }
        playerStateMachine._Actioning = true;
        _Locked = true;
    }

    private void UnlockDoor()
    {
        _Locked = false;
        _RubberLocked = false;
        _RulerLocked = false;
        _PencilLocked = false;
    }
    
    public void FinishAbility()
    {
        gameManager._CanMove = true;
        playerStateMachine._Actioning = false;
    }
}
