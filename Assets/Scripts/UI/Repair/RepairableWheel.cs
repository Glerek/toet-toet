using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
public class RepairableWheel : RepairablePart
{
	[SerializeField]
	private Car.WheelPosition _wheelPosition = Car.WheelPosition.Back;

	// At start fetch the right subsystem from the car and then enable / disable if broken or not!!
	// Then autoa highlight from OnRepairMode + BeginDrag
	// Plus from OnEndDrag from Item do the repair

	protected override void Initialize()
	{
		InventoryContainer.OnItemStartDragEvent += OnItemDragStart;
		InventoryContainer.OnItemStopDragEvent += OnItemDragStop;
	}

	protected override void Release()
	{
		InventoryContainer.OnItemStartDragEvent -= OnItemDragStart;
		InventoryContainer.OnItemStopDragEvent -= OnItemDragStop;
	}

	private void OnItemDragStart(InventoryItem item)
	{
		if (_ongoingRepairMode &&
			item.Data.Type == SubsystemData.SubsystemType.Wheel &&
			(GameManager.Instance.CurrentGameMode as DrivingMode).Car.GetWheel(_wheelPosition) == null)
		{
			SetAllowedTarget(true);
		}
	}

	private void OnItemDragStop(InventoryItem item)
	{
		SetAllowedTarget(false);
	}

	public override void DropItem(InventoryItem item)
	{
		if (_allowedTarget)
		{
			if (item.Data.Type == _type)
			{
				Debug.Log($"{item.Data.Name} dropped onto {gameObject.name}");

				if (item.Data.PrefabTemplate is Wheel)
				{
					Wheel newWheel = GameObject.Instantiate(item.Data.PrefabTemplate) as Wheel;
					Car vehicle = (GameManager.Instance.CurrentGameMode as DrivingMode).Car;

					newWheel.transform.SetParent(transform);
					newWheel.transform.localPosition = Vector3.zero;
					vehicle.AddWheel(newWheel, _wheelPosition);
					InventoryManager.Instance.RemoveFromInventory(item.Data);
					vehicle.transform.Translate(vehicle.transform.up * (newWheel.GetWorldSize().y / 2f));
				}
			}
			else
			{
				Debug.LogError($"Dropped a {item.Data.Type} item onto a {_type} part. Should never happen");
			}
		}
	}
}