using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    public float torque = 10.0f;

    GameObject[] wheelObjects;
	public GameObject[] WheelObjects { get { return wheelObjects; } }
    GameObject lightObject;
	public GameObject LightObject { get { return lightObject; } }

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

    private IEnumerator DecreaseDurability()
    {
        while (true)
        {
            foreach (var wheel in wheelObjects)
            {
                wheel.GetComponent<Wheel>().DecreaseDurability(5.0f);
            }
            lightObject.GetComponent<CarLight>().DecreaseDurability(5.0f);

            yield return new WaitForSeconds(1.0f);
        }
    }

    
}
