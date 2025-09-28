using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEditor;
using UnityEngine;

public enum Stat
{
    MaxHP, MoveSpeed, AttackCD, WeaponRange, KnockForce, KnockTime, StunTime
    
}
[CreateAssetMenu(fileName = "TestStat", menuName = "Scriptable Objects/TestStat")]
public class TestStat : ScriptableObject
{
    [SerializedDictionary("Stat", "Value")]
    public SerializedDictionary<Stat, float> statDict;
    
}
