using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private Vector2 speed = new Vector2(50, 10);
    private Wheel frontWheel = new Wheel();
    private Wheel backWheel = new Wheel();
    private CarLight carLight = new CarLight();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
