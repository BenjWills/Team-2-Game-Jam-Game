using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentScript : MonoBehaviour
{
    InteractBaseScript interactBaseScript;
    BoxCollider ventCollider;
    private void Awake()
    {
        interactBaseScript = GetComponent<InteractBaseScript>();
        interactBaseScript._InteractType = "Vent";
        ventCollider = GetComponent<BoxCollider>();
        ventCollider.enabled = false;
    }

    public void VentActivated()
    {
        ventCollider.enabled = true;
    }

    public void VentInteracted()
    {
        Debug.Log("I AM A VENT");
        Debug.Log("Finished Game");
        ventCollider.enabled = false;
    }
}
