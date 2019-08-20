using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficCreater : MonoBehaviour
{
    public Transform path;
    public GameObject[] cars;
    public int carCount = 5;


    private List<Transform> nodes;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] pathTransform = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransform.Length; i++)
        {            
            nodes.Add(pathTransform[i]);
        }

        for (int i = 0; i < carCount; i++)
        {
            int randPathCount = Random.Range(0, nodes.Count);

            if (randPathCount < nodes.Count - 1)
            {
                Instantiate(cars[Random.Range(0, cars.Length)], nodes[randPathCount].position, Quaternion.FromToRotation(nodes[randPathCount].position, nodes[randPathCount + 1].position));
            }
            else
            {
                Instantiate(cars[Random.Range(0, cars.Length)], nodes[randPathCount].position, Quaternion.identity);
            }
            nodes.RemoveAt(randPathCount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
