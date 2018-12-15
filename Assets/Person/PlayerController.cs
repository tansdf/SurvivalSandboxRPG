//Скрипт управления персонажем.
//Помимо движения здесь будут написаны другие механики взаимодействия персонажа

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 1.0f;
	public int hp = 100;
    public Text respectText;

	private Rigidbody2D rb2D;

	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
        respectText.text = "";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float xMove = Input.GetAxis("Horizontal");
		float yMove = Input.GetAxis("Vertical");
		transform.Translate(xMove * speed * Time.deltaTime, yMove * speed * Time.deltaTime, 0);
		//rb2D.AddForce(new Vector2(xMove, yMove) * speed * Time.deltaTime);
	}

	void Update()
	{
		
	}

	//Когда в триггер персонажа (область рядом с ним) попадает какой то объект
	void OnTriggerStay2D(Collider2D coll)
	{
		//Проверка, какой объект
		if (coll.gameObject.tag.Equals ("MinedObjects")) 
		{
			Harvest (coll.gameObject);
		}

        if (coll.gameObject.tag.Equals("GraveForRespect"))
        {
            if (Input.GetKey("f"))
            {
                respectText.text = "You paid Respect";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("GraveForRespect"))
        {
              respectText.text = "";            
        }
    }

    //Метод добычи. 
    void Harvest(GameObject GO)
	{
		if (Input.GetKey("space")) 
		{
			GO.GetComponent<ObjectScript> ().Hit(gameObject);
		}
	}    
}
