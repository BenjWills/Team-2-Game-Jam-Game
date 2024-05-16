using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopSpriteScript : MonoBehaviour
{
    Animator _Animator;
    bool backwards;
    PlayerStateMachine playerStateMachine;
    List<GameObject> objectInCollider = new();

    private void Start()
    {
        backwards = false;
        _Animator = GetComponent<Animator>();
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
    }

    private void Update()
    {
        _Animator.SetBool("Backwards", backwards);
        backwards = false;
        foreach (GameObject obj in objectInCollider) 
        {
            if (obj == playerStateMachine.characterControllers[playerStateMachine.CharacterType].gameObject)
            {
                backwards = true;
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        objectInCollider.Add(col.gameObject);
    }

    private void OnTriggerExit(Collider col)
    {
        objectInCollider.Remove(col.gameObject);
    }
}
