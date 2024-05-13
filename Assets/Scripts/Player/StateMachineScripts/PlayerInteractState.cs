using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteractState : PlayerBaseState
{
    public PlayerInteractState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    GameObject objectHighlighted;

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
        int index = 0;
        switch (Ctx.ReturnCharacterName())
        {
            case "rubber":
                foreach (GameObject interactable in Ctx._InteractablesInRubber)
                {
                    var mask = Ctx.layerMaskInteract.value;
                    if (!Physics.Linecast(Ctx._PlayerTrack[Ctx.CharacterType].position, interactable.transform.position, mask) && Ctx.IsInteractPressed && index == 0)
                    {
                        //trigger action or something
                    }
                }
                break;
            case "ruler":
                foreach (GameObject interactable in Ctx._InteractablesInRuler)
                {
                    var mask = Ctx.layerMaskInteract.value;
                    if (!Physics.Linecast(Ctx._PlayerTrack[Ctx.CharacterType].position, interactable.transform.position, mask) && Ctx.IsInteractPressed && index == 0)
                    {
                        //trigger action or something
                    }
                }
                break;
            case "pencil":
                foreach (GameObject interactable in Ctx._InteractablesInPencil)
                {
                    var mask = Ctx.layerMaskInteract.value;
                    if (!Physics.Linecast(Ctx._PlayerTrack[Ctx.CharacterType].position, interactable.transform.position, mask) && Ctx.IsInteractPressed && index == 0)
                    {
                        //trigger action or something
                    }
                }
                break;
        }
    }
}
