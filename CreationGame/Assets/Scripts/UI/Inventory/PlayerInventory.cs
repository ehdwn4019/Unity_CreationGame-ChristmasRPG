using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerInventory : MonoBehaviour
{
    public Action<int> moneyChange;
    public Dictionary<string, int> items = new Dictionary<string, int>();

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

    Action slotCountChange;
    InventorySlot[] slots;
    Player player;

    public int money = 1000;
    bool activeInventory;
    bool isTouchInventoryBtn;
    int slotCount = 3;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        slots = slotsParent.GetComponentsInChildren<InventorySlot>();
        player = GetComponent<Player>();
        inventory.SetActive(false);
        activeInventory = false;
        moneyChange += MoneyUpdate;
        slotCountChange += SlotChange;
        slotCountChange.Invoke();
        moneyText.text = money.ToString() + " 원";
    }

    // Update is called once per frame
    void Update()
    {
        InventoryOnOff();

        foreach(KeyValuePair<string , int> dic in items)
        {
            Debug.Log("아이템 이름 : " + dic.Key + " 개수 : " + dic.Value);
        }
        
    }

    #region 인벤토리 UI
    //상점에 있는경우 인벤토리가 자동으로 열리기 때문에 직접열기 불가능
    public void TouchInveontory()
    {
        if (store.IsInSotre)
            return;

        isTouchInventoryBtn = true;
    }

    //인벤토리 온오프
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

    //추가된 슬롯에 따라서 슬롯 활성화
    public void SlotChange()
    {
        for(int i=0; i<slots.Length; i++)
        {
            slots[i].index = i;

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
    
    //슬롯 추가하기
    public void AddSlot()
    {
        slotCount += 3;
        slotCountChange.Invoke();
    }

    #endregion

    #region 인벤토리 아이템
    public void AcquireItem(Item item, int count = 1)
    {
        SoundManager.instance.PlaySoundEffect("아이템획득");

        //슬롯에 아이템이 있는경우 
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                //슬롯아이템과 획득한 아이템이 같은 경우
                if (slots[i].item.itemName == item.itemName)
                {
                    slots[i].SetSlotCount(count);
                    if(items.ContainsKey(item.itemName))
                    {
                        int itemValue = items[item.itemName];
                        items[item.itemName] = itemValue + count;
                    }
                    return;
                }
            }
        }

        //슬롯에 아이템이 없는 경우
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(item, count);
                if(!items.ContainsKey(item.itemName))
                {
                    items.Add(item.itemName, count);
                }
                return;
            }
        }
    }

    public void BuyItem(int count = 1)
    {
        //머니가 부족한 경우
        if (store.item.itemPrice > money)
            return;

        //머니 획득시 보유머니 최신화
        int price = store.item.itemPrice;
        moneyChange.Invoke(-price);

        SoundManager.instance.PlaySoundEffect("아이템구매");

        //슬롯에 아이템이 있는 경우
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                //슬롯아이템과 상점 아이템이 같은 경우
                if (slots[i].item.itemName == store.item.itemName)
                {
                    slots[i].SetSlotCount(count);
                    if(items.ContainsKey(store.item.itemName))
                    {
                        int itemValue = items[store.item.itemName];
                        items[store.item.itemName] = itemValue + count;
                    }
                    return;
                }
            }
        }

        //슬롯에 아이템이 없는 경우
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(store.item, count);
                if(!items.ContainsKey(store.item.itemName))
                {
                    items.Add(store.item.itemName, count);
                }
                return;
            }
        }
    }


    public void GetRewardGrayKey(int count)
    {
        //슬롯에 아이템이 있는 경우 (퀘스트 보상 획득시 , 장애물 통과후 획득시)
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                //슬롯에 아이템이 키와 같은 경우
                if (slots[i].item.itemName == grayKey.itemName)
                {
                    slots[i].SetSlotCount(count);
                    if(items.ContainsKey(grayKey.itemName))
                    {
                        int itemValue = items[grayKey.itemName];
                        items[grayKey.itemName] = itemValue + count;
                    }
                    return;
                }
            }
        }

        //슬롯에 아이템이 없는 경우 
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(grayKey, count);
                if(!items.ContainsKey(grayKey.itemName))
                {
                    items.Add(grayKey.itemName, count);
                }
                return;
            }
        }
    }

    //MoneyChange Action에 추가된 함수 , 보유 머니 최신화
    void MoneyUpdate(int price)
    {
        money += price;
        moneyText.text = money.ToString() + " 원";
    }

    #endregion
}
