using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulerFollowState : AIStates
{
    public override void EnterState(AIScript ais)
    {
        if (ais.character[2] == null)
        {
            ais.characterCaughtCounter += 1;
        }
        ais.aiEndPoint.position = ais.character[1].position;
    }
    public override void UpdateState(AIScript ais)
    {
        ais.agent.SetDestination(ais.aiEndPoint.position);

        if (ais.finalValueDis == ais.AICharacterDis)
        {
            ais.TransitionToState(ais.rfs);
        }
        if (ais.finalValueDis == ais.AICharacterDis3)
        {
            ais.TransitionToState(ais.pfs);
        }
    }
}
