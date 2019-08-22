using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bus : MonoBehaviour
{
    public Text textPassCount;


    private static int gold = 0;
    private int passCount = 0;


    private bool passOutable = false; //yolcular indirilebilir mi ? 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textPassCount.text = "Yolcu : " + passCount + " \n Para: " + gold;
    }

    private void OnTriggerExit(Collider other) //yolcuları aldık ve artık indirilebilirler
    {
        if (other.tag == "busStation")
        {
            passOutable = true;
        }

        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "busStation" && GetComponentInParent<Rigidbody>().velocity == Vector3.zero && passOutable) //durak içinde duruyorsak ve yolcular indirilebilirse indir.
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
            Debug.Log("indiryolcu FOREACH " + passOutable);
            Person.getInBusPersons().Clear();
            passOutable = false; //artık yolcular indir yeni yolcular başka durağa inebilir
        }

        if (other.tag == "busStation") //Duraktayken yolcu sayısı güncellenir
        {
            passCount = Person.getInBusPersons().Count;
        }

    }

    public static void AddGold(int sumGold)
    {
        gold += sumGold;
    }
}
