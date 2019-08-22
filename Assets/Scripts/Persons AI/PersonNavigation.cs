using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PersonNavigation : MonoBehaviour
{
    private NavMeshAgent navAgent;

    public float patrolRadius = 10f;
    public float patrol_timer = 10f;
    private float timerCount;

    public NavMeshAgent getNavAgent()
    {
        return navAgent;
    }

    private void Awake()
    {
        patrol_timer = Random.Range(patrol_timer - patrol_timer / 2, patrol_timer);
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timerCount = patrol_timer;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(" isstoped" + navAgent.isStopped);
        Patrol();
    }

    //private void OnDrawGizmos()
    //{
        //Gizmos.DrawWireSphere(transform.position, patrolRadius);
    //}

    void Patrol()
    {
        timerCount += Time.deltaTime;

        if (timerCount > patrol_timer)
        {
            SetNewRandomDestination();

            timerCount = 0;
        }
    }

    void SetNewRandomDestination()
    {
        Vector3 newDestination = Vector3.zero;
        //Debug.Log("GetComponent<Person>().getIsGotoBusStation() : " + GetComponent<Person>().getIsGotoBusStation());
        if (GetComponent<Person>().getIsGotoBusStation())
        {
            //Debug.Log("GetComponent<Person>().getBusPoint(); : " + GetComponent<Person>().getBusPoint());
            newDestination = GetComponent<Person>().getBusPoint();
        }
        else if (GetComponent<Person>().getIsGotoCardStation())
        {
            //Debug.Log("GetComponent<Person>().getBusPoint(); : " + GetComponent<Person>().getBusPoint());
            newDestination = GetComponent<Person>().getCardStationPoint();
        }
        else
        {
            newDestination = RandomNavSphere(transform.position, patrolRadius, -1);
        }

        navAgent.SetDestination(newDestination);
    }

    Vector3 RandomNavSphere(Vector3 originPos, float radius, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * radius;
        randDir += originPos;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, radius, layerMask);

        return navHit.position;
    }
}
