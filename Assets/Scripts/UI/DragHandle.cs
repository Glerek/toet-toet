﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandle : MonoBehaviour, IDragHandler, IEndDragHandler
{
	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("aa");
	}

	public void OnDrag(PointerEventData eventData)
	{
		Debug.Log("aasd");
		this.transform.localPosition = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.localPosition = Vector2.zero;
	}
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
