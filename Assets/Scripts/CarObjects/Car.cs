using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    public float torque = 10.0f;
    public int gus = 100;

    GameObject[] tires;

    public void accel()
    {
        if (gus > 0)
        {
            foreach (var tire in tires)
            {
                tire.GetComponent<Rigidbody2D>().AddTorque(-torque, ForceMode2D.Force);
            }
            gus--;
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
