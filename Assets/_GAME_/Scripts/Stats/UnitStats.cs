using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEditor.Presets;
public enum UnitStat
{
    [Description("MaxHP")] MaxHP, 
    [Description("SPD")] MoveSpeed, 
    [Description("ATK")] Attack, 
    [Description("ATKCD")] AttackCD, 
    [Description("RNG")] WeaponRange, 
    [Description("KFC")] KnockForce, 
    [Description("KTM")] KnockTime, 
    [Description("STM")] StunTime
}
[System.Serializable]
[CreateAssetMenu(fileName="UnitStats", menuName = "Stat Manager/UnitStats")]
public class UnitStats : ScriptableObject
{
    [SerializedDictionary("Stat", "Value")]
    public SerializedDictionary<UnitStat, float> statDict;
    
}

public static class EnumExtensions
{
    public static string GetEnumDesc(this Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());
        if (field == null)
            return value.ToString(); // Fallback to enum name
            
        DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
        // Return desc OR enum name as fallback
        return attribute == null ? value.ToString() : attribute.Description;
    }
}
