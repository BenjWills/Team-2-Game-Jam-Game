using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    public NavMeshAgent agent;
    private GameObject AIObject;
    public Transform aiEndPoint;
    public Transform[] character;

    private float AICharacterDis;
    private float AICharacterDis2;
    private float AICharacterDis3;
    private float finalValueDis;
    private bool desicionMade;

    private void Awake()
    {
        AIObject = agent.gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("1 " + AICharacterDis);
        Debug.Log("2 " + AICharacterDis2);
        Debug.Log("3 " + AICharacterDis3);

        AICharacterDis = Vector3.Distance(character[0].position, AIObject.transform.position);
        AICharacterDis2 = Vector3.Distance(character[1].position, AIObject.transform.position);
        AICharacterDis3 = Vector3.Distance(character[2].position, AIObject.transform.position);

        finalValueDis = Mathf.Min(AICharacterDis, AICharacterDis2, AICharacterDis3);

        if (finalValueDis == AICharacterDis && desicionMade == false)
        {
            desicionMade = true;
            aiEndPoint.position = character[0].position;
            StartCoroutine(DesicionTime());
        }
        else if (finalValueDis == AICharacterDis2 && desicionMade == false)
        {
            desicionMade = true;
            aiEndPoint.position = character[1].position;
            StartCoroutine(DesicionTime());
        }
        else if (finalValueDis == AICharacterDis3 && desicionMade == false)
        {
            desicionMade = true;
            aiEndPoint.position = character[2].position;
            StartCoroutine(DesicionTime());
        }

        agent.SetDestination(aiEndPoint.position);
    }

    IEnumerator DesicionTime()
    {
        yield return new WaitForSeconds(5);
        desicionMade = false;
    }
}
