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

    private void Awake()
    {
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
        Vector3 newDestination = RandomNavSphere(transform.position, patrolRadius, -1);
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
