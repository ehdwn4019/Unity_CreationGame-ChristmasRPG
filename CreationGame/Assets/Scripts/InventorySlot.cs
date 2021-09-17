using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public Image iconImg;

    public void UpdateSlotUI()
    {
        iconImg.sprite = item.itemImg;
        iconImg.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        iconImg.gameObject.SetActive(false);
    }
}
