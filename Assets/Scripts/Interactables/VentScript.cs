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

    private void Awake()
    {
        interactBaseScript = GetComponent<InteractBaseScript>();
        interactBaseScript._InteractType = "Vent";
        ventCollider = GetComponent<BoxCollider>();
        ventCollider.enabled = false;
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
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
        Debug.Log("I AM A VENT");
        Debug.Log("Finished Game");
        ventCollider.enabled = false;
    }
}
