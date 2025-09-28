using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerPrefab : MonoBehaviour
    {
        [HideInInspector]
        public float maxHP, moveSpeed, attackCD, weaponRange, knockForce, knockTime, stunTime;
        [HideInInspector]
        public int attack;
        [SerializeField] private TestStat testStat;
        private SerializedDictionary<Stat, float> statDict;
        [SerializeField] private UnitStats unitStats;
        private void OnEnable()
        {
            statDict = testStat.statDict;
            maxHP = statDict.TryGetValue(Stat.MaxHP, out float value) ? value : 0f;
            moveSpeed = unitStats.Speed;
            attack = unitStats.Attack;
            attackCD = unitStats.AttackCD;
            weaponRange = unitStats.WeaponRange;
            knockForce = unitStats.KnockForce;
            knockTime = unitStats.KnockTime;
            stunTime = unitStats.StunTime;
        }
    }
}

