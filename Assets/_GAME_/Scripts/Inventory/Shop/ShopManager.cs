using System;
using System.Collections.Generic;
using _GAME_.Scripts.Player;
using UnityEngine;
/// <summary>
/// 1. Manage all ShopSlots <br />
/// 2. Handle buy items (check InventoryManager for gold)
/// </summary>
public class ShopManager : MonoBehaviour
{
    private static ShopManager activeShop;
    
    [SerializeField] private ShopSlot[] shopSlots;
    [SerializeField] private InventoryManager invManager;
    [SerializeField] private PlayerPrefab player;
    [System.Serializable]
    public class ShopItems
    {
        public ItemSO item;
        public int price;
        
    }
    public void PopulateShopSlots(List<ShopItems> shopItems)
    {
        for (int i = 0; i < shopItems.Count && i < shopSlots.Length; i++)
        {
            ShopItems shopItem = shopItems[i];
            shopSlots[i].Init(shopItem.item, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);
        }
        // Disable empty slots
        for (int i = shopItems.Count; i < shopSlots.Length; i++)
        {
            shopSlots[i].gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Check if Player has enough space and gold
    /// </summary>
    /// <param name="itemToBuy, price"></param>
    public void TryBuyItem(ItemSO item, int price)
    {
        if (item != null && invManager.gold >= price) // has enough money
        {
            if (HasSpaceForItem(item))
            {
                invManager.gold -= price;
                invManager.goldText.text = $"{invManager.gold}";
                invManager.AddItemToInventory(item, 1, player);
            }
        } 
            
        
    }

    private bool HasSpaceForItem(ItemSO item)
    {
        foreach (InventorySlot invSlot in invManager.itemSlots)
        {
            // Check for avail stack slot
            if (invSlot.item == item && invSlot.quantity < item.stackSize)
            {
                return true;
            }
            // Check for empty slot
            else if (invSlot.item != item)
            {
                return true;
            }
        }
        // No room left
        return false;
    }

    public void SellItem(ItemSO item)
    {
        if (item == null) return;
        // Traverse shop's current items
        foreach (ShopSlot slot in shopSlots)
        {
            // Check if shop has same item
            if (item == slot.item)
            {
                invManager.gold += slot.price;
                invManager.goldText.text = $"{invManager.gold}";
                return;
            }
        }
    }
    
}
