using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairingUI : Singleton<RepairingUI>
{
	[SerializeField]
	private Canvas _canvas = null;

	[SerializeField]
	private List<Image> _items = new List<Image>();
	public List<Image> Items { get { return _items; } }

	public void Display(bool show)
	{
		_canvas.gameObject.SetActive(show);

		if (show)
		{
			for (int i = 0; i < _items.Count; i++)
			{
				if (InventoryManager.Instance.Inventory.Count > i)
				{
					_items[i].sprite = InventoryManager.Instance.Inventory[i].Icon;
				}
				else
				{
					_items[i].sprite = null;
				}
			}
		}
	}
}
