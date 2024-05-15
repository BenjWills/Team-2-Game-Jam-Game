using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    InteractBaseScript interactBaseScript;
    [SerializeField] GameObject DoorSprite;
    Animator doorAnim;
    [SerializeField] GameObject DoorScale;
    public bool open;
    public bool displayUnlocked;
    bool finishedClosing;
    bool AbilityTriggered;
    public bool _Locked;
    bool _RubberLocked;
    bool _RulerLocked;
    bool _PencilLocked;

    public bool Unlocking;
    public float UnlockTimer;
    public float RulerUnlockTimer;

    [SerializeField] float PencilTimer = 5f;
    [SerializeField] float RubberTimer = 10f;
    [SerializeField] float RulerTimer = 20f;

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
                _RubberLocked = true;
                break;
            case "ruler":
                _RulerLocked = true;
                break;
            case "pencil":
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
