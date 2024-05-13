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
    public override void EnterState() { }
    public override void UpdateState() 
    {
        Debug.Log("Rubber");
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
