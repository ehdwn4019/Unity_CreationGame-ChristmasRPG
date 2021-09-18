using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour , IPointerUpHandler
{
    public Item item;
    public int itemCount;
    public Image iconImg;
    public int index;

    Player player;
    PlayerInventory inven;
    Text itemCountText;

    [SerializeField]
    GameObject itemCountTextObj;

    [SerializeField]
    GameObject itemCountImg;

    //[SerializeField]
    //GameObject test;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        inven = FindObjectOfType<PlayerInventory>();
        //itemCountText = GetComponentInChildren<Text>();
        //itemCountImg = GetComponentInChildren<GameObject>().GetComponentInChildren<GameObject>();
        //itemCountText = GameObject.Find("ItemImg").transform.Find()
        itemCountText = itemCountTextObj.GetComponent<Text>();
    }

    private void Update()
    {
        //Debug.Log("isTouchSlotBtn : "+isTouchSlotBtn);
        //UsePotion();
    }

    public void UpdateSlotUI()
    {
        //if(item.itemType == Item.ItemType.Potion)
        //{
        //    iconImg.sprite = item.itemImg;
        //    iconImg.gameObject.SetActive(true);
        //
        //    
        //}
        //else
        //{
        //    iconImg.sprite = item.itemImg;
        //    iconImg.gameObject.SetActive(true);
        //
        //    //나중에 위아래 바꾸기
        //    itemCountImg.SetActive(true);
        //    itemCountText.text = itemCount.ToString();
        //}

        iconImg.sprite = item.itemImg;
        iconImg.gameObject.SetActive(true);

        if (itemCount>=1)
        {
            itemCountImg.SetActive(true);
            itemCountText.text = itemCount.ToString();
        }
        else
        {
            itemCountText.text = "0";
            itemCountImg.SetActive(false);
        }
    }

    public void SetSlotCount(int count)
    {
        itemCount += count;
        itemCountText.text = itemCount.ToString();
    }

    public void RemoveSlot()
    {
        item = null;
        itemCount = 0;
        itemCountText.text = "0";
        //itemCountImg.gameObject.SetActive(false);
        iconImg.gameObject.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("QQW");
        //isTouchSlotBtn = true;

        if (item == null)
            return;
    
        if(item.Use())
        {
            Debug.Log("AA");
            //PlayerInventory inven = GetComponent<PlayerInventory>();
            //Player player = GetComponent<Player>();
            //player.RecoveryHp();
            inven.RemoveItem(index);
        }

        //isTouchSlotBtn = false;
    }

    //public void TouchSlot()
    //{
    //    isTouchSlotBtn = true;
    //}
    
    //public void UsePotion()
    //{
    //    if (item == null)
    //        return;
    //
    //    if (item.Use() && isTouchSlotBtn)
    //    {
    //        //isTouchSlotBtn = true;
    //        Debug.Log("AA");
    //        //PlayerInventory inven = GetComponent<PlayerInventory>();
    //        //Player player = GetComponent<Player>();
    //        //player.RecoveryHp();
    //        //inven.GetPotionIndex(slotNum);
    //        inven.RemoveItem(index);
    //    }
    //
    //    isTouchSlotBtn = false;
    //}
}
