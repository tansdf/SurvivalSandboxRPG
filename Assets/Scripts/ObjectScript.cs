using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour {


	public int hp;
	//Тип выпадаемого объекта
	public string TypeOfObject;
	//Количество выпадаемого объекта
	public int LootAmount;
	//Передается ссылка на игровой обьект персонажа
	public void Hit(GameObject playerGO)
	{
		hp--;
		if (hp <= 0) 
		{
			playerGO.GetComponent<InventoryScript> ().AddToInventory (TypeOfObject, LootAmount);
			Destroy (gameObject);
		}
	}
}
