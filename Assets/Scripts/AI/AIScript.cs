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

    public bool cCaught;
    public bool c1Caught;
    public bool c2Caught;


    private void Awake()
    {
        AIObject = agent.gameObject;
    }

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        if (cCaught == true)
        {
            AICharacterDis = 1000;
            Destroy(rubber);
        }
        else
        {
            AICharacterDis = Vector3.Distance(character[0].position, AIObject.transform.position);
        }
        if (c1Caught == true)
        {
            AICharacterDis2 = 1000;
            Destroy(ruler);
        }
        else
        {
            AICharacterDis2 = Vector3.Distance(character[1].position, AIObject.transform.position);
        }
        if (c2Caught == true)
        {
            AICharacterDis3 = 1000;
            Destroy(pencil);
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
        if (collision.gameObject.CompareTag("Rubber"))
        {
            cCaught = true;
        }
        if (collision.gameObject.CompareTag("Ruler"))
        {
            c1Caught = true;
        }
        if (collision.gameObject.CompareTag("Pencil"))
        {
            c2Caught = true;
        }
    }

    public void TransitionToState(AIStates state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
