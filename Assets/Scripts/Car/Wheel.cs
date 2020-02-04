using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : Subsystem
{
    private Car.WheelStructure _parentStructure = null;
    public Car.WheelStructure ParentStructure
    {
        get { return _parentStructure; }
        set { _parentStructure = value; }
    }
}
