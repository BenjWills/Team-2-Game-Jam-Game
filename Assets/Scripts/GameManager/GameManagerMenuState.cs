using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManagerMenuState : GameManagerBaseState
{
    public GameManagerMenuState(GameManagerStateMachine currentContext, GameManagerStateFactory gameManagerStateFactory)
    : base(currentContext, gameManagerStateFactory)
    {
        IsRootState = true;
        InitializeSubState();
    }
    public override void EnterState() 
    {
        //Ctx._InMenu= true;
        //foreach(CinemachineVirtualCamera camera in Ctx.Cameras)
        //{
        //    camera.Priority = 10;
        //}
        //Ctx.menuCamera.Priority = 11;
        //Ctx.menuManager.GoToMenu();
        //AudioManager.Instance.DialogueSource.Stop();
        //AudioManager.Instance.PlayMusic("Menu Music");
    }
    public override void UpdateState()
    {
        CheckSwitchStates();
    }
    public override void ExitState() 
    {
        //Ctx.inMenu = false;
        //AudioManager.Instance.StopMusic();
    }
    public override void CheckSwitchStates()
    {
    }
    public override void InitializeSubState()
    {
    }
}
