using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour {

	public int hp;
	public string TypeOfObject;

	public void Hit()
	{
		hp--;
		if (hp <= 0)
			Destroy (gameObject);
	}
}
