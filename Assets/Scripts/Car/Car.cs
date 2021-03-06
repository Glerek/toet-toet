﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
	#region Subclasses & Enum
	public enum WheelPosition
	{
		Front,
		Back
	}

	[Serializable]
	public class WheelStructure
	{
		public WheelPosition Position;
		public WheelJoint2D Joint;
		public Subsystem Wheel;
	}
	#endregion

	[SerializeField]
	private float _speed = 250.0f;

	[SerializeField]
	private float _tortoiseStuckDelay = 3f;

	[SerializeField]
	private float _immobilizedDelay = 2f;

	[SerializeField]
	private List<WheelStructure> _wheels = new List<WheelStructure>();
	public List<WheelStructure> Wheels { get { return _wheels; } }

	[SerializeField]
	private SubsystemContainer _subsystemUI = null;
	public SubsystemContainer SubsystemUI { get { return _subsystemUI; } }

	[SerializeField]
	private RepairUI _repairUI = null;
	public RepairUI RepairUI { get { return _repairUI; } }

	private float _movement = 0f;
	private bool _ongoingRepairMode = false;
	private Rigidbody2D _carRigidbody = null;
	private bool _rigidbodyWasSleeping = false;
	private float _immobilizedTimer = 0f;
	private float _turtleStuckTimer = 0f;
	private bool _carStucked = false;

	#region Events
	private Action<Subsystem> _onSubsystemAdded = null;
	public event Action<Subsystem> OnSubsystemAdded
	{
		add
		{
			_onSubsystemAdded -= value;
			_onSubsystemAdded += value;
		}

		remove
		{
			_onSubsystemAdded -= value;
		}
	}

	private Action<Subsystem> _onSubsystemRemoved = null;
	public event Action<Subsystem> OnSubsystemRemoved
	{
		add
		{
			_onSubsystemRemoved -= value;
			_onSubsystemRemoved += value;
		}

		remove
		{
			_onSubsystemRemoved -= value;
		}
	}

	private Action<bool> _onRepairMode = null;
	public event Action<bool> OnRepairMode
	{
		add
		{
			_onRepairMode -= value;
			_onRepairMode += value;
		}

		remove { _onRepairMode -= value; }
	}

	private Action<bool> _onCarMovementChanged = null;
	public event Action<bool> OnCarMovementChanged
	{
		add
		{
			_onCarMovementChanged -= value;
			_onCarMovementChanged += value;
		}

		remove { _onCarMovementChanged -= value; }
	}

	private Action _onCarStuck = null;
	public event Action OnCarStuck
	{
		add
		{
			_onCarStuck -= value;
			_onCarStuck += value;
		}

		remove { _onCarStuck -= value; }
	}
	#endregion

	void Start()
	{
		_carRigidbody = gameObject.GetComponent<Rigidbody2D>();

		List<Subsystem> subsystems = new List<Subsystem>();
		for (int i = 0; i < _wheels.Count; i++)
		{
			AddWheel(_wheels[i].Wheel as Wheel, _wheels[i].Position);
			subsystems.Add(_wheels[i].Wheel);
		}

		_subsystemUI.Initialize(subsystems);
	}

	public void AddWheel(Wheel wheel, WheelPosition position)
	{
		WheelStructure structure = _wheels.Find(item => item.Position == position);
		wheel.ParentStructure = structure;
		structure.Wheel = wheel;
		structure.Joint.connectedBody = wheel.GetComponent<Rigidbody2D>();

		AddSubsystem(wheel);
	}

	public Subsystem GetWheel(WheelPosition position)
	{
		return _wheels.Find(item => item.Position == position).Wheel;
	}

	private void AddSubsystem(Subsystem subsystem)
	{
		if (_onSubsystemAdded != null)
		{
			_onSubsystemAdded(subsystem);
		}

		subsystem.OnBreakAction += OnSubsystemBroken;
	}

	private void OnSubsystemBroken(Subsystem subsystem)
	{
		subsystem.OnBreakAction -= OnSubsystemBroken;

		switch (subsystem.Data.Type)
		{
			case SubsystemData.SubsystemType.Wheel:
				Wheel wheel = subsystem as Wheel;
				wheel.transform.SetParent(transform.root);
				wheel.GetComponent<Rigidbody2D>().AddTorque(-30f);
				wheel.gameObject.layer = LayerMask.NameToLayer("ToDespawn");

				WheelStructure structure = _wheels.Find(item => item.Wheel == wheel);
				structure.Joint.connectedBody = null;
				structure.Wheel = null;
				break;

			default:
				Debug.LogWarning($"Break not implemented for {subsystem.Data.Type}");
				break;
		}

		if (_onSubsystemRemoved != null)
		{
			_onSubsystemRemoved(subsystem);
		}
	}

	public void SetMovement(float rawMovement)
	{
		_movement = - rawMovement * _speed;
	}

	public void SetRepairMode(bool enable)
	{
		_ongoingRepairMode = enable;
		FreezeCar(enable);
		
		if (_onRepairMode != null)
		{
			_onRepairMode(enable);
		}
	}

	public void FreezeCar(bool freeze)
	{
		_carRigidbody.constraints = freeze ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;
	}

	private void Update()
	{
		if (_carRigidbody != null)
		{
			bool vehicleImmobilized = IsVehicleImmobilized();
			if (vehicleImmobilized != _rigidbodyWasSleeping)
			{
				_rigidbodyWasSleeping = vehicleImmobilized;

				if (_onCarMovementChanged != null)
				{
					_onCarMovementChanged(_rigidbodyWasSleeping);
				}
			}

			if (_carStucked == false)
			{
				CheckForTurtleStuck();
			}
		}
	}

	private bool IsVehicleImmobilized()
	{
		_immobilizedTimer += Time.deltaTime;
		if (Vector2.Distance(_carRigidbody.velocity, Vector2.zero) > 0.1)
		{
			_immobilizedTimer = 0f;
		}

		return _immobilizedTimer > _immobilizedDelay;
	}

	private void CheckForTurtleStuck()
	{
		// If all wheels are airborne for more than the corresponding timer we're stuck
		// (Maybe at some point also check that we're not moving on the x-axis)
		bool allWheelsAirborne = true;
		for (int i = 0; i < _wheels.Count; i++)
		{
			Vector3 wheelPosition = transform.position + new Vector3(_wheels[i].Joint.anchor.x, _wheels[i].Joint.anchor.y, 0);
			RaycastHit2D hit = Physics2D.Raycast(wheelPosition, -transform.up, 1.5f, LayerMask.GetMask("Road"));
			allWheelsAirborne &= hit.collider == null;
		}

		_turtleStuckTimer += Time.deltaTime;

		if (!allWheelsAirborne)
		{
			_turtleStuckTimer = 0f;
		}

		if (_turtleStuckTimer > _tortoiseStuckDelay)
		{
			_carStucked = true;
			_turtleStuckTimer = 0f;
			_onCarStuck?.Invoke();
		}
	}

	private void FixedUpdate()
	{
		if (_ongoingRepairMode == false)
		{
			for (int i = 0; i < _wheels.Count; i++)
			{
				bool isMoving = _movement != 0f;
				_wheels[i].Joint.useMotor = isMoving;

				if (isMoving)
				{
					JointMotor2D motor = new JointMotor2D { motorSpeed = _movement, maxMotorTorque = 10000 };
					_wheels[i].Joint.motor = motor;
				}
			}
		}
	}
}
