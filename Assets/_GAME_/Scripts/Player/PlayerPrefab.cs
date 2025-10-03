using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerPrefab : MonoBehaviour
    {
        private float maxHP, moveSpeed, attack, attackCD, weaponRange, knockForce, knockTime, stunTime;
        public Dictionary<UnitStat, float> statDict;
        [SerializeField] private UnitStats unitStats;
        [SerializeField] private PlayerHealth playerHealth;
        [HideInInspector] public float level;
        private void OnEnable()
        {
            statDict = unitStats.statDict;
            maxHP = statDict.GetValueOrDefault(UnitStat.MaxHP);
            attack = statDict.GetValueOrDefault(UnitStat.Attack);
            attackCD = statDict.GetValueOrDefault(UnitStat.AttackCD);
            weaponRange = statDict.GetValueOrDefault(UnitStat.WeaponRange);
            knockForce = statDict.GetValueOrDefault(UnitStat.KnockForce);
            knockTime = statDict.GetValueOrDefault(UnitStat.KnockTime);
            stunTime = statDict.GetValueOrDefault(UnitStat.StunTime);
            maxHP = statDict.TryGetValue(UnitStat.MaxHP, out var value) ? value : maxHP;
        }

        public void UpdateMaxHP(int amount)
        {
            maxHP += amount;
            unitStats.statDict[UnitStat.MaxHP] = maxHP;
            playerHealth.UpdateHealthUI();
        }
    }
}

