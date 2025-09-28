using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public enum UnitStat
{
    MaxHP, MoveSpeed, Attack, AttackCD, WeaponRange, KnockForce, KnockTime, StunTime
}
[System.Serializable]
[CreateAssetMenu(fileName="UnitStats", menuName = "Stat Manager/UnitStats")]
public class UnitStats : ScriptableObject
{
    [SerializedDictionary("Stat", "Value")]
    public SerializedDictionary<UnitStat, float> statDict;
}
