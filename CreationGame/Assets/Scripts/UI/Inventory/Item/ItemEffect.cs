using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템의 정보
[System.Serializable]
public struct ItemEffectData
{
    public string itemName;
    public int amountPotion;
}

//아이템 획득시 플레이어에게 적용될 효과
public class ItemEffect : MonoBehaviour
{
    [SerializeField]
    ItemEffectData[] itemEffectDatas;

    [SerializeField]
    Player player;

    //아이템 사용
    public void UseItem(Item item)
    {
        if(item.itemType == Item.ItemType.Potion)
        {
            for(int i=0; i<itemEffectDatas.Length; i++)
            {
                if(itemEffectDatas[i].itemName == item.itemName)
                {
                    SoundManager.instance.PlaySoundEffect("포션");
                    player.RecoveryHp(itemEffectDatas[i].amountPotion);
                }
            }
        }
    }
}

