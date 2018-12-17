using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIItemBar : MonoBehaviour {

	//Скрипт для того чтобы проще делать соответствия между названием предмета и его текстурой

	public Sprite NullSprite;
	public Sprite StoneSprite;
	public Sprite WoodSprite;
	public Sprite AppleSprite;

	public Sprite getSprite(string name)
	{
		switch (name) 
		{
		case("Stone"):
			return StoneSprite;
		case("Wood"):
			return WoodSprite;
		case("Apple"):
			return AppleSprite;
		case("Null"):
			return NullSprite;
		default:
			return null;
		}
	}
}
