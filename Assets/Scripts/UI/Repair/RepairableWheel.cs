using UnityEngine;
using UnityEngine.EventSystems;

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
			DisplayRepairHighlight(true);
		}
	}

	private void OnItemDragStop(InventoryItem item, PointerEventData pointerData)
	{
		// TODO do that

        // Ray ray = _repairCamera.ScreenPointToRay(Input.mousePosition);
        // Debug.DrawRay(ray.origin, 5f * ray.direction, Color.red, 5f);
        // RaycastHit2D hit2D =  Physics2D.GetRayIntersection(ray, 5f, LayerMask.GetMask(new string[] {"Car", "Wheel"})); 
        // if (hit2D.collider != null)
        // {
        //     Debug.Log(hit2D.collider);
        // }

		// TODO Also Repair the wheel and remove it from inventory
		DisplayRepairHighlight(false);
	}
}