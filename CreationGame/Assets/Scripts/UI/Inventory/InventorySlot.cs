using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour , IPointerClickHandler
{
    public Item item;
    public Image itemImg;
    public Image slotImg;
    public Text itemCountText;

    [SerializeField]
    GameObject itemCountImg;

    ItemEffect itemEffect;
    Player player;
    PlayerInventory inven;

    public int itemCount;
    public int index;

    private void Start()
    {
        itemEffect = FindObjectOfType<ItemEffect>();
        player = FindObjectOfType<Player>();
        inven = FindObjectOfType<PlayerInventory>();
    }

    //슬롯 이미지 투명도 조절
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
        this.itemCountImg.SetActive(true);
        this.itemCountText.text = itemCount.ToString();

        SetColor(1);
    }

    //아이템 개수 조정
    public void SetSlotCount(int count)
    {
        itemCount += count;
        itemCountText.text = itemCount.ToString();

        if(item.itemType == Item.ItemType.Potion)
        {
            player.potionCountText.text = itemCount.ToString();
        }
        
        //아이템 개수가 없을 경우 
        if (itemCount <=0 )
        {
            ClearSlot();
        }
    }

    //포션 아이템
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
    //아이템 초기화
    void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImg.sprite = null;
        SetColor(0);
        itemCountText.text = "0";
        itemCountImg.SetActive(false);
    }

    public void UpdateSlotUI()
    {
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

    //아이템 사용 
    public void OnPointerClick(PointerEventData eventData)
    {
        if(item!=null)
        {
            //아이템 효과 얻기
            if(item.itemType == Item.ItemType.Potion)
            {
                itemEffect.UseItem(item);
                SetSlotCount(-1);
            }
        }
    }
}
