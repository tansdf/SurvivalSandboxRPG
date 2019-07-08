using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "Item Data", order = 51)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    public int id;
    [SerializeField]
    public string itemName;
    [SerializeField]
    public Sprite icon;
    [SerializeField]
    public int maxStackSize;
}
