using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCone : MonoBehaviour
{
    PlayerStateMachine playerStateMachine;
    public Material highlighted;
    public Material nothighlighted;
    public bool active;
    [Range(0,2)]
    [SerializeField] private int characterNumber;
    private List<GameObject> InCone = new();
    private void Start()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Interact"))
        {
            InCone.Add(col.gameObject);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Interact"))
        {
            InCone.Remove(col.gameObject);
        }
    }
    private void Update()
    {
        switch (characterNumber)
        {
            case 0:
                playerStateMachine._InteractablesInRubber = InCone;
                break;
            case 1:
                playerStateMachine._InteractablesInRuler = InCone;
                break;
            case 2:
                playerStateMachine._InteractablesInPencil = InCone;
                break;
        }
        if (InCone.Count > 0)
        {
            GetComponent<Renderer>().material = highlighted;
        }
        else
        {
            GetComponent<Renderer>().material = nothighlighted;
        }
    }
}
