﻿//Скрипт управления персонажем.
//Помимо движения здесь будут написаны другие механики взаимодействия персонажа

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 1.0f;

	private Rigidbody2D rb2D;

	void Start () {
		rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float xMove = Input.GetAxis("Horizontal");
		float yMove = Input.GetAxis("Vertical");
		transform.Translate(xMove * speed * Time.deltaTime, yMove * speed * Time.deltaTime, 0);
		//rb2D.AddForce(new Vector2(xMove, yMove) * speed * Time.deltaTime);
	}
}
