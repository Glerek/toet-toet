using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : Pickable
{
	private bool _attachedToVehicle = true;

    protected override void OnBroken()
	{
		SpriteRenderer.color = Color.red;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var mag = collision.relativeVelocity.magnitude;
		// Debug.Log(mag);

		if (mag > GameManager.Instance.CollisionMagnitudeThreshold)
		{
			DecreaseDurability(mag);
		}
    }
}
