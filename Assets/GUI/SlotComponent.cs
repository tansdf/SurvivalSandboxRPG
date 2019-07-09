using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotComponent : MonoBehaviour, IPointerClickHandler
{

    //ScriptableObject - Id, Имя, Иконка, Максимальный стак
	[SerializeField]
	private ItemData itemInSlot;
    //Прозрачный спрайт
	[SerializeField]
	public Sprite uimask;
	public int itemCount; 

	public ItemData getItemSlot()
	{
		return itemInSlot;
	}

    public void OnPointerClick(PointerEventData pointerEventData)
    {
		var inventoryController = GameObject.Find("Inventory").GetComponent<InventoryController>();
		if(pointerEventData.button.ToString().CompareTo("Left") == 0)
		{
			if(itemInSlot != null)
			{
				if(inventoryController.itemInMouse == null)
				{
					inventoryController.putItemInMouse(itemInSlot, itemCount);
					clearItemSlot();
				}
				else if(inventoryController.itemInMouse == itemInSlot)
				{
					if(inventoryController.itemCountInMouse + itemCount <= itemInSlot.maxStackSize)
					{
						setItemCount(inventoryController.itemCountInMouse + itemCount);
						inventoryController.clearItemInMouse();
					}
					else
					{
						inventoryController.putItemInMouse(itemInSlot, inventoryController.itemCountInMouse - (itemInSlot.maxStackSize - itemCount));
						setItemCount(itemInSlot.maxStackSize);
					}
				}
			}
			else 
			{
				if(inventoryController.itemInMouse != null)
				{
					setItemSlot(inventoryController.itemInMouse, inventoryController.itemCountInMouse);
					inventoryController.clearItemInMouse();
				}
			}
		}
		else if(pointerEventData.button.ToString().CompareTo("Right") == 0)
		{
			if(itemInSlot != null)
			{
				if(inventoryController.itemInMouse == null)
				{
					inventoryController.putItemInMouse(itemInSlot, 1);
					setItemCount(itemCount - 1);
				}
				else if(inventoryController.itemInMouse == itemInSlot && inventoryController.itemCountInMouse <= itemInSlot.maxStackSize)
				{
					inventoryController.putItemInMouse(itemInSlot, inventoryController.itemCountInMouse + 1);
					setItemCount(itemCount - 1);
				}
			}
			else 
			{
				if(inventoryController.itemInMouse != null)
				{
					setItemSlot(inventoryController.itemInMouse, 1);
					if(inventoryController.itemCountInMouse > 1) inventoryController.putItemInMouse(inventoryController.itemInMouse, inventoryController.itemCountInMouse - 1);
					else inventoryController.clearItemInMouse();
				}
			}
		}
    }
    
	public void setItemSlot(ItemData item, int count)
	{
		itemInSlot = item;
		gameObject.GetComponentsInChildren<Image> ()[1].sprite = item.icon;
		itemCount = count;
		gameObject.GetComponentInChildren<Text> ().text = count.ToString ();
	}

	public void setItemCount(int count)
	{
		itemCount = count;
		gameObject.GetComponentInChildren<Text> ().text = count.ToString ();
		if(count == 0) clearItemSlot();
	}

	public void clearItemSlot()
	{
		gameObject.GetComponentsInChildren<Image> () [1].sprite = uimask;
		gameObject.GetComponentInChildren<Text> ().text = "";
		itemInSlot = null;
		itemCount = 0;
	}
}
