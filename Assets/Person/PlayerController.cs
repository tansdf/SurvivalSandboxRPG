//Скрипт управления персонажем.
//Помимо движения здесь будут написаны другие механики взаимодействия персонажа

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

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
	public VideoPlayer video;
	bool isDead = false;



    void Start () {
		rb2D = GetComponent<Rigidbody2D>();
		camera = GetComponentsInChildren<Camera> ()[0];
		ProgressBar.SetActive (false);
        respectText.text = "";
		anim = gameObject.GetComponent<Animator> ();
		video.Play();
		video.Pause();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(!isDead)
		{
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

			if(hp==0)
			{
				foreach(var collider in Physics2D.OverlapCircleAll(transform.position, 0.5f, LayerMask.GetMask("Damage")))
					Debug.Log(collider);
				
				if(Physics2D.OverlapCircleAll(transform.position, 0.5f, LayerMask.GetMask("Damage")).Length > 0)
				{
					anim.SetTrigger("DeadLava");
				}
				else
				{
					anim.SetTrigger("DeadHunger");
				}
				isDead = true;
			}
		}
	}

	void Update()
	{
		if(!isDead)
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
				if (inventoryController.isOpen)
				{
					Debug.Log("Close");
					inventoryController.HideInventory();
				} 
				else 
				{
					Debug.Log("Open");
					inventoryController.OpenInventory();
				}
			}

			gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)((gameObject.transform.position.y-gameObject.GetComponent<SpriteRenderer>().bounds.size.y/2)*-100);

			if(Input.GetAxis("Mouse ScrollWheel") != 0)
			{
				inventoryController.SetSelectedSlot(Input.GetAxis("Mouse ScrollWheel"));
			}
		}

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
		SlotComponent currentSlot = inventoryController.getSelectedSlot();
		//Нужно будет поменять, когда в ItemData появиться как нибудь параметр на съедобность
		if(currentSlot.getItemSlot() != null && currentSlot.getItemSlot().id == 0)
		{
			currentSlot.setItemCount(currentSlot.itemCount - 1);
			satiety += 30;
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

	private IEnumerator WaitVideo()
    {
        yield return new WaitForSecondsRealtime(5.0f);
		video.Stop();
		SceneManager.LoadScene("MenuScene");
    }

	void Dead()
	{
		video.Play();
		StartCoroutine("WaitVideo");
	}
}
