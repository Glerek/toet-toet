using UnityEngine;
using UnityEngine.EventSystems;

public abstract class RepairablePart : MonoBehaviour
{
	[SerializeField]
	private SubsystemData.SubsystemType _type = SubsystemData.SubsystemType.Count;
	protected bool _ongoingRepairMode = false;
	private bool _highlightDisplayed = false;

	private void Start()
	{
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

	protected void DisplayRepairHighlight(bool display)
	{
		if (_highlightDisplayed != display)
		{
			// TODO
			Debug.Log($"Display Highlight for {gameObject.name}: {display}");
			_highlightDisplayed = display;
		}
	}
}