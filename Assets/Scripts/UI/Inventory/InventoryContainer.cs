using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryContainer : MonoBehaviour
{
	[SerializeField]
	private GameObject _itemsContainer = null;

	[SerializeField]
	private List<InventoryItem> _items = new List<InventoryItem>(InventoryManager.MAX_INVENTORY_SIZE);

	private static Action<InventoryItem> _onItemStartDragEvent = null;
	public static event Action<InventoryItem> OnItemStartDragEvent
	{
		add
		{
			_onItemStartDragEvent -= value;
			_onItemStartDragEvent += value;
		}

		remove { _onItemStartDragEvent -= value; }
	}

	private static Action<InventoryItem> _onItemStopDragEvent = null;
	public static event Action<InventoryItem> OnItemStopDragEvent
	{
		add
		{
			_onItemStopDragEvent -= value;
			_onItemStopDragEvent += value;
		}

		remove { _onItemStopDragEvent -= value; }
	}

	private bool _forceShow = false;

	private void Start()
	{
		for (int i = 0; i < _items.Count; i++)
		{
			_items[i].Initialize(OnStartDrag, OnStopDrag);
		}

		OnDisplayInventory(false);

		InventoryManager.Instance.DisplayInventoryCallback += OnDisplayInventory;
		InventoryManager.Instance.OnInventoryChanged += OnInventoryChanged;
		(GameManager.Instance.CurrentGameMode as DrivingMode).Car.OnRepairMode += ForceDisplay;
	}

	private void OnDestroy()
	{
		if (InventoryManager.HasInstance)
		{
			InventoryManager.Instance.DisplayInventoryCallback -= OnDisplayInventory;
			InventoryManager.Instance.OnInventoryChanged -= OnInventoryChanged;
		}

		if (GameManager.HasInstance)
		{
			(GameManager.Instance.CurrentGameMode as DrivingMode).Car.OnRepairMode -= ForceDisplay;
		}
	}

	private void OnDisplayInventory(bool show)
	{
		_itemsContainer.SetActive(_forceShow || show);
	}

	private void OnInventoryChanged(List<SubsystemData> newInventory)
	{
		for (int i = 0; i < _items.Count; i++)
		{
			_items[i].SetData(newInventory.Count > i ? newInventory[i] : null);
		}
	}

	public void ForceDisplay(bool forceShow)
	{
		_forceShow = forceShow;
		OnDisplayInventory(_forceShow);
	}

	private void OnStartDrag(InventoryItem item)
	{
		_onItemStartDragEvent?.Invoke(item);
	}

	private void OnStopDrag(InventoryItem item)
	{
		_onItemStopDragEvent?.Invoke(item);
	}
}
