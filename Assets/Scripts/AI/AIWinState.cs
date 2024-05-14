using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWinState : AIStates
{
    public override void EnterState(AIScript ais)
    {
        Debug.Log("AI Win");
    }
    public override void UpdateState(AIScript ais)
    {
        
    }
}
