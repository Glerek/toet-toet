using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    public float torque = 10.0f;

    GameObject[] wheelObjects;
    GameObject lightObject;

    public void accel()
    {
        foreach (var wheelObj in wheelObjects)
        {
            if (wheelObj.GetComponent<Wheel>().CanWork())
            {
                wheelObj.GetComponent<Rigidbody2D>().AddTorque(-torque, ForceMode2D.Force);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        wheelObjects = GameObject.FindGameObjectsWithTag("Wheel");
        lightObject = GameObject.FindGameObjectWithTag("Light");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var wheel in wheelObjects)
        {
            wheel.GetComponent<Wheel>().DecreaseDurability(0.1f);
        }
        lightObject.GetComponent<CarLight>().DecreaseDurability(0.1f);
    }

    
}
