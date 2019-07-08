using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour {


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
		
	private Item NullItem;
	public List<Item> InventoryList = new List<Item>();
	public Item[] SlotList = new Item[8];
	public List<GameObject> ItemBarSlots = new List<GameObject>();
	public GameObject ItemBar;
	public int SelectedIndex = -1;
	int LastSelected;
	public GameObject SelectedTextUI;

	void Start()
	{
		NullItem = new Item ("Null", 0);
		InitItemBar ();
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

			//SetItemSlot (InventoryList [indElement], indElement);
			AddToItemBar(InventoryList[indElement]);
		} else 
		{
			this.InventoryList.Add (new Item (_name, _amount));
			int indSlot = FindFreeSlot ();
			if (InventoryList.Count <= 8 && indSlot < 8)
			{// Нужно переделать условие, но оно будет работать пока всего в игре не больше восьми предметов
				
				//SetItemSlot (InventoryList.FindLast (x => x.name != null), indSlot);
				AddToItemBar(new Item(_name, _amount));
			}
		}
	}

	public void DeleteFromInventory(string _name, int _amount)
	{
		if (InventoryList.Find (x => x.name == _name).amount <= _amount) {
			//SetItemSlot (new Item ("Null", 0), InventoryList.FindIndex (x => x.name == _name));
			DeleteFromItemBar(new Item(_name, _amount));
			InventoryList.RemoveAll (x => x.name == _name);
		} else if (InventoryList.Find (x => x.name == _name).amount > _amount) 
		{
			int indElement = InventoryList.FindIndex(x => x.name == _name);
			InventoryList[indElement] = new Item (_name, InventoryList[indElement].amount - _amount);
			DeleteFromItemBar(new Item(_name, _amount));
			//SetItemSlot (InventoryList [indElement], indElement);
		}
	}

	private void SetItemSlot(Item item, int index)
	{
		ItemBarSlots [index].GetComponentsInChildren<Image> ()[1].sprite = ItemBar.GetComponent<GUIItemBar> ().getSprite (item.name);
		if(item.amount != 0)ItemBarSlots [index].GetComponentsInChildren<Text> () [0].text = item.amount.ToString ();
		else ItemBarSlots [index].GetComponentsInChildren<Text> () [0].text = "";
		SlotList [index] = item;
	}

	private void InitItemBar()
	{
		for (int i = 0; i < ItemBarSlots.Count; i++) 
		{
			SlotList[i] = NullItem;
		}
	}

	private void AddToItemBar(Item item)
	{
		bool ItemInBar = false;
		int slotInd = 0;;
		for (int i = 0; i < SlotList.Length; i++) 
		{
			if (SlotList [i].name.Equals(item.name)) 
			{
				ItemInBar = true;
				slotInd = i;
			}
		}
		if (ItemInBar) {
			SetItemSlot (item, slotInd);
		} else
		{
			slotInd = FindFreeSlot ();
			if (slotInd < SlotList.Length) {
				SetItemSlot (item, slotInd);
			}
		}
	}

	private void DeleteFromItemBar(Item item)
	{
		int slotInd = 0;
		for(int i = 0; i < SlotList.Length; i++)
		{
			if (SlotList [i].name.Equals (item.name)) 
			{
				slotInd = i;
			}
		}
		if (SlotList [slotInd].amount <= item.amount) 
		{
			SetItemSlot (NullItem, slotInd);
		}
		else
		{
			SetItemSlot (new Item (item.name, SlotList[slotInd].amount - item.amount), slotInd);
		}
	}

	//Метод для нахождения индекса свободного слота
	private int FindFreeSlot()
	{
		int i = 0;
		while (SlotList[i].name != "Null") 
		{
			i++;
		}
		return i;
	}

	private int FindExistSlot(Item item)
	{
		return 0;
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
			if(InventoryList.Count > SelectedIndex) SelectedTextUI.GetComponent<Text>().text = SlotList [SelectedIndex].name;
			ItemBarSlots [LastSelected].transform.localScale = new Vector2 (1.0f, 1.0f);
			LastSelected = SelectedIndex;
		}else
			ItemBarSlots [LastSelected].transform.localScale = new Vector2 (1.0f, 1.0f);
	}
		
}
