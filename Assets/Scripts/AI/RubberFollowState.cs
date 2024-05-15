using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberFollowState : AIStates
{
    public override void EnterState(AIScript ais)
    {
        if (ais.character[0] == null)
        {
            ais.characterCaughtCounter += 1;
        }
        ais.aiEndPoint.position = ais.character[0].position;
    }

    public override void UpdateState(AIScript ais)
    {
        ais.agent.SetDestination(ais.aiEndPoint.position);

        if (ais.finalValueDis == ais.AICharacterDis2)
        {
            ais.TransitionToState(ais.rufs);
        }
        if (ais.finalValueDis == ais.AICharacterDis3)
        {
            ais.TransitionToState(ais.pfs);
        }
    }
}
