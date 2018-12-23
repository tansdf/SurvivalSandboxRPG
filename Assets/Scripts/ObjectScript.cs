using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;

public class ObjectScript : MonoBehaviour {


	public int maxhp;

	public int hp;
	//Тип выпадаемого объекта
	public string TypeOfObject;
	//Количество выпадаемого объекта
	public int LootAmount;
	//Передается ссылка на игровой обьект персонажа

	//Тип рандомно выпадаемого объекта
	public string TypeOfRandomObject;
	//Максимальное количество
	public int MaxLootAmount;

	//Позже, если прям так уж необходимо будет, надо переписать это как-нибудь 
	//Но пока так сойдет

	public void Hit(GameObject playerGO)
	{
		hp--;
		if (hp <= 0) 
		{
			playerGO.GetComponent<InventoryScript> ().AddToInventory (TypeOfObject, LootAmount);
			System.Random rand = new System.Random();
			int x = rand.Next (0, MaxLootAmount + 1);
			if (x > 0) 
			{
				playerGO.GetComponent<InventoryScript> ().AddToInventory (TypeOfRandomObject, x);
			}
			Destroy (gameObject);
		}
	}
}
