using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultScript : MonoBehaviour
{
    InteractBaseScript interactBaseScript;
    private void Awake()
    {
        interactBaseScript = GetComponent<InteractBaseScript>();
        interactBaseScript._InteractType = "Vault";
    }
}
