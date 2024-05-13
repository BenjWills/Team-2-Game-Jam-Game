using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory
{
    PlayerStateMachine context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        context = currentContext;
    }

    public PlayerBaseState Idle()
    {
        return new PlayerIdleState(context, this);
    }
    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(context, this);
    }
    public PlayerBaseState Interact()
    {
        return new PlayerInteractState(context, this);
    }
    public PlayerBaseState Rubber()
    {
        return new PlayerRubberState(context, this);
    }
    public PlayerBaseState Ruler()
    {
        return new PlayerRulerState(context, this);
    }
    public PlayerBaseState Pencil()
    {
        return new PlayerPencilState(context, this);
    }
}
