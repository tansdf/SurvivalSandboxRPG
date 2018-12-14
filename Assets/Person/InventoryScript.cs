using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {

	//Очень очень базовый вариант инвентаря, позже скорее всего будет изменен
	//Пока что идинтификация элементов по строке, позже быть может сделать по ID
	public struct Item
	{
		public int amount { get; set;}
		public string name { get; set;}

		public Item(string n, int a)
		{
			amount = a;
			name = n;
		}
	}
		
	public List<Item> InventoryList = new List<Item>();
	public List<GameObject> ItemBarSlots = new List<GameObject>();
	public GameObject ItemBar;

	void Start()
	{
		//SetItemSlot (new Item ("Stone", 12), 1);
	}

	public void AddToInventory(string _name, int _amount)
	{
		//Если в инвентаре уже есть этот обьект, то увеличиваем количество
		//иначе просто добавлям
		if (InventoryList.Exists (x => x.name == _name)) 
		{
			//костыль
			//Так как элементы списка нельзя изменять, их можно только заменять или считывать
			int indElement = InventoryList.IndexOf(InventoryList.Find (x => x.name == _name));
			InventoryList[indElement] = new Item (_name, InventoryList[indElement].amount + _amount);
			SetItemSlot (InventoryList [indElement], indElement);
		} else 
		{
			this.InventoryList.Add (new Item (_name, _amount));
			if(InventoryList.Count <= 8)
				SetItemSlot (InventoryList.FindLast(x => x.name != null), InventoryList.FindLastIndex(x => x.name != null));
		}
	}

	public void SetItemSlot(Item item, int index)
	{
		ItemBarSlots [index].GetComponent<Image> ().sprite = ItemBar.GetComponent<GUIItemBar> ().getSprite (item.name);
		ItemBarSlots [index].GetComponentsInChildren<Text> () [0].text = item.amount.ToString ();
	}
}
