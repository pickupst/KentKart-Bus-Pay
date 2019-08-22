using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    public GameObject Kentkart;
    public float fillTime = 5f; //5 saniye doldurmayı bekler

    public bool isEmptyCard = true; //her insanın kentkartı başta boş
    public bool isGotoBusStation = false;
    public bool isGotoCardStation = false;

    private Vector3 busPoint;
    private Vector3 cardStationPoint;

    private GameObject card;

    public float maxBusDistance = 7f;

    static List<GameObject> inBusPersons = new List<GameObject>();

    public static List<GameObject> getInBusPersons()
    {
        return inBusPersons;
    }

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        card = (GameObject)Instantiate(Kentkart, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        card.transform.parent = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEmptyCard == false) //kart doluysa
        {
            card.SetActive(false); //kentkart resmini gösterme
        }
        else if (!card.activeSelf)
        {
            card.SetActive(true);
        }
    }

    private void OnEnable() //Otobüsten inerkennn
    {
        if (inBusPersons.Count > 0) //ilk başta liste boşken kendini silmeye çaışmasın
        {
            //inBusPersons.Remove(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

    }

    public Vector3 getBusPoint()
    {
        return busPoint;
    }

    public Vector3 getCardStationPoint()
    {
        return cardStationPoint;
    }

    private void OnTriggerStay(Collider other)
    {
        //Destroy(other.gameObject);
        //Debug.Log("OnTriggerStay " + isGotoBusStation);
        if (other.tag == "cardPoint" && isEmptyCard == true)
        {
            cardStationPoint = other.transform.position;
            //Debug.Log(other.transform.position);
            isGotoCardStation = true;
            if (Vector3.Distance(other.transform.position, transform.position) < 3f)
            {
                //Debug.Log("KART DOLUYOR");
                StartCoroutine(FillCard());
                isGotoCardStation = false;
            }

        }
        else if (other.tag == "busStation" && isGotoBusStation == false && isEmptyCard == false && !other.GetComponent<busStation>().IsFull())
        {
            busPoint = other.transform.position;
            isGotoBusStation = true;   
        }
        else if (other.tag == "Player" && isGotoBusStation == true)
        {
           if (other.GetComponent<Rigidbody>().velocity == Vector3.zero && Vector3.Distance(other.transform.position, transform.position) < maxBusDistance)
            {
                //Debug.Log("Vector3.Distance(other.transform.position, transform.position): " + Vector3.Distance(other.transform.position, transform.position));

                //otobüs geldi ve otobüse biniyor!!!
                isEmptyCard = true;
                isGotoBusStation = false;
                inBusPersons.Add(gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator FillCard()
    {
        isEmptyCard = false;
        yield return new WaitForSeconds(fillTime); // card doldurma süresi
    }

    public bool getIsGotoBusStation ()
    {
        //Debug.Log("GetComponent<Person>().getBusPoint(); : " + busPoint);

        return isGotoBusStation;
    }

    public bool getIsGotoCardStation()
    {
        //Debug.Log("GetComponent<Person>().getBusPoint(); : " + busPoint);

        return isGotoCardStation;
    }

}
