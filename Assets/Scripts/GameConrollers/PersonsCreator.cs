using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonsCreator : MonoBehaviour
{
    public Transform[] limitsPos;
    public GameObject[] persons;
    public float personsCount;

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("limitsPos: " + limitsPos.position);
        for (int i = 0; i < personsCount; i++)
        {
            int randPersonIndex = Random.Range(0, persons.Length);
            int randIndex = Random.Range(0, limitsPos.Length);
            //float posX = Random.Range(limitsPos.position.x, 0.2);
            Vector3 RandPosition = new Vector3(limitsPos[randIndex].position.x, 0.2f, limitsPos[randIndex].position.z);
            Instantiate(persons[randPersonIndex], RandPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
