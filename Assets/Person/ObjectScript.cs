using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour {

	public int hp;
	public string TypeOfObject;

	//Передается ссылка на игровой обьект персонажа
	public void Hit(GameObject playerGO)
	{
		hp--;
		if (hp <= 0) 
		{
			playerGO.GetComponent<InventoryScript> ().AddToInventory ("stone", 5);
			Destroy (gameObject);
		}
	}
}
