using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRulerState : PlayerBaseState
{
    public PlayerRulerState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
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

    }
    public override void InitializeSubState() 
    {
    }
    public override void InitializeCharacterState()
    {

    }
}
