using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DragHandle : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public static GameObject item;
    Vector3 startPosition;
    Transform startParent;



    #region IBeginDragHandler implementation
    public void OnBeginDrag(PointerEventData eventData)
    {
        item = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;

    }

    #endregion



    #region IDragHandler implementation
    public void OnDrag(PointerEventData eventData)
    {
        transform.position =
                 new Vector3(Input.mousePosition.x,
                 Input.mousePosition.y,
                 Camera.main.nearClipPlane);
    }
    #endregion



    #region IEndDraghandler implementation
    public void OnEndDrag(PointerEventData eventData)
    {
        item = null;

        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
        GetComponent<CanvasGroup>().blocksRaycasts = true;

    }
    #endregion


}
