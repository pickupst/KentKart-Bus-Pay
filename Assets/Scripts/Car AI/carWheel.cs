using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carWheel : MonoBehaviour
{

    public WheelCollider targetWheelCollider;
    private Vector3 wheelPosition = new Vector3();
    private Quaternion wheelRotation = new Quaternion();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        targetWheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);
        transform.position = wheelPosition;
        transform.rotation = wheelRotation;

    }
}
