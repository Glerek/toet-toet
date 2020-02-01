using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairingUI : Singleton<RepairingUI>
{
    public List<GameObject> items;

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
