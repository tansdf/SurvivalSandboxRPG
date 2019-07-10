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
	public Animator anim;
    public InventoryController inventoryController;
	private GameObject MinedObjectRef;
	private Rigidbody2D rb2D;

    public Transform swipeParticle;
    public Transform parentForAttackAnim;


    void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		camera = GetComponentsInChildren<Camera> ()[0];
		ProgressBar.SetActive (false);
        respectText.text = "";
		anim = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		//Debug.Log (xMove + "    " + yMove);
		if(moveDirection.magnitude != 0)
		{
			anim.SetFloat ("xMove", moveDirection.x);
			anim.SetFloat ("yMove", moveDirection.y);
			anim.SetBool("isRunning", true);
		}
		else
		{
			anim.SetBool("isRunning", false);
		}

		transform.position += (Vector3)moveDirection.normalized*Time.deltaTime*speed;
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
			ApplyDamage(Time.deltaTime * 5);
		if (Input.GetKeyUp ("e"))
			Use();
        if (Input.GetMouseButtonDown(0))
        {
            DrawAttack();
			Attack();
        }

        if(Input.GetKeyDown("i"))
        {
            if (inventoryController.gameObject.activeInHierarchy) inventoryController.gameObject.SetActive(false);
            else inventoryController.gameObject.SetActive(true);
        }

		gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)((gameObject.transform.position.y-gameObject.GetComponent<SpriteRenderer>().bounds.size.y/2)*-100);


    }

	//Когда в триггер персонажа (область рядом с ним) попадает какой то объект
	void OnTriggerStay2D(Collider2D coll)
	{
		//Проверка, какой объект

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

	void Attack()
	{
		Vector2 mP = camera.ScreenToWorldPoint(Input.mousePosition);
 		RaycastHit2D[] hits;
		ProgressBar.SetActive (false);
		
		if (Vector2.Distance(mP, (Vector2)transform.position) < 1) 
		{
			hits = Physics2D.RaycastAll(new Vector3(mP.x, mP.y, 1), new Vector3(0, 0, -1));
			foreach(var hit in hits)
			{
				Debug.Log(hit.transform.gameObject.name);
				if(hit.transform.gameObject!=gameObject)
				{
					hit.transform.gameObject.SendMessage("ApplyDamage", 30.0f, SendMessageOptions.DontRequireReceiver);
					hit.transform.gameObject.SendMessage("Hit", gameObject, SendMessageOptions.DontRequireReceiver);
				}
			}
		
		}   
    }

    void DrawAttack()
    {
        Vector3 mP = camera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mPonScreen = Input.mousePosition;

        Vector3 playerPosition = gameObject.transform.position;
        Vector3 playerOnScreenPos = camera.WorldToScreenPoint(gameObject.transform.position);
        mPonScreen.x = mPonScreen.x - playerOnScreenPos.x;
        mPonScreen.y = mPonScreen.y - playerOnScreenPos.y;

        var heading = - playerOnScreenPos + mPonScreen;
        var distance = heading.magnitude;
        var direction = heading / distance;
        var k = 0;

        Instantiate(swipeParticle, new Vector3(gameObject.transform.position.x+(k*direction.x), gameObject.transform.position.y+(k*direction.y), gameObject.transform.position.z), Quaternion.FromToRotation(playerOnScreenPos,mPonScreen), parentForAttackAnim);
    }

	void ApplyDamage(float damage)
	{
		if(hp-damage>0)
		{
			hp-=damage;
		}
		else
		{
			hp=0;
		}
	}
}
