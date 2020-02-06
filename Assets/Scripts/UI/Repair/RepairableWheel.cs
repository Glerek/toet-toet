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

	private void OnItemDragStart(InventoryItem item, PointerEventData pointerData)
	{
		if (_ongoingRepairMode &&
			item.Data.Type == SubsystemData.SubsystemType.Wheel &&
			GameManager.Instance.Car.GetWheel(_wheelPosition) == null)
		{
			SetAllowedTarget(true);
		}
	}

	private void OnItemDragStop(InventoryItem item, PointerEventData pointerData)
	{
		if (_allowedTarget)
		{
			// TODO MOVE THAT TO INVENTORY ITEM 
			Ray ray = _repairUI.RepairCamera.ScreenPointToRay(pointerData.position);
			RaycastHit2D hit2D =  Physics2D.GetRayIntersection(ray, 5f, LayerMask.GetMask(new string[] {"RepairablePart"})); 
			if (hit2D.collider != null && hit2D.collider.gameObject == gameObject)
			{
				Debug.Log($"Dropped item on {gameObject.name}");
			}

		}
		// TODO do that

		// TODO Also Repair the wheel and remove it from inventory
		SetAllowedTarget(false);
	}
}