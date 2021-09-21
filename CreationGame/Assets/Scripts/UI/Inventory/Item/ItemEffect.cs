using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//문제되면 class로 바꾸기
public struct ItemEffectData
{
    public string itemName;
    //public string part;
    public int amountPotion;
}

public class ItemEffect : MonoBehaviour
{
    //public abstract bool ExcuteRole();
    [SerializeField]
    ItemEffectData[] itemEffectDatas;

    [SerializeField]
    Player player;

    public void UseItem(Item item)
    {
        if(item.itemType == Item.ItemType.Potion)
        {
            for(int i=0; i<itemEffectDatas.Length; i++)
            {
                if(itemEffectDatas[i].itemName == item.itemName)
                {
                    player.RecoveryHp(itemEffectDatas[i].amountPotion);
                }
            }
        }
    }
}

