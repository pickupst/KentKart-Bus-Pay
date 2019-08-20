using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carEngine : MonoBehaviour
{

    public float maxMotorTorque = 200f;
    public float maxSteerAngle = 90f;

    public float currentSpeed;
    public float maxSpeed = 150f;

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
                    if (currentNode >= pathTransform.Length - 1)
                    {
                        currentNode = 0;
                    }

                }
            }
        }
        Debug.Log("CurrentNode = " + currentNode + " position " + transform.position + " length " + pathTransform.Length);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log("Nodes Count : " + nodes.Count);

        ApplySteer();
        Drive();
        CheckWayPointDistance();
        //Slower();
        //Debug.Log("Velocity : " + rb.velocity);
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
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 100;
        if (currentSpeed < maxSpeed)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
        
    }

    private void CheckWayPointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 5f)
        {
            if (currentNode >= nodes.Count - 1)
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
