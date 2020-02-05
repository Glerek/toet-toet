using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContainer : MonoBehaviour
{
	[SerializeField]
	private GameObject _itemsContainer = null;

	[SerializeField]
	private List<InventoryItem> _items = new List<InventoryItem>(InventoryManager.MAX_INVENTORY_SIZE);

	private bool _forceShow = false;

	private void Start()
	{
		OnDisplayInventory(false);

		InventoryManager.Instance.DisplayInventoryCallback += OnDisplayInventory;
		InventoryManager.Instance.OnInventoryChanged += OnInventoryChanged;
		GameManager.Instance.Car.OnRepairMode += ForceDisplay;
	}

	private void OnDestroy()
	{
		InventoryManager.Instance.DisplayInventoryCallback -= OnDisplayInventory;
		InventoryManager.Instance.OnInventoryChanged -= OnInventoryChanged;
		GameManager.Instance.Car.OnRepairMode -= ForceDisplay;
	}

	private void OnDisplayInventory(bool show)
	{
		_itemsContainer.SetActive(_forceShow || show);
	}

	private void OnInventoryChanged(List<SubsystemData> newInventory)
	{
		for (int i = 0; i < newInventory.Count; i++)
		{
			_items[i].SetSprite(newInventory[i] != null ? newInventory[i].Icon : null);
		}
	}

	public void ForceDisplay(bool forceShow)
	{
		_forceShow = forceShow;
		OnDisplayInventory(_forceShow);
	}
}
