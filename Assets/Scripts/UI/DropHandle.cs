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
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        foreach (var hit in raycastResults)
        {
            if (hit.gameObject.name == "FrontWheel")
            {

                //TODO
                //change front wheel
                //remove the current item from inventory
                //updated repairUI

                this.enabled = false;
            }
            else if (hit.gameObject.name == "BackWheel")
            {

                //TODO
                //change front wheel
                //remove the current item from inventory
                //updated repairUI

                this.enabled = false;
            }
        }
    }
    #endregion
}
