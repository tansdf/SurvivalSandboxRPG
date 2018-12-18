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
	public int SelectedIndex = -1;
	int LastSelected;
	public GameObject SelectedTextUI;

	void Start()
	{
		//SetItemSlot (new Item ("Stone", 12), 1);
	}

	void Update()
	{
		if (Input.GetAxis ("Mouse ScrollWheel") != 0)
			SetSelectedItem (Input.GetAxis ("Mouse ScrollWheel"));
	}

	public void AddToInventory(string _name, int _amount)
	{
		//Если в инвентаре уже есть этот обьект, то увеличиваем количество
		//иначе просто добавлям
		//Debug.Log(_name + "  " + _amount);
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

	public void DeleteFromInventory(string _name, int _amount)
	{
		if (InventoryList.Find (x => x.name == _name).amount <= _amount) {
			SetItemSlot (new Item ("Null", 0), InventoryList.FindIndex (x => x.name == _name));
			InventoryList.RemoveAll (x => x.name == _name);
		} else if (InventoryList.Find (x => x.name == _name).amount > _amount) 
		{
			int indElement = InventoryList.FindIndex(x => x.name == _name);
			InventoryList[indElement] = new Item (_name, InventoryList[indElement].amount - _amount);
			SetItemSlot (InventoryList [indElement], indElement);
		}
	}

	private void SetItemSlot(Item item, int index)
	{
		ItemBarSlots [index].GetComponentsInChildren<Image> ()[1].sprite = ItemBar.GetComponent<GUIItemBar> ().getSprite (item.name);
		if(item.amount != 0)ItemBarSlots [index].GetComponentsInChildren<Text> () [0].text = item.amount.ToString ();
		else ItemBarSlots [index].GetComponentsInChildren<Text> () [0].text = "";
	}

	private void SetSelectedItem(float scrollDir)
	{
		if (scrollDir < 0)
			SelectedIndex++;
		else if (scrollDir > 0)
			SelectedIndex--;
		if (SelectedIndex < -1)
			SelectedIndex = 7;
		else if (SelectedIndex > 7)
			SelectedIndex = -1;
		if (SelectedIndex != -1) 
		{
			ItemBarSlots [SelectedIndex].transform.localScale = new Vector2 (1.15f, 1.15f);
			if(InventoryList.Count > SelectedIndex) SelectedTextUI.GetComponent<Text>().text = InventoryList [SelectedIndex].name;
			ItemBarSlots [LastSelected].transform.localScale = new Vector2 (1.0f, 1.0f);
			LastSelected = SelectedIndex;
		}else
			ItemBarSlots [LastSelected].transform.localScale = new Vector2 (1.0f, 1.0f);
	}
		
}
