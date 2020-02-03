using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
	#region Subclasses
	[Serializable]
	public class WheelStructure
	{
		public WheelJoint2D Joint;
		public Subsystem Wheel;
	}
	#endregion

	[SerializeField]
	private float _torque = 10.0f;

	[SerializeField]
	private WheelStructure _backWheel = null;

	[SerializeField]
	private WheelStructure _frontWheel = null;

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

	// TODO LATER
	// [SerializeField]
	// private List<CarLight> _lights = new List<CarLight>();
	// public float LightsDurability
	// {
	// 	get
	// 	{
	// 		float totalDurability = 0f;
	// 		for (int i = 0; i < _lights.Count; i++)
	// 		{
	// 			totalDurability += _lights[i].Durability;
	// 		}

	// 		return totalDurability / (float)_lights.Count;
	// 	}
	// }

	// private bool _canMove = true;
	// public bool CanMove
	// {
	// 	get { return _canMove; }
	// 	set
	// 	{
	// 		if (_canMove != value)
	// 		{
	// 			_canMove = value;

	// 			DrivingUI.Instance.Display(_canMove);
	// 			RepairingUI.Instance.Display(!_canMove);
	// 		}
	// 	}
	// }

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
		_backWheel.Wheel.OnBreak += OnWheelBroken;
		_frontWheel.Wheel.OnBreak += OnWheelBroken;
	}

	private void OnWheelBroken(Subsystem wheel)
	{
		// Wheel brokenWheel = wheel as Wheel;
		// brokenWheel.OnBreakAction -= OnWheelBroken;

		// if (_backWheel.Wheel == brokenWheel)
		// {
		// 	_backWheel.Joint.connectedBody = null;
		// 	_backWheel.Wheel.transform.SetParent(transform.parent);
		// 	_backWheel.Wheel.GetComponent<Rigidbody2D>().AddTorque(-30f);
		// 	_backWheel.Wheel = null;
		// }

		// if (_frontWheel.Wheel == brokenWheel)
		// {
		// 	_frontWheel.Joint.connectedBody = null;
		// 	_frontWheel.Wheel.transform.SetParent(transform.parent);
		// 	_frontWheel.Wheel.GetComponent<Rigidbody2D>().AddTorque(-30f);
		// 	_frontWheel.Wheel = null;
		// }

		// CanMove = _backWheel.Wheel != null && _frontWheel.Wheel != null;
	}

	public void Accelerate()
	{
		ApplyTorque(-_torque);
	}

	public void Brake()
	{
		ApplyTorque(_torque);
	}

	private void ApplyTorque(float torque)
	{
		if (_backWheel.Wheel != null && !_backWheel.Wheel.IsBroken)
		{
			_backWheel.Wheel.GetComponent<Rigidbody2D>().AddTorque(torque, ForceMode2D.Force);
		}

		if (_frontWheel.Wheel != null && !_frontWheel.Wheel.IsBroken)
		{
			_frontWheel.Wheel.GetComponent<Rigidbody2D>().AddTorque(torque, ForceMode2D.Force);
		}
	}
}
