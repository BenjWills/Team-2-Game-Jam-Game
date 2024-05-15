using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultScript : MonoBehaviour
{
    InteractBaseScript interactBaseScript;
    PlayerStateMachine playerStateMachine;
    GameManagerStateMachine gameManager;

    [SerializeField] BoxCollider VaultCollider;
    public BoxCollider SacrificeCollider;

    private void Awake()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        gameManager = FindObjectOfType<GameManagerStateMachine>();
        interactBaseScript = GetComponent<InteractBaseScript>();
        interactBaseScript._InteractType = "Vault";
        
    }

    private void Start()
    {
        SacrificeCollider = GetComponent<BoxCollider>();
        SacrificeCollider.enabled = false;
    }

    public void TimerReached()
    {
        VaultCollider.enabled = false;
        //make vault door go bye bye
        SacrificeCollider.enabled = true;
    }
    
    
    public void SacrificeCharacter()
    {
        switch(playerStateMachine.CharacterType)
        {
            case 0:
                playerStateMachine._RubberTaken = true;
                break;
            case 1:
                playerStateMachine._RulerTaken = true;
                break; 
            case 2:
                playerStateMachine._PencilTaken = true;
                break;
        }
        gameManager.CharacterSacrificed = true;
        playerStateMachine.ShouldChangeCharacter = true;
        SacrificeCollider.enabled = false;
        playerStateMachine.ForceSwitch();
    }


}
