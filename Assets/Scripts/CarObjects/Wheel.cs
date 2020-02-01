using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : Pickable
{
    protected override void OnBroken() {}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var mag = collision.relativeVelocity.magnitude;
        DecreaseDurability(mag);
    }
}
