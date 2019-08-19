using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carEngine : MonoBehaviour
{
    public float maxSpeed = 7;
    public float maxSteerAngle = 40f;
    public float motor = 50f;
    public Transform path;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;

    private Rigidbody rb;

    private List<Transform> nodes;
    private int currentNode = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Transform[] pathTransform = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for (int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != path.transform)
            {
                nodes.Add(pathTransform[i]);

                if (Vector3.Distance(transform.position, pathTransform[i].position) < 10f)
                {
                    currentNode = i;
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplySteer();
        Drive();
        CheckWayPointDistance();
        Slower();
        Debug.Log("Velocity : " + rb.velocity);
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
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 5f)
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
        //Debug.Log("CurrentNode : " + currentNode + " position " + nodes[currentNode].position);
    }

    private void Slower() //hız sabitleme
    {

        if (rb.velocity.x > maxSpeed || rb.velocity.x < -maxSpeed) //eğer hız aşarsa
        {
            Debug.Log("SINIR AŞILDIIIII");
            rb.velocity = new Vector3(maxSpeed * (rb.velocity.x / Mathf.Abs(rb.velocity.x)), rb.velocity.y, rb.velocity.z);
        }

        if (rb.velocity.z > maxSpeed || rb.velocity.z < -maxSpeed) 
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, maxSpeed * (rb.velocity.z / Mathf.Abs(rb.velocity.z)));
        }
    }
}
