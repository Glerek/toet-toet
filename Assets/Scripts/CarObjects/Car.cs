using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    public float torque = 10.0f;

    GameObject[] tires;

    public void accel()
    {
        foreach (var tire in tires)
        {
            tire.GetComponent<Rigidbody2D>().AddTorque(-torque, ForceMode2D.Force);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tires = GameObject.FindGameObjectsWithTag("Tire");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    
}
