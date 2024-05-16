using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFadeScript : MonoBehaviour
{
    PlayerStateMachine playerStateMachine;
    MenuManager menuManager;
    void Start()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        menuManager = FindObjectOfType<MenuManager>();
    }

    private void Update()
    {
        if (playerStateMachine==null)
        {
            playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        }
    }

    public void FinishedCameraFade()
    {
        playerStateMachine.FinishTransform = true;
    }

    public void FinishCaught()
    {
        menuManager.P_Finish(false);
    }


    public void FinishComp()
    {
        menuManager.P_Finish(true);
    }



}
