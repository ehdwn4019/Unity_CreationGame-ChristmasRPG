using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Store : MonoBehaviour , IPointerClickHandler 
{
    [SerializeField]
    GameObject inventory;

    [SerializeField]
    GameObject store;

    [SerializeField]
    BoxCollider boxCollider;

    [SerializeField]
    Image itemImg;

    [SerializeField]
    Text itemName;

    [SerializeField]
    Text itemPrice;

    public Item item;
    Canvas canvas;

    bool isInSotre;

    public bool IsInSotre { get { return isInSotre; } }

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        store.SetActive(false);
        canvas.enabled = false;
        isInSotre = false;
        SetItemList();
    }

    //상점 아이템 셋팅
    void SetItemList()
    {
        itemImg.sprite = item.itemImg;
        itemName.text ="아이템 이름 : "+item.itemName;
        itemPrice.text = "가격 : "+ item.itemPrice.ToString()+" 원";
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            boxCollider.enabled = true;
            canvas.enabled = true;
            isInSotre = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            store.SetActive(false);
            inventory.SetActive(false);
            boxCollider.enabled = false;
            canvas.enabled = false;
            isInSotre = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(boxCollider.enabled == true)
        {
            store.SetActive(true);
            inventory.SetActive(true);
        }
    }
}
