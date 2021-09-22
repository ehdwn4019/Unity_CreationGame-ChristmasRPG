using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour , IPointerClickHandler// , IBeginDragHandler , IDragHandler , IEndDragHandler , IDropHandler
{
    public Item item;
    public int itemCount;
    public Image itemImg;
    public int index;
    public Image slotImg;

    //[SerializeField]
    public Text itemCountText;

    [SerializeField]
    GameObject itemCountImg;

    ItemEffect itemEffect;

    Player player;
    PlayerInventory inven;
    //Text itemCountText;

    //[SerializeField]
    //GameObject itemCountTextObj;

    //[SerializeField]
    //GameObject test;

    private void Start()
    {
        itemEffect = FindObjectOfType<ItemEffect>();
        player = FindObjectOfType<Player>();
        //player = FindObjectOfType<Player>();
        inven = FindObjectOfType<PlayerInventory>();


        //itemCountText = GetComponentInChildren<Text>();
        //itemCountImg = GetComponentInChildren<GameObject>().GetComponentInChildren<GameObject>();
        //itemCountText = GameObject.Find("ItemImg").transform.Find()
        //itemCountText = itemCountTextObj.GetComponent<Text>();
    }

    private void Update()
    {
        //TouchPotionBtn();
        Debug.Log(player.IsTouchPotionBtn);
        //Debug.Log("isTouchSlotBtn : "+isTouchSlotBtn);
        //UsePotion();
        Debug.Log(index);
    }

    //필요하면 사용하기
    //이미지 투명도 조절
    void SetColor(float alpha)
    {
        Color color = itemImg.color;
        color.a = alpha;
        itemImg.color = color;
    }

    //아이템 획득
    public void AddItem(Item item,int count = 1)
    {
        this.item = item;
        this.itemCount = count;
        this.itemImg.sprite = item.itemImg;
        
        //this.itemImg.gameObject.SetActive(true);
        this.itemCountImg.SetActive(true);
        this.itemCountText.text = itemCount.ToString();

        //필요하면 사용
        SetColor(1);
    }

    //아이템 개수 조정
    public void SetSlotCount(int count)
    {
        itemCount += count;
        itemCountText.text = itemCount.ToString();

        if(item.itemType == Item.ItemType.Potion)
        {
            //inven.GetSlotIndex(index);
            player.potionCountText.text = itemCount.ToString();
        }
        
        if (itemCount <=0 )
        {
            ClearSlot();
        }
    }

    public void SetPotionCount()
    {
        itemCount--;
        itemCountText.text = itemCount.ToString();
        player.potionCountText.text = itemCount.ToString();

        if (itemCount <= 0)
        {
            player.potionCountText.text = "0";
            ClearSlot();
        }
    }

    public void SetSlotPotionCount(int count)
    {
        itemCount += count;
        itemCountText.text = itemCount.ToString();
        player.potionCountText.text = itemCount.ToString();
    }

    //아이템 초기화
    void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImg.sprite = null;

        //필요하면 사용하기
        SetColor(0);

        itemCountText.text = "0";
        itemCountImg.SetActive(false);
        //itemImg.gameObject.SetActive(false);
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

        itemImg.sprite = item.itemImg;
        itemImg.gameObject.SetActive(true);

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

    //public void SetSlotCount(int count)
    //{
    //    itemCount += count;
    //    itemCountText.text = itemCount.ToString();
    //}

    public void RemoveSlot()
    {
        item = null;
        itemCount = 0;
        itemCountText.text = "0";
        //itemCountImg.gameObject.SetActive(false);
        itemImg.gameObject.SetActive(false);
    }

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    Debug.Log("QQW");
    //    //isTouchSlotBtn = true;
    //
    //    if (item == null)
    //        return;
    //
    //    //여기
    //    //if(item.Use())
    //    //{
    //    //    Debug.Log("AA");
    //    //    //PlayerInventory inven = GetComponent<PlayerInventory>();
    //    //    //Player player = GetComponent<Player>();
    //    //    //player.RecoveryHp();
    //    //    inven.RemoveItem(index);
    //    //}
    //
    //    //isTouchSlotBtn = false;
    //}

    //아이템 사용 
    public void OnPointerClick(PointerEventData eventData)
    {
        //if(eventData. == PointerEventData.InputButton)
        if(item!=null)
        {
            //나중에 포션으로 바꾸기 
            if(item.itemType == Item.ItemType.Potion)
            {
                itemEffect.UseItem(item);
                SetSlotCount(-1);
            }
        }
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    if(item != null)
    //    {
    //        DragSlot.instance.dragSlot = this;
    //        DragSlot.instance.DragSetImg(itemImg);
    //        DragSlot.instance.transform.position = eventData.position;
    //        //transform.position = eventData.position;
    //    }
    //}
    //
    //public void OnDrag(PointerEventData eventData)
    //{
    //    if(item!=null)
    //    {
    //        DragSlot.instance.transform.position = eventData.position;
    //        //transform.position = eventData.position;
    //    }
    //}
    //
    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    //transform.position = startPos;
    //    DragSlot.instance.SetColor(0);
    //    DragSlot.instance.dragSlot = null;
    //}
    //
    //public void OnDrop(PointerEventData eventData)
    //{
    //    //빈슬롯 드래그 오류 방지
    //    if (DragSlot.instance.dragSlot != null)
    //    {
    //        ChangeSlot();
    //    }
    //}

    //void ChangeSlot()
    //{
    //    Item tempItem = item;
    //    int tempItemCount = itemCount;
    //
    //    AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
    //
    //    if(tempItem != null)
    //    {
    //        DragSlot.instance.dragSlot.AddItem(tempItem, tempItemCount);
    //    }
    //    else
    //    {
    //        DragSlot.instance.dragSlot.ClearSlot();
    //    }
    //}

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
