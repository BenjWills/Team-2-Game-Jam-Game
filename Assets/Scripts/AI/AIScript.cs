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
    public readonly AIWinState ws = new AIWinState();

    public NavMeshAgent agent;
    PlayerStateMachine playerStateMachine;


    private GameObject AIObject;
    public GameObject rubber;
    public GameObject ruler;
    public GameObject pencil;

    public Transform aiEndPoint;
    public List<Transform> character;

    public float AICharacterDis;
    public float AICharacterDis2;
    public float AICharacterDis3;
    public float finalValueDis;
    public float characterCaughtCounter;

    private void Awake()
    {
        playerStateMachine = FindObjectOfType<PlayerStateMachine>();
        AIObject = gameObject;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == rubber)
        {
            playerStateMachine._RubberArrested = true;
            if (playerStateMachine.CharacterType==0)
            {
                playerStateMachine.ShouldChangeCharacter = true;
                playerStateMachine.ForceSwitch();
            }
        }
        if (collision.gameObject == ruler)
        {
            playerStateMachine._RulerArrested = true;
            if (playerStateMachine.CharacterType == 1)
            {
                playerStateMachine.ShouldChangeCharacter = true;
                playerStateMachine.ForceSwitch();
            }
        }
        if (collision.gameObject == pencil)
        {
            playerStateMachine._PencilArrested = true;
            if (playerStateMachine.CharacterType == 2)
            {
                playerStateMachine.ShouldChangeCharacter = true;
                playerStateMachine.ForceSwitch();
            }
        }
    }

    private void TrackPlayer()
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

    public void TransitionToState(AIStates state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
