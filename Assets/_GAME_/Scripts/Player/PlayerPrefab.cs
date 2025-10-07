using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerPrefab : MonoBehaviour
    {
        private float maxHP, moveSpeed, attack, attackCD, weaponRange, knockForce, knockTime, stunTime;
        public Dictionary<UnitStat, float> statDict;
        [SerializeField] private ScriptableObject unitStatsScript;
        
        [SerializeField] private UnitStats unitStats;
        [SerializeField] private PlayerHealth playerHealth;
        [HideInInspector] public float level;
        [SerializeField] private StatsUI statsUI;
        
        [SerializeField] private Preset statPreset;
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
            
#if UNITY_EDITOR
            EditorApplication.playModeStateChanged += DetectExitPlay;
#endif
        }

        void OnDisable()
        {
            EditorApplication.playModeStateChanged -= DetectExitPlay;
        }
        
        #region ResetStatWhenExit
        #if UNITY_EDITOR
        void DetectExitPlay(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingPlayMode)
                ResetStat();
            /*if (!EditorApplication.isPlayingOrWillChangePlaymode && EditorApplication.isPlaying)
                ResetStat();*/
        }
        #endif
       
        void ResetStat()
        {
            statPreset.ApplyTo(unitStatsScript);
            
        }
        #endregion
        
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

