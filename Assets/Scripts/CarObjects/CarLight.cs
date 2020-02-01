using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLight : Pickable
{
    public override void OnBroken()
    {
        gameObject.SetActive(false);
    }
}
