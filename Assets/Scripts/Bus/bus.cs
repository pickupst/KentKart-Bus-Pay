using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bus : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "busStation")
        {
            Debug.Log("indiryolcu");
            foreach (GameObject person in Person.getInBusPersons())
            {
                Vector3 randDir = Random.insideUnitSphere * 3f;
                randDir += other.transform.position;

                person.transform.position = randDir;
                person.SetActive(true);

                Debug.Log("indiryolcu FOREACH");
            }
        }
        
    }
}
