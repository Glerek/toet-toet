using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContainer : MonoBehaviour
{
	[SerializeField]
	private GameObject _itemsContainer = null;

	[SerializeField]
	private List<InventoryItem> _items = new List<InventoryItem>(InventoryManager.MAX_INVENTORY_SIZE);

	private void Start()
	{
		OnDisplayInventory(false);

		InventoryManager.Instance.DisplayInventoryCallback += OnDisplayInventory;
		InventoryManager.Instance.OnInventoryChanged += OnInventoryChanged;
	}

	private void OnDestroy()
	{
		InventoryManager.Instance.DisplayInventoryCallback -= OnDisplayInventory;
		InventoryManager.Instance.OnInventoryChanged -= OnInventoryChanged;
	}

	private void OnDisplayInventory(bool show)
	{
		_itemsContainer.SetActive(show);
	}

	private void OnInventoryChanged(List<SubsystemData> newInventory)
	{
		for (int i = 0; i < newInventory.Count; i++)
		{
			_items[i].SetSprite(newInventory[i] != null ? newInventory[i].Icon : null);
		}
	}
}
