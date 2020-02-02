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

	[Serializable]
	public class WheelStructure
	{
		public WheelJoint2D Joint;
		public Wheel Wheel;
	}

	[SerializeField]
	private WheelStructure _backWheel = null;
	public WheelStructure BackWheel
	{
		get { return _backWheel; }
	}

	[SerializeField]
	private WheelStructure _frontWheel = null;
	public WheelStructure FrontWheel
	{
		get { return _frontWheel; }
	}

	public float WheelsDurability
	{
		get
		{
			float totalDurability = 0f;
			float amountOfWheels = 0f;

			if (_backWheel.Wheel != null)
			{
				totalDurability += _backWheel.Wheel.Durability;
				amountOfWheels++;
			}

			if (_frontWheel.Wheel != null)
			{
				totalDurability += _frontWheel.Wheel.Durability;
				amountOfWheels++;
			}
			
			return totalDurability / amountOfWheels;
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
	private bool _duringBreak = true;
	private bool _canMove = true;
	public bool CanMove
	{
		get { return _canMove; }
		set
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
		_backWheel.Wheel.OnBreakAction += OnWheelBroken;
		_frontWheel.Wheel.OnBreakAction += OnWheelBroken;

		// StartCoroutine(DecreaseDurability());
	}

	private void OnWheelBroken(Pickable wheel)
	{
		Wheel brokenWheel = wheel as Wheel;
		brokenWheel.OnBreakAction -= OnWheelBroken;

		if (_backWheel.Wheel == brokenWheel)
		{
			_backWheel.Joint.connectedBody = null;
			_backWheel.Wheel.transform.SetParent(transform.parent);
			_backWheel.Wheel.GetComponent<Rigidbody2D>().AddTorque(-30f);
			_backWheel.Wheel = null;
		}

		if (_frontWheel.Wheel == brokenWheel)
		{
			_frontWheel.Joint.connectedBody = null;
			_frontWheel.Wheel.transform.SetParent(transform.parent);
			_frontWheel.Wheel.GetComponent<Rigidbody2D>().AddTorque(-30f);
			_frontWheel.Wheel = null;
		}

		CanMove = _backWheel.Wheel != null && _frontWheel.Wheel != null;
	}

	// private IEnumerator DecreaseDurability()
	// {
	// 	float timer = 0f;
	// 	while (true)
	// 	{
	// 		timer = 0f;

	// 		if (_duringAcceleration)
	// 		{
	// 			_backWheel.Wheel.DecreaseDurability(_durabilityDecreasePerSecond);
	// 			_frontWheel.Wheel.DecreaseDurability(_durabilityDecreasePerSecond);

	// 			foreach (var light in _lights)
	// 			{
	// 				light.DecreaseDurability(_durabilityDecreasePerSecond);
	// 			}

	// 			while (timer < 1f && _duringAcceleration)
	// 			{
	// 				timer += Time.deltaTime;
	// 				yield return null;
	// 			}
	// 		}

	// 		yield return null;
	// 	}
	// }

	public void Accel(bool enabled)
	{
		_duringAcceleration = enabled;

		if (enabled && _canMove)
		{
			if (_backWheel.Wheel != null && _backWheel.Wheel.CanWork())
			{
				_backWheel.Wheel.GetComponent<Rigidbody2D>().AddTorque(-_torque, ForceMode2D.Force);
			}

			if (_frontWheel.Wheel != null && _frontWheel.Wheel.CanWork())
			{
				_frontWheel.Wheel.GetComponent<Rigidbody2D>().AddTorque(-_torque, ForceMode2D.Force);
			}
		}
	}

	public void Brake(bool enabled)
	{
		_duringBreak = enabled;

		if (enabled && _canMove)
		{
			if (_backWheel.Wheel != null && _backWheel.Wheel.CanWork())
			{
				_backWheel.Wheel.GetComponent<Rigidbody2D>().AddTorque(_torque, ForceMode2D.Force);
			}

			if (_frontWheel.Wheel != null && _frontWheel.Wheel.CanWork())
			{
				_frontWheel.Wheel.GetComponent<Rigidbody2D>().AddTorque(_torque, ForceMode2D.Force);
			}
		}
	}
}
