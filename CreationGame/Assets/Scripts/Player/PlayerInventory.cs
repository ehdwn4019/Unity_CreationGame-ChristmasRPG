using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    GameObject inventory;

    //[SerializeField]
    //InventorySlot[] inventorySlots;
    //
    //[SerializeField]
    //Transform slotsContent;

    bool isTouchInventoryBtn;
    bool activeInventory;

    // Start is called before the first frame update
    void Start()
    {
        //inventorySlots = slotsContent.GetComponentsInChildren<InventorySlot>();
        inventory.SetActive(false);
        activeInventory = false;
    }

    // Update is called once per frame
    void Update()
    {
        InventoryOnOff();
    }

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
}
