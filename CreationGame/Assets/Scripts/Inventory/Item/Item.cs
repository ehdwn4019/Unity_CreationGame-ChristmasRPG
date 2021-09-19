using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item" , menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Potion,
        Key,
    }

    public ItemType itemType;
    public string itemName;
    public Sprite itemImg;
    public GameObject itemPrefab;
}
