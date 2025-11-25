using System.Collections.Generic;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerPrefab : MonoBehaviour
    {
        public Vector3 spawnPos;
        private float maxHP, moveSpeed, attack, attackCD, weaponRange, knockForce, knockTime, stunTime;
        public Dictionary<UnitStat, float> statDict;
        [SerializeField] private UnitStats initialStat;
        
        [SerializeField] private UnitStats unitStats;
        [SerializeField] public PlayerHealth playerHealth;
        [HideInInspector] public float level;
        [SerializeField] private StatsUI statsUI;
        
        [SerializeField] public AudioSource audioSource;
        [SerializeField] public AudioManager audioManager;
        [SerializeField] public ExpManager expManager;

        void Awake()
        {
            
        }
        void Start()
        {
            audioManager = FindFirstObjectByType<AudioManager>();
        }

        private void OnEnable()
        {
            PopulateInitialStatDict();
        }
            
        /// <summary>
        /// Khởi tạo ingame copy của stat dictionary (reset khi thoát game)
        /// </summary>
        void PopulateInitialStatDict()
        {
            // map stat to initial
            unitStats.statDict[UnitStat.MaxHP] = initialStat.statDict.GetValueOrDefault(UnitStat.MaxHP);
            unitStats.statDict[UnitStat.MoveSpeed] = initialStat.statDict.GetValueOrDefault(UnitStat.MoveSpeed);
            unitStats.statDict[UnitStat.Attack] = initialStat.statDict.GetValueOrDefault(UnitStat.Attack);
            unitStats.statDict[UnitStat.AttackCD] = initialStat.statDict.GetValueOrDefault(UnitStat.AttackCD);
            unitStats.statDict[UnitStat.WeaponRange] = initialStat.statDict.GetValueOrDefault(UnitStat.WeaponRange);
            unitStats.statDict[UnitStat.KnockForce] = initialStat.statDict.GetValueOrDefault(UnitStat.KnockForce);
            unitStats.statDict[UnitStat.KnockTime] = initialStat.statDict.GetValueOrDefault(UnitStat.KnockTime);
            unitStats.statDict[UnitStat.StunTime] = initialStat.statDict.GetValueOrDefault(UnitStat.StunTime);
            // get stat values
            statDict = unitStats.statDict;
            maxHP = statDict.GetValueOrDefault(UnitStat.MaxHP);
            moveSpeed = statDict.GetValueOrDefault(UnitStat.MoveSpeed);
            attack = statDict.GetValueOrDefault(UnitStat.Attack);
            attackCD = statDict.GetValueOrDefault(UnitStat.AttackCD);
            weaponRange = statDict.GetValueOrDefault(UnitStat.WeaponRange);
            knockForce = statDict.GetValueOrDefault(UnitStat.KnockForce);
            knockTime = statDict.GetValueOrDefault(UnitStat.KnockTime);
            stunTime = statDict.GetValueOrDefault(UnitStat.StunTime);
            maxHP = statDict.TryGetValue(UnitStat.MaxHP, out var value) ? value : maxHP;
        }
        
        public void ResetStat()
        {
            PopulateInitialStatDict();
        }
        
        public void UpdateMaxHP(int amount)
        {
            maxHP += amount;
            unitStats.statDict[UnitStat.MaxHP] = maxHP;
            playerHealth.UpdateHealthUI();
        }

        public void UpdateCurrentHP(int amount)
        {
            playerHealth.ChangeHealth(amount);
        }

        public void UpdateAttackDamage(int amount)
        {
            attack += amount;
            unitStats.statDict[UnitStat.Attack] = attack;
            statsUI.UpdateAttackUI();
        }
        public void UpdateMoveSpeed(int amount)
        {
            moveSpeed += amount;
            unitStats.statDict[UnitStat.MoveSpeed] = moveSpeed;
            statsUI.UpdateSpeedUI();
        }
    }
}

