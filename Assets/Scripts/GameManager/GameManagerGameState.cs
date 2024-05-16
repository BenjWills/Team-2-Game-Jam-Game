using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManagerGameState : GameManagerBaseState
{
    public GameManagerGameState(GameManagerStateMachine currentContext, GameManagerStateFactory gameManagerStateFactory)
    : base(currentContext, gameManagerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState() 
    {
        
    }
    public override void UpdateState()
    {
        if (Ctx.rubberCamera==null)
        {
            Ctx.LoadIntoGame();
        }
        CheckSwitchStates();
        Ctx.GameplayTimer();
    }
    public override void ExitState() 
    {
    }
    public override void CheckSwitchStates()
    {
        if(!Ctx._PlayingGame)
        {
            SwitchState(Factory.Menu());
        }
    }
    public override void InitializeSubState()
    {
    }
}
