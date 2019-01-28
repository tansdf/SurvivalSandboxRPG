//Скрипт управления персонажем.
//Помимо движения здесь будут написаны другие механики взаимодействия персонажа

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 1.0f;
	public float hp = 100.0f;
	//Сытость
	public float satiety = 100.0f;

    public Text respectText;
	public GameObject HPBar;
	public GameObject HungerBar;
	public GameObject ProgressBar;
	public Camera camera;

	private GameObject MinedObjectRef;
	private Rigidbody2D rb2D;

	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		camera = GetComponentsInChildren<Camera> ()[0];
		ProgressBar.SetActive (false);
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
		if (satiety >= 0) 
		{
			satiety = satiety - Time.deltaTime;
			HungerBar.GetComponent<Slider> ().value = satiety / 100;
		}
		if (hp >= 0.0f) 
			HPBar.GetComponent<Slider> ().value = hp / 100;
		if (satiety <= 0)
			Hit (1);
		if (Input.GetKeyUp ("e"))
			Use();
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
		Vector3 mP = camera.ScreenToWorldPoint(Input.mousePosition);
		Vector3 goP = GO.transform.position;
		float xDelta, yDelta, colR;
		try{
			Vector2 colV = GO.GetComponent<CircleCollider2D>().offset;
			colR = GO.GetComponent<CircleCollider2D>().radius;
			xDelta = Mathf.Abs(mP.x - goP.x - colV.x);
			yDelta = Mathf.Abs(mP.y - goP.y - colV.y);
		}catch(MissingComponentException ex) 
		{
			Debug.Log (ex.Message);
			xDelta = Mathf.Abs (mP.x - goP.x);
			yDelta = Mathf.Abs (mP.x - goP.x);
			colR = 0.2f;
		}
		Debug.Log (xDelta + "    " + yDelta + "    " + colR);
		if(xDelta <= colR && yDelta <= colR && Input.GetMouseButton(1))
		{
			ProgressBar.SetActive (true);
			GO.GetComponent<ObjectScript> ().Hit (gameObject);
			ProgressBar.GetComponent<Slider> ().value = 100.0f - ((float)GO.GetComponent<ObjectScript> ().hp / GO.GetComponent<ObjectScript> ().maxhp) * 100.0f;
			if (GO == null)
				ProgressBar.GetComponent<Slider> ().value = 0.0f;
			ColorBlock cb = ProgressBar.GetComponent<Slider> ().colors;
			cb.disabledColor = new Color (0.2f, ProgressBar.GetComponent<Slider> ().value / 100, 0, 1);
			ProgressBar.GetComponent<Slider> ().colors = cb;
			if (ProgressBar.GetComponent<Slider> ().value == 100.0f)
				ProgressBar.SetActive (false);
		} else 
		{
			ProgressBar.SetActive (false);
		}
	}

	//Пинок
	void Hit(int damage)
	{
		if(hp >= 0)
			hp = hp - Time.deltaTime * 5;
		/*if(hp <= 0)
		{
		//Он помирает	
		}*/
	}
	void Use()
	{
		InventoryScript InvScript = this.GetComponent<InventoryScript> ();
		int selind =InvScript.SelectedIndex;
		//Debug.Log (selind + "  " + InvScript.InventoryList.Count);
		if (selind != -1 && InvScript.InventoryList.Count > selind) 
		{
			if (InvScript.SlotList [selind].name == "Apple") 
			{
				satiety += 50.0f;
				if (satiety > 100)
					satiety = 100.0f;
				InvScript.DeleteFromInventory ("Apple", 1);
			}
		}
	}
}
