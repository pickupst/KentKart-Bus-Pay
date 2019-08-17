using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class busController : MonoBehaviour
{

    public float speed = 5f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveVertical, 0.0f, -moveHorizontal);

        rb.AddForce(movement * speed);
        rb.AddTorque(Vector3.up * speed * moveHorizontal, ForceMode.Impulse);
    }
}
