using _GAME_.Scripts.Player;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] itemSlots;
    public int gold;
    [SerializeField] private UseItem useItem;
    public TextMeshProUGUI goldText;
    [SerializeField] private GameObject lootPrefab;
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
        goldText.text = $"{gold}";
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
    /// If item looted is gold, add to gold amount. 
    /// Else add to slots
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
        // Check if stack slot haves room
        // If exceed -> add the minimum to slot's quantity
        foreach (var slot in itemSlots)
        {
            if (slot.item == item && slot.quantity < item.stackSize)
            {
                int availSpace = item.stackSize - slot.quantity;
                int amountToAdd = Mathf.Min(availSpace, quantity);
                slot.quantity += amountToAdd;
                quantity -= amountToAdd;    // subtract added instances of item
                
                slot.UpdateSlotUI();
                if (quantity <= 0)  // item ran out
                    return;
            }
        }
        // If item remains & slot reaches stackSize -> check empty slot
        foreach (var slot in itemSlots)
        {
            if (slot.item == null)
            {
                int amountToAdd = Mathf.Min(item.stackSize, quantity);
                slot.item = item;
                slot.quantity = amountToAdd;
                quantity -= amountToAdd;
                slot.UpdateSlotUI();
            }
            if (quantity <= 0)
                return;
        }
        // If Out of slot but item still remains -> Drop item
        if (quantity > 0)
        {
            DropLoot(item, quantity);
        }
        
    }

    private void DropLoot(ItemSO item, int quantity)
    {
        Loot loot = Instantiate(lootPrefab, player.gameObject.transform.position, Quaternion.identity).GetComponent<Loot>();
        loot.InitLoot(item, quantity);
    }

    public void DropItem(InventorySlot slot)
    {
        DropLoot(slot.item, 1);
        slot.quantity --;
        if (slot.quantity <= 0)
        {
            slot.item = null;
        }
        slot.UpdateSlotUI(); 
    }
}
