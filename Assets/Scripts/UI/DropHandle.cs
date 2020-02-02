using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropHandle : MonoBehaviour, IDropHandler
{

    # region IDrophandler implementation
    public void OnDrop(PointerEventData eventData)
    {
		GameObject target = eventData.pointerEnter;
		GameObject draggedItem = eventData.pointerDrag;

		string index = draggedItem.name.Substring(draggedItem.name.Length - 1, 1);
		Debug.Log(index);
		int inventoryIndex = int.Parse(index);
		Wheel draggedWheel = InventoryManager.Instance.Inventory[0] as Wheel;

		draggedWheel.transform.SetParent(GameManager.Instance.Car.transform);
		draggedWheel.transform.localPosition = Vector3.zero;
		draggedWheel.Durability = Pickable.MAX_DURABILITY;

		switch (target.name)
		{
			case "FrontWheel":
				GameManager.Instance.Car.FrontWheel.Wheel = draggedWheel;
				GameManager.Instance.Car.FrontWheel.Joint.connectedBody = draggedWheel.GetComponent<Rigidbody2D>();
				break;

			case "BackWheel":
				GameManager.Instance.Car.BackWheel.Wheel = draggedWheel;
				GameManager.Instance.Car.BackWheel.Joint.connectedBody = draggedWheel.GetComponent<Rigidbody2D>();
				break;
		}
		
		InventoryManager.Instance.RemoveFromInventory(draggedWheel);
		GameManager.Instance.Car.CanMove = true;

        // var raycastResults = new List<RaycastResult>();
        // EventSystem.current.RaycastAll(eventData, raycastResults);
        // foreach (var hit in raycastResults)
        // {
        //     if (hit.gameObject.name == "FrontWheel")
        //     {


        //         this.enabled = false;
        //     }
        //     else if (hit.gameObject.name == "BackWheel")
        //     {
        //         this.enabled = false;
        //     }
        // }
    }
    #endregion
}
