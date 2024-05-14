using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentScript : MonoBehaviour
{
    InteractBaseScript interactBaseScript;
    private void Awake()
    {
        interactBaseScript = GetComponent<InteractBaseScript>();
        interactBaseScript._InteractType = "Vent";
    }

    public void VentTriggered()
    {
        Debug.Log("I AM A VENT");
    }
}
