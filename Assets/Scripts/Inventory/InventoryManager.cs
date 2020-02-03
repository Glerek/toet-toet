
using System;
using System.Collections.Generic;

public class InventoryManager : Singleton<InventoryManager>
{
	public const int MAX_INVENTORY_SIZE = 3;
	// private List<Pickable> _inventory = new List<Pickable>();
	// public List<Pickable> Inventory
	// {
	// 	get { return _inventory; }
	// }

	// private Action _onInventoryChanged = null;
	// public event Action OnInventoryChanged
	// {
	// 	add
	// 	{
	// 		_onInventoryChanged -= value;
	// 		_onInventoryChanged += value;
	// 	}

	// 	remove { _onInventoryChanged -= value; }
	// }

	// public void AddToInventory(Pickable pickup)
	// {
	// 	if (_inventory.Count < MAX_INVENTORY_SIZE)
	// 	{
	// 		_inventory.Add(pickup);

	// 		if (_onInventoryChanged != null)
	// 			_onInventoryChanged();
	// 	}
	// }

	// public void RemoveFromInventory(Pickable pickup)
	// {
	// 	if (_inventory.Contains(pickup))
	// 	{
	// 		_inventory.Remove(pickup);

	// 		if (_onInventoryChanged != null)
	// 			_onInventoryChanged();
	// 	}
	// }
}