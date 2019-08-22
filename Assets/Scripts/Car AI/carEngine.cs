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

    [Header("Sensors")]
    public float sensorLength = 10f;
    public Vector3 frontSensorPosition = new Vector3(0f, 0.2f, 0.5f);
    public float frontSideSensorPosition = 0.2f;
    public float frontSensorAngle = 30f;

    private Rigidbody rb;

    private List<Transform> nodes;
    private int currentNode = 0;

    private bool isSensorActive = false;

    private float stopForce = -20f;

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
        Sensors();
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

    /* private void Slower() //hız sabitleme
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
    }*/
    
    private void Sensors()
    {

        isSensorActive = false;

        RaycastHit hit;
        Vector3 sensorStartPos = transform.position + frontSensorPosition;
        //ön orta sensör
            if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            isSensorActive = true;
            if (currentSpeed < 5f && Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength / 2))
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, stopForce), ForceMode.Impulse);
            }

        }
        

        //ön sağ sensör
        sensorStartPos.z += frontSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            isSensorActive = true;
            if (currentSpeed < 5f && Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength / 2))
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, stopForce), ForceMode.Impulse);
            }
        }
        

        //ön sağ açılı sensör
        
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            isSensorActive = true;
            if (currentSpeed < 5f && Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength / 2))
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, stopForce), ForceMode.Impulse);
            }
        }
        

        //ön sol sensör
        sensorStartPos.z -= 2 * frontSideSensorPosition;
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            isSensorActive = true;
            if (currentSpeed < 5f && Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength / 2))
            {
                GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, stopForce), ForceMode.Impulse);
            }
        }
        

        //ön sol açılı sensör

        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            Debug.DrawLine(sensorStartPos, hit.point);
            isSensorActive = true;
        }

        if (isSensorActive && hit.collider != null && (hit.collider.gameObject.tag == "car" || hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "PlayerChild") )
        {
            Brake();
        }
        else
        {
            NonBrake();
        }
    }

    private void Brake()
    {
        wheelFL.motorTorque = 0;
        wheelFR.motorTorque = 0;

        wheelFL.brakeTorque = maxMotorTorque * 10;
        wheelFR.brakeTorque = maxMotorTorque * 10;
    }

    private void NonBrake()
    {
        wheelFL.motorTorque = maxMotorTorque;
        wheelFR.motorTorque = maxMotorTorque;

        wheelFL.brakeTorque = 0;
        wheelFR.brakeTorque = 0;
    }
}
