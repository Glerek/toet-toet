using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : Pickable
{
    public override void OnBroken()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var mag = collision.relativeVelocity.magnitude;
        Debug.Log("Wheel: " + mag);
        DecreaseDurability(mag);
    }
}
