using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentScript : MonoBehaviour
{
    InteractBaseScript interactBaseScript;
    BoxCollider ventCollider;

    [SerializeField] Sprite openVent;
    [SerializeField] Sprite closedVent;
    [SerializeField] GameObject Arrow;
    [SerializeField] SpriteRenderer VentSprite;

    PlayerStateMachine playerStateMachine;
    GameManagerStateMachine gameManager;


    private void Awake()
    {
        interactBaseScript = GetComponent<InteractBaseScript>();
        interactBaseScript._InteractType = "Vent";
        ventCollider = GetComponent<BoxCollider>();
        ventCollider.enabled = false;
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        gameManager = FindObjectOfType<GameManagerStateMachine>();
    }

    private void Start()
    {
        Arrow.SetActive(false);
        VentSprite.sprite = closedVent;
    }

    private void Update()
    {
        if (Arrow.activeSelf)
        {
            Arrow.transform.LookAt(playerStateMachine.transform.position);
            Arrow.transform.rotation = Quaternion.Euler(0, Arrow.transform.rotation.y, 0);
        }
    }

    public void VentActivated()
    {
        Arrow.SetActive(true);
        ventCollider.enabled = true;
        VentSprite.sprite = openVent;
    }

    public void VentInteracted()
    {
        gameManager.CharactersEscaped++;
        gameManager.CharactersRemaining++;
        if (gameManager.CharactersEscaped+gameManager.CharactersCaught != 3)
        {
            switch (playerStateMachine.CharacterType)
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
            playerStateMachine.ShouldChangeCharacter = true;
            playerStateMachine.characterControllers[playerStateMachine.CharacterType].transform.position = new Vector3(100, 0, 100);
            playerStateMachine.ForceSwitch();
        }
        else
        {
            playerStateMachine.CameraFadeAnimator.SetBool("Switch", true);
            playerStateMachine.CameraFadeAnimator.SetTrigger("Finish");
        }
    }
}
