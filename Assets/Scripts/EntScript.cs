using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntScript : MonoBehaviour
{
    float xMove;
    float yMove;
    public Animator anim;



    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 60);
        anim = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
        //Debug.Log (xMove + "    " + yMove);
        //transform.Translate(xMove * speed * Time.deltaTime, yMove * speed * Time.deltaTime, 0);

        //rb2D.AddForce(new Vector2(xMove, yMove) * speed * Time.deltaTime);
        anim.SetFloat("xMove", xMove);
        anim.SetFloat("yMove", yMove);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
