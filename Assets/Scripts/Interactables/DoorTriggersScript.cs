using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggersScript : MonoBehaviour
{
    [SerializeField] DoorScript doorScript;

    public void FinishedClosing()
    {
        doorScript.FinishedClosing();
    }
    public void FinishAbility()
    {
        doorScript.FinishAbility();
    }


}
