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
    public int itemCount;
    public List<ItemEffect> effects;

    public bool Use()
    {
        if (itemType == ItemType.Key)
        {
            for(int i=0; i<effects.Count; i++)
            {
                return effects[i].ExcuteRole();
            }
        }
        else
        {
            return false;
        }

        return false;
    }
}
