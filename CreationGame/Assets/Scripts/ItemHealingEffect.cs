using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ItemEffect/HealthEffect")]
public class ItemHealingEffect : ItemEffect
{
    [SerializeField]
    int amountPotion = 20;

    public override bool ExcuteRole()
    {
        Player player = FindObjectOfType<Player>();
        player.RecoveryHp(amountPotion);

        return true;
    }
}
