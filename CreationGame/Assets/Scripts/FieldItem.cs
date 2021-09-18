using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItem : MonoBehaviour
{
    [SerializeField]
    Item fieldItem;

    //[SerializeField]
    //SpriteRenderer fieldItemImg;

    public void SetItem(Item item)
    {
        fieldItem.itemType = item.itemType;
        fieldItem.itemName = item.itemName;
        fieldItem.itemImg = item.itemImg;
        fieldItem.itemCount = item.itemCount;
        fieldItem.effects = item.effects;

        //fieldItemImg.sprite = item.itemImg;
    }

    public Item GetItem()
    {
        return fieldItem;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
