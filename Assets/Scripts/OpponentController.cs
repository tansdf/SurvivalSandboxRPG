//Скрипт управляющий врагами
//Скорее всего не понадобится в будущем, он сделан для того чтобы тестировать различные механики атаки персонажа
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    
	public float MaxHp;
	public float Hp;
	public string MobName;


    void Start()
    {
		Hp = MaxHp;
    }

	public void Hit(float damage)
	{
		if(Hp > 0)
		{
			Hp = Hp - damage;
			StartCoroutine (DamageCoroutine());
		}
		if (Hp <= 0)
			Death ();
	}

	void Death()
	{
		Debug.Log ("Манекен умер(");
		Destroy (gameObject);
	}

	IEnumerator DamageCoroutine()
	{
		float gbColor = 0.0f;
		while (gbColor <= 1.0f) 
		{
			Color col = gameObject.GetComponent<SpriteRenderer> ().color;
			col.g = gbColor;
			col.b = gbColor;
			gameObject.GetComponent<SpriteRenderer> ().color = col;
			gbColor += 0.1f;
			Debug.Log (gbColor);
			yield return null;
		}
	}
}
