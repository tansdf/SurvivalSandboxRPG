using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
	public int count;

	public void addItem(ItemData item)
	{
		//gameObject.GetComponent<InventoryController>().AddToInventory(item, count);
	}

	public void delItem(ItemData item)
	{
		//gameObject.GetComponent<InventoryController>().DeleteFromInventory(item, count);
	}
}
