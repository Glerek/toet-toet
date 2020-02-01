using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    GameObject[] tires;

    // Start is called before the first frame update
    void Start()
    {
        tires = GameObject.FindGameObjectsWithTag("Tire");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var tire in tires)
        {
            tire.GetComponent<Rigidbody2D>().AddTorque(-10, ForceMode2D.Force);
        }
    }
}
