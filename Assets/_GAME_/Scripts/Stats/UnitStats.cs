using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName="UnitStats", menuName = "UnitStats")]
public class UnitStats : ScriptableObject
{
    [Header("Health")] 
    [SerializeField] float maxHP;
    
    [Header("Movement")]
    [SerializeField] private float speed;
    
    [Header("Combat")]
    [SerializeField] private int attack;
    [SerializeField] private float attackCD;
    [SerializeField] private float weaponRange;
    
    [Header("Knockback")]
    [SerializeField] private float knockForce;
    [SerializeField] private float knockTime;
    [SerializeField] private float stunTime;
    
    public float MaxHP { get => maxHP; set =>  maxHP = value; }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    public int Attack { get => attack; set => attack = value; }
    public float AttackCD { get => attackCD; set => attackCD = value; }
    public float WeaponRange  { get => weaponRange; set => weaponRange = value; }
    public float KnockForce { get => knockForce; set => knockForce = value; }
    public float KnockTime { get => knockTime; set => knockTime = value; }
    public float StunTime { get => stunTime; set =>  stunTime = value; }
}
