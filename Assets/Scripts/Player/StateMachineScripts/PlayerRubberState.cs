using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRubberState : PlayerBaseState
{
    public PlayerRubberState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) 
    {
        IsRootState= true;
        InitializeSubState();
    }

    public override void EnterState() 
    {
    }
    public override void UpdateState() 
    {
        CheckSwitchStates();
        ChangeAnimation();
    }
    public override void ExitState() 
    {
        Ctx.CharacterAnimators[0].SetBool("walking", false);
    }
    public override void CheckSwitchStates() 
    {

    }
    public override void InitializeSubState() 
    {
    }
    public override void InitializeCharacterState()
    {

    }

    private void ChangeAnimation()
    {
        Ctx.CharacterAnimators[0].SetBool("walking", Ctx._Walking);
        Ctx.CharacterAnimators[0].SetBool("action", Ctx._Actioning);
        Debug.Log("!");
    }

}
