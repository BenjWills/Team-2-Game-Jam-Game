using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) 
    {
        IsRootState= true;
        InitializeSubState();
    }
    public override void EnterState() { }
    public override void UpdateState() 
    {
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates() 
    {
        if (Ctx.gameManager._CanMove)
        {
            if (Ctx.vertInput != 0 || Ctx.horInput != 0)
            {
                SwitchState(Factory.Walk());
            }
        }
        InitializeCharacterState();
        SetSubState(Factory.Interact());
    }
    public override void InitializeSubState() 
    {
        SetSubState(Factory.Interact());
    }
    public override void InitializeCharacterState()
    {
        if (Ctx._IsRubber)
        {
            SetCharacterState(Factory.Rubber());
        }
        else if (Ctx._IsRuler)
        {
            SetCharacterState(Factory.Ruler());
        }
        else if (Ctx._IsPencil)
        {
            SetCharacterState(Factory.Pencil());
        }
    }
}
