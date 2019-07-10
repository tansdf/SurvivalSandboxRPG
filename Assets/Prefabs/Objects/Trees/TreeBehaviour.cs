using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TreeBehaviour : MonoBehaviour
{

    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;

    private SpriteRenderer spRender;

#if UNITY_EDITOR
    void SpriteSelect()
    {
        spRender = gameObject.GetComponent<SpriteRenderer>();
        System.Random rd = new System.Random();
        int num = rd.Next(1, 4);
        switch (num)
        {
            case 1:
                spRender.sprite = sprite1;
                break;
            case 2:
                spRender.sprite = sprite2;
                break;
            case 3:
                spRender.sprite = sprite3;
                break;
            case 4:
                spRender.sprite = sprite4;
                break;
            default:
                spRender.sprite = sprite4;
                break;
        }
    }
#endif

    // Start is called before the first frame update
    void Start()
    {
        //spRender = gameObject.GetComponent<SpriteRenderer>();
        //System.Random rd = new System.Random();
        //int num = rd.Next(1, 4);
        //switch (num)
        //{
        //    case 1:
        //        spRender.sprite = sprite1;
        //        break;
        //    case 2:
        //        spRender.sprite = sprite2;
        //        break;
        //    case 3:
        //        spRender.sprite = sprite3;
        //        break;
        //    case 4:
        //        spRender.sprite = sprite4;
        //        break;
        //    default:
        //        spRender.sprite = sprite4;
        //        break;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
