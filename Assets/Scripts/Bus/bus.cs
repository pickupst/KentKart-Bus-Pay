using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bus : MonoBehaviour
{
    public Text textPassCount;
    int passCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textPassCount.text = "Yolcu : " + passCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "busStation")
        {
            //Debug.Log("indiryolcusayısı" + Person.getInBusPersons().Count);
            foreach (GameObject person in Person.getInBusPersons())
            {
                Vector3 randDir = Random.insideUnitSphere * 3f;
                randDir += other.transform.position;

                person.transform.position = randDir;
                person.SetActive(true);

                Debug.Log("indiryolcu FOREACH");
            }

            Person.getInBusPersons().Clear();
        }
        
    }

    private void OnTriggerStay (Collider other)
    {
        if (other.tag == "busStation") //Duraktayken yolcu sayısı güncellenir
        {
            passCount = Person.getInBusPersons().Count;
        }

    }
}
