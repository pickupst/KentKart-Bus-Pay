using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    bool isEmptyCard = false;
    bool isGotoBusStation = false;
    private Vector3 busPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("isEmptyCard: " + isEmptyCard + " isGotoBusStation: " + isGotoBusStation);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        //Debug.Log("OnTriggerStay " + isGotoBusStation);
        if (other.tag == "busStation" )
        {
            busPoint = other.transform.position;
            isGotoBusStation = true;
            Debug.Log("OnTriggerEnter " + isGotoBusStation);
        }
    }

    public Vector3 getBusPoint()
    {
        return busPoint;
    }

    private void OnTriggerStay(Collider other)
    {
        //Destroy(other.gameObject);
        //Debug.Log("OnTriggerStay " + isGotoBusStation);
        if (other.tag == "busStation" )
        {
            busPoint = other.transform.position;
            isGotoBusStation = true;

            
        }
    }

    public bool getIsGotoBusStation ()
    {
        Debug.Log("GetComponent<Person>().getBusPoint(); : " + busPoint);

        return isGotoBusStation;
    }

}
