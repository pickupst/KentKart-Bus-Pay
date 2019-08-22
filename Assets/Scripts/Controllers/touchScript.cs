using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchScript : MonoBehaviour
{
    public GameObject carControllerScript;

    private int brakeFinger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < 2; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Stationary && Input.GetTouch(i).position.x > Screen.width / 2)
            {
                brakeFinger = i;
                carControllerScript.GetComponent<carController>().Brake();
            }

            if (Input.GetTouch(brakeFinger).phase == TouchPhase.Ended)
            {
                carControllerScript.GetComponent<carController>().NonBrake();
            }
        }

    }
}
