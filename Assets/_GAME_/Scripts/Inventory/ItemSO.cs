using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Inventory/Create new Item")]
public class ItemSO : ScriptableObject
{
    [Header("Info")]
    public string itemName;
    [TextArea] public string description;
    public Sprite itemIcon;
    
    [SerializeField] public bool isGold;

    [Header("Temporary items")] 
    public float duration;

    [Header("Stat effect")]
    [SerializedDictionary] public SerializedDictionary<UnitStat, float> statEffectDict;
    [SerializeField] public float heal;
}
