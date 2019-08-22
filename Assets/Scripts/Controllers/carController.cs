using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{

    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;

    public Joystick joystick;

    public WheelCollider frontWheelL, frontWheelR;
    public WheelCollider rearWheelL, rearWheelR;

    public Transform frontTransformL, frontTransformR;
    public Transform rearTransformL, rearTransformR;

    public float maxSteerAngle = 30;
    public float motor = 50;
    public float brakePower = 2;

    private Rigidbody rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void GetInput()
    {
        horizontalInput = joystick.Horizontal;
        verticalInput = -joystick.Vertical;
    }

    private void Steer()
    {
        steerAngle = maxSteerAngle * horizontalInput;
        frontWheelL.steerAngle = steerAngle;
        frontWheelR.steerAngle = steerAngle;
    }

    private void Accelerate()
    {
        frontWheelL.motorTorque = verticalInput * motor;
        frontWheelR.motorTorque = verticalInput * motor;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontWheelL, frontTransformL);
        UpdateWheelPose(frontWheelR, frontTransformR);
        UpdateWheelPose(rearWheelL, rearTransformL);
        UpdateWheelPose(rearWheelR, rearTransformR);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    public void Brake()
    {
        Debug.Log("frenliyoruz");
       frontWheelL.brakeTorque = motor * 2;
       frontWheelR.brakeTorque = motor * 2;
    }

    public void NonBrake()
    {
      frontWheelL.brakeTorque = 0;
      frontWheelR.brakeTorque = 0;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();

        UpdateWheelPoses();
    }

}
