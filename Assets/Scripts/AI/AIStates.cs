using UnityEngine;

public abstract class AIStates
{
    public abstract void EnterState(AIScript ais);
    public abstract void UpdateState(AIScript ais);
}
