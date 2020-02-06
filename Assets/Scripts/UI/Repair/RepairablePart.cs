using UnityEngine;
using UnityEngine.EventSystems;

public abstract class RepairablePart : MonoBehaviour
{
	[SerializeField]
	private SubsystemData.SubsystemType _type = SubsystemData.SubsystemType.Count;

	protected RepairUI _repairUI = null;
	protected bool _ongoingRepairMode = false;
	protected bool _allowedTarget = false;

	private void Start()
	{
		_repairUI = GameManager.Instance.Car.RepairUI;
		GameManager.Instance.Car.OnRepairMode += OnRepairMode;

		Initialize();
	}

	private void OnDestroy()
	{
		GameManager.Instance.Car.OnRepairMode -= OnRepairMode;
		Release();
	}

	private void OnRepairMode(bool ongoingRepairMode)
	{
		_ongoingRepairMode = ongoingRepairMode;
	}

	protected abstract void Initialize();
	protected abstract void Release();

	protected void SetAllowedTarget(bool allowed)
	{
		if (_allowedTarget != allowed)
		{
			_allowedTarget = allowed;

			DisplayRepairHighlight(_allowedTarget);
		}
	}

	private void DisplayRepairHighlight(bool display)
	{
		// TODO
		Debug.Log($"Display Highlight for {gameObject.name}: {display}");
	}
}