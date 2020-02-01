using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandle : MonoBehaviour
{
	void OnDrop(PointerEventData eventData)
	{
		RectTransform invPanel = transform as RectTransform;

		if (!RectTransformUtility.RectangleContainsScreenPoint(invPanel, Input.mousePosition))
		{
			Debug.Log("aa");
		}
	}
}
