
using System;
using System.Collections.Generic;

public class InventoryManager : Singleton<InventoryManager>
{
	public const int MAX_INVENTORY_SIZE = 3;
	private List<SubsystemData>_inventory = new List<SubsystemData>(MAX_INVENTORY_SIZE);
	public List<SubsystemData> Inventory
	{
		get { return _inventory; }
	}

	private Action<List<SubsystemData>> _onInventoryChanged = null;
	public event Action<List<SubsystemData>> OnInventoryChanged
	{
		add
		{
			_onInventoryChanged -= value;
			_onInventoryChanged += value;
		}

		remove { _onInventoryChanged -= value; }
	}

	private Action<bool> _displayInventoryCallback = null;
	public event Action<bool> DisplayInventoryCallback
	{
		add
		{
			_displayInventoryCallback -= value;
			_displayInventoryCallback += value;
		}

		remove { _displayInventoryCallback -= value; }
	}

	public void AddToInventory(SubsystemData subsystem)
	{
		if (_inventory.Count < MAX_INVENTORY_SIZE)
		{
			_inventory.Add(subsystem);

			if (_onInventoryChanged != null)
				_onInventoryChanged(_inventory);
		}
	}

	public void RemoveFromInventory(SubsystemData subsystem)
	{
		if (_inventory.Contains(subsystem))
		{
			_inventory.Remove(subsystem);

			if (_onInventoryChanged != null)
				_onInventoryChanged(_inventory);
		}
	}

	public void DisplayInventory(bool show)
	{
		if (_displayInventoryCallback != null)
		{
			_displayInventoryCallback(show);
		}
	}
}