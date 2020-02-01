using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
	[SerializeField]
    private float _torque = 10.0f;

	[SerializeField]
	private float _durabilityDecreasePerSecond = 2f;

	[SerializeField]
	private List<Wheel> _wheels = new List<Wheel>();
	public float WheelsDurability
	{
		get
		{
			float totalDurability = 0f;
			for (int i = 0; i < _wheels.Count; i++)
			{
				totalDurability += _wheels[i].Durability;
			}

			return totalDurability / (float)_wheels.Count;
		}
	}

	[SerializeField]
	private List<CarLight> _lights = new List<CarLight>();
	public float LightsDurability
	{
		get
		{
			float totalDurability = 0f;
			for (int i = 0; i < _lights.Count; i++)
			{
				totalDurability += _lights[i].Durability;
			}

			return totalDurability / (float)_lights.Count;
		}
	}

	private bool _duringAcceleration = false;

    void Start()
    {
        StartCoroutine(DecreaseDurability());
    }

    private IEnumerator DecreaseDurability()
    {
		float timer = 0f;
        while (true)
        {
			timer = 0f;

			if (_duringAcceleration)
			{
				foreach (var wheel in _wheels)
				{
					wheel.DecreaseDurability(_durabilityDecreasePerSecond);
				}

				foreach(var light in _lights)
				{
					light.DecreaseDurability(_durabilityDecreasePerSecond);
				}

				while (timer < 1f && _duringAcceleration)
				{
					timer += Time.deltaTime;
					yield return null;
				}
			}

			yield return null;
        }
    }

    public void HandleMovement(bool accelerate)
    {
		_duringAcceleration = accelerate;

		if (_duringAcceleration)
		{
			foreach (var wheel in _wheels)
			{
				if (wheel.CanWork())
				{
					wheel.GetComponent<Rigidbody2D>().AddTorque(-_torque, ForceMode2D.Force);
				}
				else
				{
					_duringAcceleration = false;
				}
			}
		}
    }
}
