using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    public AIStates currentState;
    public readonly RubberFollowState rfs = new RubberFollowState();
    public readonly PencilFollowState pfs = new PencilFollowState();
    public readonly RulerFollowState rufs = new RulerFollowState();
    public readonly DoorFollowState dfs = new DoorFollowState();
    public readonly AIWinState ws = new AIWinState();

    public NavMeshAgent agent;
    PlayerStateMachine playerStateMachine;
    GameManagerStateMachine gameManager;

    GameObject collidedObject;

    private GameObject AIObject;
    public GameObject rubber;
    public GameObject ruler;
    public GameObject pencil;

    public Transform aiEndPoint;
    public List<Transform> character;

    BoxCollider copCollider;

    public float AICharacterDis;
    public float AICharacterDis2;
    public float AICharacterDis3;
    public float finalValueDis;
    public float characterCaughtCounter;

    bool finishRubber;
    bool finishRuler;
    bool finishPencil;

    public bool Unlocking;
    DoorScript doorScript;

    private void Awake()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        gameManager = FindObjectOfType<GameManagerStateMachine>();
        AIObject = gameObject;
        rubber = GameObject.FindGameObjectWithTag("Rubber");
        ruler = GameObject.FindGameObjectWithTag("Ruler");
        pencil = GameObject.FindGameObjectWithTag("Pencil");
        aiEndPoint = GameObject.FindGameObjectWithTag("EndPoint").transform;
        character.Add(rubber.transform);
        character.Add(ruler.transform);
        character.Add(pencil.transform);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TrackPlayer();
        CheckForActiveDoor();
        CheckIfArrested();
        if (playerStateMachine._RubberArrested)
        {
            AICharacterDis = 1000;
            //Destroy(rubber);
        }
        else
        {
            AICharacterDis = Vector3.Distance(character[0].position, AIObject.transform.position);
        }
        if (playerStateMachine._RulerArrested)
        {
            AICharacterDis2 = 1000;
            //Destroy(ruler);
        }
        else
        {
            AICharacterDis2 = Vector3.Distance(character[1].position, AIObject.transform.position);
        }
        if (playerStateMachine._PencilArrested)
        {
            AICharacterDis3 = 1000;
            //Destroy(pencil);
        }
        else
        {
            AICharacterDis3 = Vector3.Distance(character[2].position, AIObject.transform.position);
        }

        if (characterCaughtCounter == 3)
        {
            TransitionToState(ws);
        }

        finalValueDis = Mathf.Min(AICharacterDis, AICharacterDis2, AICharacterDis3);

        currentState.UpdateState(this);
    }

    private void CheckForActiveDoor()
    {
        if (doorScript != null)
        {
            Unlocking = doorScript.Unlocking;
            if (!doorScript.Unlocking)
            {
                doorScript = null;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        var mask = playerStateMachine.layerMaskInteract.value;
        if (Vector3.Distance(transform.position, collision.gameObject.transform.position) <= 2)
        {
            if (!Physics.Linecast(transform.position, collision.gameObject.transform.position, mask))
            {
                collidedObject = collision.gameObject;
                CheckForCriminals();
                if (collidedObject.CompareTag("Interact"))
                {
                    var InteractScript = collidedObject.GetComponent<InteractBaseScript>();
                    if (InteractScript._InteractType == "Door")
                    {
                        AgainstDoor();
                    }
                }
            }
        }
    }
    private void CheckForCriminals()
    {
        if (collidedObject.gameObject == rubber)
        {
            gameManager.CharactersRemaining++;
            playerStateMachine._RubberArrested = true;
            finishRubber = true;
            gameManager.CharactersCaught++;
            if (gameManager.CharactersRemaining>=3)
            {
                playerStateMachine.CameraFadeAnimator.SetBool("Switch", true);
                playerStateMachine.CameraFadeAnimator.SetTrigger("Finish");
            }
            else
            {
                if (playerStateMachine.CharacterType == 0)
                {
                    playerStateMachine.ShouldChangeCharacter = true;
                    playerStateMachine.ForceSwitch();
                }
            }
            
        }
        if (collidedObject.gameObject == ruler)
        {
            gameManager.CharactersRemaining++;
            playerStateMachine._RulerArrested = true;
            finishRuler = true;
            gameManager.CharactersCaught++;
            if (gameManager.CharactersRemaining >= 3)
            {
                playerStateMachine.CameraFadeAnimator.SetBool("Switch", true);
                playerStateMachine.CameraFadeAnimator.SetTrigger("Finish");
            }
            else
            {
                if (playerStateMachine.CharacterType == 1)
                {
                    playerStateMachine.ShouldChangeCharacter = true;
                    playerStateMachine.ForceSwitch();
                }
            }
        }
        if (collidedObject.gameObject == pencil)
        {
            gameManager.CharactersRemaining++;
            playerStateMachine._PencilArrested = true;
            finishPencil = true;
            gameManager.CharactersCaught++;
            if (gameManager.CharactersRemaining >= 3)
            {
                playerStateMachine.CameraFadeAnimator.SetBool("Switch", true);
                playerStateMachine.CameraFadeAnimator.SetTrigger("Finish");
            }
            else
            {
                if (playerStateMachine.CharacterType == 2)
                {
                    playerStateMachine.ShouldChangeCharacter = true;
                    playerStateMachine.ForceSwitch();
                }
            }
        }
        if (playerStateMachine._RubberArrested && playerStateMachine._RulerArrested&& playerStateMachine._PencilArrested)
        {
            playerStateMachine.CameraFadeAnimator.SetBool("Switch", true);
            playerStateMachine.CameraFadeAnimator.SetTrigger("Caught");
        }
    }   

    private void CheckIfArrested()
    {
        if(collidedObject!=null)
        {
            if (collidedObject.transform.position != new Vector3(100, 0, 100))
            {
                if (finishRubber && playerStateMachine.CharacterType != 0)
                {
                    collidedObject.transform.position = new Vector3(100, 0, 100);
                    finishRubber = false;
                }
                if (finishRuler && playerStateMachine.CharacterType != 1)
                {
                    collidedObject.transform.position = new Vector3(100, 0, 100);
                    finishRuler = false;
                }
                if (finishPencil && playerStateMachine.CharacterType != 2)
                {
                    collidedObject.transform.position = new Vector3(100, 0, 100);
                    finishPencil = false;
                }
            }
        }
    }

    private void AgainstDoor()
    {
        doorScript = collidedObject.GetComponent<DoorScript>();
        if (doorScript._Locked)
        {
            if (doorScript._RubberLocked||doorScript._PencilLocked)
            {
                doorScript.Unlocking = true;
            }
        }
        if (!doorScript.open&&!doorScript._Locked)
        {
            AudioManager.Instance.Play("Police Door");
            doorScript.open= true;
        }
    }

    private void TrackPlayer()
    {
        if (!Unlocking)
        {
            if (finalValueDis == AICharacterDis)
            {
                aiEndPoint.position = character[0].position;
                TransitionToState(rfs);
            }
            else if (finalValueDis == AICharacterDis2)
            {
                aiEndPoint.position = character[1].position;
                TransitionToState(rufs);
            }
            else if (finalValueDis == AICharacterDis3)
            {
                aiEndPoint.position = character[2].position;
                TransitionToState(pfs);
            }
        }
        else
        {
            TransitionToState(dfs);
        }
    }

    public void TransitionToState(AIStates state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
