using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPencilState : PlayerBaseState
{
    public PlayerPencilState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) 
    {
        IsRootState= true;
        InitializeSubState();
    }
    public override void EnterState() { }
    public override void UpdateState() 
    {
        Debug.Log("Pencil");
        CheckSwitchStates();
    }
    public override void ExitState() { }
    public override void CheckSwitchStates() 
    {
        InitializeCharacterState();
    }
    public override void InitializeSubState() 
    { 
    }
    public override void InitializeCharacterState()
    {
    }
}
