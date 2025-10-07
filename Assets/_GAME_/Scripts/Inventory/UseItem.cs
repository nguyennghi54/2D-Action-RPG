using System.Collections.Generic;
using _GAME_.Scripts.Player;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    private float tmp;
    public void ApplyItemEffect(ItemSO slot, PlayerPrefab player)
    {
        // Heal item
        if (slot.heal > 0)
        {
            player.UpdateCurrentHP((int)slot.heal);
            return;
        }
        // Max HP boost
        tmp = slot.statEffectDict.GetValueOrDefault(UnitStat.MaxHP);
        if (tmp > 0)
        {
            player.UpdateMaxHP((int) tmp);
            return;
        }
        // Speed boost
        tmp = slot.statEffectDict.GetValueOrDefault(UnitStat.MoveSpeed);
        if (tmp > 0)
        {
            player.UpdateMoveSpeed((int) tmp);
            return;
        }
        // Attack boost
        tmp = slot.statEffectDict.GetValueOrDefault(UnitStat.Attack);
        if (tmp > 0)
        {
            player.UpdateAttackDamage((int) tmp);
            return;
        }
        
    }
}
