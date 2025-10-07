using _GAME_.Scripts.Player;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] itemSlots;
    public int gold;
    [SerializeField] private UseItem useItem;
    [SerializeField] private TextMeshProUGUI goldText;
    private GameObject[] playerUnits;
    private PlayerPrefab player;
    void OnEnable()
    {
        Loot.OnItemLooted += AddItemToInventory;
    }

    void OnDisable()
    {
        Loot.OnItemLooted -= AddItemToInventory;
    }

    void Start()
    {
        foreach (var slot in itemSlots)
        {
            slot.UpdateSlotUI();
        }
    }
    
    public void UseItem(InventorySlot slot)
    {
        if (slot.item != null && slot.quantity >= 0)
        {
            useItem.ApplyItemEffect(slot.item, player);
            slot.quantity--;
            if (slot.quantity <= 0)
            {
                slot.item = null;
            }
            slot.UpdateSlotUI();
        }
    }
    /// <summary>
    /// If item looted is gold, add to gold amount 
    /// Else add to empty slot
    /// </summary>
    /// <param name="item"></param>
    /// <param name="quantity"></param>
    public void AddItemToInventory(ItemSO item, int quantity, PlayerPrefab player)
    {
        this.player = player;
        if (item.isGold)
        {
            gold += quantity;
            goldText.text = $"{gold}";
            return;
        }
        if(!item.isGold)
        {
            foreach (var slot in itemSlots)
            {
                if (slot.item == null)
                {
                    slot.item = item;
                    slot.quantity = quantity;
                    slot.UpdateSlotUI();
                    return;
                }
            }
        }
    }
}
