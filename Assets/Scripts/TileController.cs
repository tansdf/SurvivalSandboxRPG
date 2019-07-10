using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TileController : MonoBehaviour
{
    public GameObject ProgressBar;
	public int maxhp;

    public ItemData Drop;
	//Количество выпадаемого объекта
	public int LootAmount;
    //Передается ссылка на игровой обьект персонажа

    //Тип рандомно выпадаемого объекта
    public ItemData RandomDrop;
	//Максимальное количество
	public int MaxLootAmount;
    Tilemap tilemap;

    Dictionary<Vector3Int, float> tileHp = new  Dictionary<Vector3Int, float>();

	void Start()
	{
        tilemap = GetComponent<Tilemap>();

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        foreach(var pos in tilemap.cellBounds.allPositionsWithin)
        {
            tileHp.Add(pos, maxhp);
        }
	}


    public void Hit(GameObject playerGO)
	{
        Vector2 mP = playerGO.GetComponentsInChildren<Camera>()[0].ScreenToWorldPoint(Input.mousePosition);
        Vector3Int pPos = tilemap.WorldToCell(mP);
		tileHp[pPos]--;
		if (tileHp[pPos] <= 0) 
		{
			playerGO.GetComponent<PlayerController>().inventoryController.AddToInventory (Drop, LootAmount);
			System.Random rand = new System.Random();
			int x = rand.Next (0, MaxLootAmount + 1);
			if (x > 0) 
			{
				playerGO.GetComponent<PlayerController>().inventoryController.AddToInventory (RandomDrop, x);
			}
			tilemap.SetTile(pPos, null);
			ProgressBar.SetActive (false);
		}
		else
		{
			ProgressBar.SetActive (true);
			ProgressBar.GetComponent<Slider>().value = ((float)tileHp[pPos]/maxhp) * 100.0f;
			ColorBlock cb = ProgressBar.GetComponent<Slider> ().colors;
			cb.disabledColor = new Color (0.2f, ProgressBar.GetComponent<Slider> ().value / 100, 0, 1);
			ProgressBar.GetComponent<Slider> ().colors = cb;
		}
	}
}
