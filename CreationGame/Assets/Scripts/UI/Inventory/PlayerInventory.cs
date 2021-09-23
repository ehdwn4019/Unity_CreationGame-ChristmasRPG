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
    Store store;

    [SerializeField]
    GameObject slotsParent;

    [SerializeField]
    Text moneyText;

    [SerializeField]
    Item grayKey;

    bool activeInventory;
    bool isTouchInventoryBtn;
    int slotCount = 3;
    int index;

    public int money = 1000;

    Action slotCountChange;
    public Action<int> moneyChange; 
    InventorySlot[] slots;
    Player player;

    //[ser]
    public Dictionary<string,int> items = new Dictionary<string, int>();

    //[SerializeField]
    //List<ItemPickUp> test;

    // Start is called before the first frame update
    void Start()
    {
        slots = slotsParent.GetComponentsInChildren<InventorySlot>();
        inventory.SetActive(false);
        activeInventory = false;
        moneyChange += MoneyUpdate;
        slotCountChange += SlotChange;
        slotCountChange.Invoke();
        player = GetComponent<Player>();
        moneyText.text = money.ToString() + " 원";
    }

    // Update is called once per frame
    void Update()
    {
        InventoryOnOff();
        //FindPotion();

        //Debug.Log("player" + player.isTouchPotionBtn);
        //if (slot == null)
        //    Debug.Log("아 ㅋㅋ");

        foreach(KeyValuePair<string , int> dic in items)
        {
            Debug.Log("아이템 이름 : " + dic.Key + " 개수 : " + dic.Value);
        }
        
    }

    #region 인벤토리 UI
    public void TouchInveontory()
    {
        if (store.IsInSotre)
            return;

        isTouchInventoryBtn = true;
    }

    void InventoryOnOff()
    {
        if (store.IsInSotre)
            return;

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
        slotCount += 3;
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
                    int itemValue = items[item.itemName];
                    items[item.itemName] = itemValue + count;
                    
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
                items.Add(item.itemName, count);
                //items.Add(item);

                return;
            }
        }
    }

    public void BuyItem(int count = 1)
    {
        //돈없으면 못사게
        if (store.item.itemPrice > money)
            return;

        int price = store.item.itemPrice;
        moneyChange.Invoke(-price);

        //money -= store.item.itemPrice;
        //moneyText.text = money.ToString() + " 원";

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == store.item.itemName)
                {
                    slots[i].SetSlotCount(count);
                    int itemValue = items[store.item.itemName];
                    items[store.item.itemName] = itemValue + count;
                    //items[i].itemCount++;
                    return;
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(store.item, count);
                items.Add(store.item.itemName, count);
                //items.Add(item);
                return;
            }
        }
    }

    public void GetRewardGrayKey(int count)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                if (slots[i].item.itemName == grayKey.itemName)
                {
                    slots[i].SetSlotCount(count);
                    int itemValue = items[grayKey.itemName];
                    items[grayKey.itemName] = itemValue + count;
                    //items[i].itemCount++;
                    return;
                }
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(grayKey, count);
                items.Add(grayKey.itemName, count);
                //items.Add(item);
                return;
            }
        }

        //if (items.ContainsKey("GrayKey"))
        //{
        //    int count = items["GrayKey"];
        //    items["GrayKey"] = count + value;
        //}
        //else
        //{
        //    items.Add("GrayKey", value);
        //}
    }

    void MoneyUpdate(int price)
    {
        money += price;
        moneyText.text = money.ToString() + " 원";
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
