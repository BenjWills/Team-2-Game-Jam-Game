using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFollowState : AIStates
{
    public override void EnterState(AIScript ais)
    {
    }
    public override void UpdateState(AIScript ais)
    {
        ais.agent.SetDestination(ais.transform.position);
    }
}
