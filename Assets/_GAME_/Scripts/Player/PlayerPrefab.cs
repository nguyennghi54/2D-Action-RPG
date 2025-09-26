using UnityEngine;

namespace _GAME_.Scripts.Player
{
    public class PlayerPrefab : MonoBehaviour
    {
        [HideInInspector]
        public float maxHP, moveSpeed, attackCD, weaponRange, knockForce, knockTime, stunTime;
        [HideInInspector]
        public int attack;
        
        [SerializeField] private UnitStats unitStats;
        private void OnEnable()
        {
            maxHP = unitStats.MaxHP;
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

