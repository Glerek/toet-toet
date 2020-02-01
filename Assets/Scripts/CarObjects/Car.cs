using System;
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
	private bool _canMove = true;
	public bool CanMove
	{
		get { return _canMove; }
		private set
		{
			if (_canMove != value)
			{
				_canMove = value;

				DrivingUI.Instance.Display(_canMove);
				RepairingUI.Instance.Display(!_canMove);
			}
		}
	}

	private Action _onVehicleStuck = null;
	public event Action OnVehicleStuck
	{
		add 
		{
			_onVehicleStuck -= value;
			_onVehicleStuck += value;
		}

		remove
		{
			_onVehicleStuck -= value;
		}
	}

    void Start()
    {
		for (int i = 0; i < _wheels.Count; i++)
		{
			_wheels[i].OnBreakAction += OnWheelBroken;
		}

        StartCoroutine(DecreaseDurability());
    }

	private void OnWheelBroken(Pickable wheel)
	{
		wheel.OnBreakAction -= OnWheelBroken;

		bool allWheelsBroken = true;
		for (int i = 0; i < _wheels.Count; i++)
		{
			allWheelsBroken &= !_wheels[i].CanWork();
		}

		_canMove = !allWheelsBroken;
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

		if (_duringAcceleration && _canMove)
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
		else
		{
			_duringAcceleration = false;
		}
    }
}
