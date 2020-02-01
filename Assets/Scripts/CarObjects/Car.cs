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

        StartCoroutine(DecreaseDurability());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Car: " + collision.relativeVelocity.magnitude);
    }

    private IEnumerator DecreaseDurability()
    {
        while (true)
        {
            foreach (var wheel in wheelObjects)
            {
                wheel.GetComponent<Wheel>().DecreaseDurability(1.0f);
            }
            lightObject.GetComponent<CarLight>().DecreaseDurability(1.0f);

            yield return new WaitForSeconds(1.0f);
        }
    }
}
