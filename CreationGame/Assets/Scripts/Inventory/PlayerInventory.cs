using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    GameObject inventory;

    [SerializeField]
    GameObject slotsParent;

    bool activeInventory;
    bool isTouchInventoryBtn;
    int slotCount = 4;
    int index;

    Action slotCountChange;
    InventorySlot[] slots;
    Player player;

    //[SerializeField]
    //List<Item> items;

    //[SerializeField]
    //List<ItemPickUp> test;

    // Start is called before the first frame update
    void Start()
    {
        slots = slotsParent.GetComponentsInChildren<InventorySlot>();
        inventory.SetActive(false);
        activeInventory = false;
        slotCountChange += SlotChange;
        slotCountChange.Invoke();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        InventoryOnOff();
        //FindPotion();

        //Debug.Log("player" + player.isTouchPotionBtn);
        //if (slot == null)
        //    Debug.Log("아 ㅋㅋ");
    }

    #region 인벤토리 UI
    public void TouchInveontory()
    {
        isTouchInventoryBtn = true;
    }

    void InventoryOnOff()
    {
        if(Input.GetKeyDown(KeyCode.I) || isTouchInventoryBtn)
        {
            activeInventory = !activeInventory;
            inventory.SetActive(activeInventory);
        }

        isTouchInventoryBtn = false;
    }

    #endregion

    #region 인벤토리 슬롯

    public void SlotChange()
    {
        for(int i=0; i<slots.Length; i++)
        {
            slots[i].index = i;
            //slots[i].i

            if(i<slotCount)
            {
                slots[i].gameObject.SetActive(true);
            }
            else
            {
                slots[i].gameObject.SetActive(false);
            }
        }
    }
    
    public void AddSlot()
    {
        slotCount += 4;
        slotCountChange.Invoke();
    }

    #endregion

    #region 인벤토리 아이템
    public void AcquireItem(Item item, int count = 1)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == item.itemName)
                {
                    slots[i].SetSlotCount(count);
                    //items[i].itemCount++;
                    return;
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(item, count);
                //items.Add(item);
                
                return;
            }
        }
    }

    //public void FindPotion()
    //{
    //    if (!player.IsTouchPotionBtn)
    //        return;
    //
    //    for(int i=0; i<slots.Length; i++)
    //    {
    //        if(slots[i].item != null)
    //        {
    //            if(slots[i].item.itemType == Item.ItemType.Potion)
    //            {
    //                slot = slots[i];
    //                return;
    //            }
    //        }
    //    }
    //}
    //
    //public InventorySlot ReturnSlot()
    //{
    //    return slot;
    //}

    //public InventorySlot ReturnSlot()
    //{
    //    return slots[index];
    //}

    //public void GetSlotIndex(int index)
    //{
    //    this.index = index;
    //}

    #endregion
}
