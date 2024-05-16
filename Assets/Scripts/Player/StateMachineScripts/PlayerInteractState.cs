using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteractState : PlayerBaseState
{
    public PlayerInteractState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    List<GameObject> InteractablesInArea = new();
    GameObject currentInteractable;

    public override void EnterState() { }
    public override void UpdateState() 
    {
        if (Ctx.gameManager._CanMove && !Ctx.gameManager._Paused)
        {
            InteractRaycast();
        }
    }
    public override void ExitState() 
    {
    }
    public override void CheckSwitchStates() 
    {
    }
    public override void InitializeSubState() { }
    public override void InitializeCharacterState()
    {

    }
    private void InteractRaycast()
    {
        InteractablesInArea.Clear();
        switch (Ctx.ReturnCharacterName())
        {
            case "rubber":
                foreach (GameObject interactable in Ctx._InteractablesInRubber)
                {
                    InteractablesInArea.Add(interactable);
                }
                break;
            case "ruler":
                foreach (GameObject interactable in Ctx._InteractablesInRuler)
                {
                    InteractablesInArea.Add(interactable);
                }
                break;
            case "pencil":
                foreach (GameObject interactable in Ctx._InteractablesInPencil)
                {
                    InteractablesInArea.Add(interactable);
                }
                break;
        }
        CheckForInteractables();
    }

    private void CheckForInteractables()
    {
        int index = 0;
        foreach (GameObject interactable in InteractablesInArea)
        {
            currentInteractable = interactable;
            var mask = Ctx.layerMaskInteract.value;
            if (!Physics.Linecast(Ctx._PlayerTrack[Ctx.CharacterType].position, interactable.transform.position, mask) && index == 0)
            {
                if (Ctx.IsInteractPressed)
                {
                    InteractOnObject();
                }
                else if (Ctx.IsAbilityPressed)
                {
                    AbilityOnObject();
                }
            }
        }
    }
    
    private void AbilityOnObject()
    {
        var InteractType = currentInteractable.GetComponent<InteractBaseScript>()._InteractType;
        switch (InteractType)
        {
            case "Door":
                AbilityOnDoor();
                break;
        }
    }

    private void InteractOnObject()
    {
        var InteractType = currentInteractable.GetComponent<InteractBaseScript>()._InteractType;
        switch (InteractType)
        {
            case "Door":
                InteractWithDoor();
                break;
            case "Vault":
                InteractWithVault();
                break;
            case "Vent":
                InteractWithVent();
                break;
        }
    }

    private void InteractWithDoor()
    {
        var DoorScript = currentInteractable.GetComponent<DoorScript>();
        if (DoorScript._Locked)
        {
            //trigger locked animation
        }
        else
        {
            if (DoorScript.open)
            {
                DoorScript.CloseDoor();
            }
            else
            {
                DoorScript.OpenDoor();
            }
        }
    }

    private void InteractWithVault()
    {
        var VaultScript = currentInteractable.GetComponent<VaultScript>();
        VaultScript.SacrificeCharacter();
    }

    private void InteractWithVent()
    {
        var VentScript = currentInteractable.GetComponent<VentScript>();
        VentScript.VentInteracted();
    }
    private void AbilityOnDoor()
    {
        var DoorScript = currentInteractable.GetComponent<DoorScript>();
        if (DoorScript._Locked)
        {
            //trigger locked animation
        }
        else
        {
            if (DoorScript.open)
            {
                DoorScript.CloseDoor();
            }
            else
            {
                DoorScript.DoorAbilityCalled();
            }
        }
    }

}
