using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator animator;
    public float hp;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)((gameObject.transform.position.y-gameObject.GetComponent<SpriteRenderer>().bounds.size.y/2)*-100);
    }

    void Die()
    {
        animator.SetTrigger("Die");
    }

    void Disappearing()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine("InvisibleSprite");
    }

    private IEnumerator InvisibleSprite()
    {
        for (float f = 1f; f >= 0; f -= 0.2f) 
        {
            yield return new WaitForSecondsRealtime(0.1f);
            Color c = spriteRenderer.color;
            c.a = f;
            spriteRenderer.color = c;
        }
        GameObject.Destroy(gameObject);
    }

    private IEnumerator RedSprite()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSecondsRealtime(0.5f);
        spriteRenderer.color = Color.white;
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
            Die();
        }
        StartCoroutine("RedSprite");
    }
}
