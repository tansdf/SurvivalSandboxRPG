using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class OrderChangerScript : MonoBehaviour
{
    public string searchByTag;
    public void OrderChange()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(searchByTag);

        foreach(var gObj in gameObjects)
        {
            gObj.GetComponent<SpriteRenderer>().sortingOrder = (int)((gObj.transform.position.y-gObj.GetComponent<SpriteRenderer>().bounds.size.y/2)*-100);
            //gObj.GetComponent<SpriteRenderer>().bounds.min;
        }
    }
}

[CustomEditor(typeof(OrderChangerScript))]
public class OrderChangerEditor : Editor
{
    /// <summary>Calls on drawing the GUI for the inspector.</summary>
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        OrderChangerScript prefabSwitch = (OrderChangerScript)target;


        if(GUILayout.Button("Swap By Tag"))
        {
            prefabSwitch.OrderChange();
        }
    }
}
