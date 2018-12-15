using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boar : MonoBehaviour {

    public float speed;
    public float hp;
    private float bx;
    bool flagpos = true;
    private float by;
    float tm;
    // Use this for initialization
    void Start () {
        float bx = transform.position.x;
        float by = transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
        float x = transform.position.x;
        float y = transform.position.y;
        if (flagpos&(Mathf.Abs(bx - x) < 3 | Mathf.Abs(by - y) < 3)) { transform.Translate(Vector2.right * speed * Time.deltaTime); }
        else {
            if (flagpos) { tm = Time.realtimeSinceStartup; }


            float tm2 = Time.realtimeSinceStartup;
           
           
            if (Mathf.Abs(tm2-tm)> 7) { transform.Translate(Vector2.left * speed * Time.deltaTime); }
           

            flagpos = false;
            if (Mathf.Abs(bx - x) < 0.5| Mathf.Abs(by - y) < 0.5) { flagpos = true; }
        }
       
	}
}
