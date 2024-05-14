using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilFollowState : AIStates
{
    public override void EnterState(AIScript ais)
    {
        Debug.Log("Pencil");
        if (ais.character[1] == null)
        {
            ais.characterCaughtCounter += 1;
        }
        ais.aiEndPoint.position = ais.character[2].position;
    }
    public override void UpdateState(AIScript ais)
    {
        ais.agent.SetDestination(ais.aiEndPoint.position);

        if (ais.finalValueDis == ais.AICharacterDis)
        {
            ais.TransitionToState(ais.rfs);
        }
        if (ais.finalValueDis == ais.AICharacterDis2)
        {
            ais.TransitionToState(ais.rufs);
        }
    }
}
