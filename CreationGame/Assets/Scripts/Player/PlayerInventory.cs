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
    Transform slotsContent;

    [SerializeField]
    InventorySlot[] inventorySlots;

    [SerializeField]
    List<Item> items = new List<Item>();

    Action<int> slotCountChange;
    Action changeItem;

    bool isTouchInventoryBtn;
    bool activeInventory;

    int slotCount = 4;

    public int SlotCount
    {
        get { return slotCount; }
        set
        {
            slotCount = value;
            slotCountChange.Invoke(slotCount);
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        inventorySlots = slotsContent.GetComponentsInChildren<InventorySlot>();
        inventory.SetActive(false);
        activeInventory = false;
        slotCountChange += SlotChange;
        slotCountChange.Invoke(slotCount);
        changeItem += UpdateSlotItem;
        
    }

    // Update is called once per frame
    void Update()
    {
        InventoryOnOff();
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

    public void SlotChange(int value)
    {
        for(int i=0; i<inventorySlots.Length; i++)
        {
            if(i<slotCount)
            {
                inventorySlots[i].gameObject.SetActive(true);
            }
            else
            {
                inventorySlots[i].gameObject.SetActive(false);
            }
        }
    }
    
    public void AddSlot()
    {
        SlotCount += 4;
    }

    #endregion

    #region 인벤토리 아이템
    public bool AddItem(Item item)
    {
        if(items.Count < slotCount)
        {
            items.Add(item);

            if(changeItem != null)
            {
                changeItem.Invoke();
            }
            
            return true;
        }

        return false;
    }
    
    void UpdateSlotItem()
    {
        for(int i=0; i<inventorySlots.Length; i++)
        {
            inventorySlots[i].RemoveSlot();
        }

        for(int i=0; i<items.Count; i++)
        {
            inventorySlots[i].item = items[i];
            inventorySlots[i].UpdateSlotUI();
        }
    }

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FieldItem"))
        {
            FieldItem fieldItem = other.GetComponent<FieldItem>();
            if(AddItem(fieldItem.GetItem()))
            {
                fieldItem.DestroyItem();
            }
        }
    }
}
