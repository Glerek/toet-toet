using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryContainer : MonoBehaviour
{
	[SerializeField]
	private GameObject _itemsContainer = null;

	[SerializeField]
	private List<InventoryItem> _items = new List<InventoryItem>(InventoryManager.MAX_INVENTORY_SIZE);

	private static Action<InventoryItem, PointerEventData> _onItemStartDragEvent = null;
	public static event Action<InventoryItem, PointerEventData> OnItemStartDragEvent
	{
		add
		{
			_onItemStartDragEvent -= value;
			_onItemStartDragEvent += value;
		}

		remove { _onItemStartDragEvent -= value; }
	}

	private static Action<InventoryItem, PointerEventData> _onItemStopDragEvent = null;
	public static event Action<InventoryItem, PointerEventData> OnItemStopDragEvent
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
			_items[i].SetData(newInventory[i]);
		}
	}

	public void ForceDisplay(bool forceShow)
	{
		_forceShow = forceShow;
		OnDisplayInventory(_forceShow);
	}

	private void OnStartDrag(InventoryItem item, PointerEventData pointerEventData)
	{
		_onItemStartDragEvent?.Invoke(item, pointerEventData);
	}

	private void OnStopDrag(InventoryItem item, PointerEventData pointerEventData)
	{
		_onItemStopDragEvent?.Invoke(item, pointerEventData);
	}
}
