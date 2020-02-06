using System;
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
	private float _torque = 10.0f;

	[SerializeField]
	private List<WheelStructure> _wheels = new List<WheelStructure>();

	[SerializeField]
	private SubsystemContainer _subsystemUI = null;
	public SubsystemContainer SubsystemUI { get { return _subsystemUI; } }

	[SerializeField]
	private RepairUI _repairUI = null;
	public RepairUI RepairUI { get { return _repairUI; } }

	private bool _ongoingRepairMode = false;

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

	void Start()
	{
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
		wheel.ParentStructure = _wheels.Find(item => item.Position == position);

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
		if (_ongoingRepairMode == false)
		{
			for (int i = 0; i < _wheels.Count; i++)
			{
				if (_wheels[i].Wheel != null && !_wheels[i].Wheel.IsBroken)
				{
					_wheels[i].Wheel.GetComponent<Rigidbody2D>().AddTorque(torque, ForceMode2D.Force);
				}
			}
		}
	}

	public void SetRepairMode(bool enable)
	{
		_ongoingRepairMode = enable;
		
		if (_onRepairMode != null)
		{
			_onRepairMode(enable);
		}
	}
}
