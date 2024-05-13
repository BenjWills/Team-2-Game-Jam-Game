using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeScript : MonoBehaviour
{
    PlayerStateMachine playerStateMachine;
    void Start()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
    }
    public void FinishedCameraFade()
    {
        playerStateMachine.FinishTransform = true;
    }
}
