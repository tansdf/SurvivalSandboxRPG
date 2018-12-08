using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		} else 
		{
			this.InventoryList.Add (new Item (_name, _amount));
		}
		Debug.Log (InventoryList [0].amount + "   " + InventoryList [0].name);
	}
}
