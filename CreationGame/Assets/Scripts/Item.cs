using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public enum ItemType
    {
        Potion,
        Key,
    }

    public ItemType itemType;
    public string itemName;
    public Sprite itemImg;

    public bool Use()
    {
        return false;
    }
}
