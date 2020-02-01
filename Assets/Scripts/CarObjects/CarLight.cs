using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLight : Pickable
{
    protected override void OnBroken()
    {
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var mag = collision.relativeVelocity.magnitude;
        DecreaseDurability(mag);
    }
}
