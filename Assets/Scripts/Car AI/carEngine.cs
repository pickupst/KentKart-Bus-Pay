using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carEngine : MonoBehaviour
{
    public float maxSteerAngle = 40f;
    public float motor = 50f;
    public Transform path;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;


    private List<Transform> nodes;
    private int currentNode = 0;

    // Start is called before the first frame update
    void Start()
    {
        Transform[] pathTransform = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != path.transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplySteer();
        Drive();
        CheckWayPointDistance();
    }

    private void ApplySteer()
    {
        Vector3 reletivevector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (reletivevector.x / reletivevector.magnitude) * maxSteerAngle;

        wheelFL.steerAngle = -newSteer;
        wheelFR.steerAngle = -newSteer;

    }

    private void Drive()
    {
        wheelFL.motorTorque = motor;
        wheelFR.motorTorque = motor;
    }

    private void CheckWayPointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 10f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
        Debug.Log("CurrentNode : " + currentNode + " position " + nodes[currentNode].position);
    }
}
