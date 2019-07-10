using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class ObjectScript : MonoBehaviour {

	public GameObject ProgressBar;
	public int maxhp;

	int hp;

    public ItemData Drop;
	//Количество выпадаемого объекта
	public int LootAmount;
    //Передается ссылка на игровой обьект персонажа

    //Тип рандомно выпадаемого объекта
    public ItemData RandomDrop;
	//Максимальное количество
	public int MaxLootAmount;

    public Transform entForSpawn;

	void Start()
	{
		hp = maxhp;
	}
	public void Hit(GameObject playerGO)
	{
		hp--;
		if (hp <= 0) 
		{
			playerGO.GetComponent<PlayerController>().inventoryController.AddToInventory (Drop, LootAmount);
			System.Random rand = new System.Random();
			int x = rand.Next (0, MaxLootAmount + 1);
			if (x > 0) 
			{
				playerGO.GetComponent<PlayerController>().inventoryController.AddToInventory (RandomDrop, x);
			}
			
			Destroy (gameObject);
			ProgressBar.SetActive (false);
			System.Random enemyRand = new System.Random();
                int entSpawnRand = rand.Next(0, 100);
                if (entSpawnRand>85)
                {
                    Instantiate(entForSpawn, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z),Quaternion.identity);
                }
		}
		else
		{
			ProgressBar.SetActive (true);
			ProgressBar.GetComponent<Slider>().value = ((float)hp/maxhp) * 100.0f;
			ColorBlock cb = ProgressBar.GetComponent<Slider> ().colors;
			cb.disabledColor = new Color (0.2f, ProgressBar.GetComponent<Slider> ().value / 100, 0, 1);
			ProgressBar.GetComponent<Slider> ().colors = cb;
		}
	}
}
