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
	private Sprite uimask;
	public int itemCount; 

	public ItemData getItemSlot()
	{
		return itemInSlot;
	}

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(pointerEventData.pointerId + " Нажатие");
        Debug.Log(pointerEventData.button);
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
	}

	public void clearItemSlot()
	{
		gameObject.GetComponentsInChildren<Image> () [1].sprite = uimask;
		gameObject.GetComponentInChildren<Text> ().text = "";
		itemInSlot = null;
		itemCount = 0;
	}
}
