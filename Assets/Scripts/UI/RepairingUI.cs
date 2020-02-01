using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairingUI : Singleton<RepairingUI>
{
	[SerializeField]
	private Canvas _canvas = null;

    public List<GameObject> items;

    private void Start()
    {
        int count = 0; //the index of the items

        //Get all items from the InventoryManager and do different things depends on the item type
        foreach (var item in InventoryManager.Instance.Inventory)
        {
            //Change Icon, may need to fix the size
            items[count].GetComponent<SpriteRenderer>().sprite = item.Icon;
            if (item is Wheel)
            {
            }
            else if (item is CarLight)
            {
            }
            count++;
        }
    }

	public void Display(bool show)
	{
		_canvas.gameObject.SetActive(show);
	}
}
