using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public ItemSO item;
    public int quantity;
    
    public Image itemImage;
    public TMP_Text quantityText;

    private InventoryManager invManager;
    private static ShopManager activeShop;
    void Start()
    {
        invManager = transform.parent.GetComponentInParent<InventoryManager>();
    }
    void OnEnable()
    {
        ShopManager.OnShopStateChanged += HandleShopStateChanged;
    }
    void OnDisale()
    {
        ShopManager.OnShopStateChanged -= HandleShopStateChanged;
    }
    
    /// <summary>
    /// Check if shop is currently open, <br />
    /// If yes, handle Inventory's click differently
    /// </summary>
    /// <param name="shopManager, isOpen"></param>
    private void HandleShopStateChanged(ShopManager shopManager, bool isOpen)
    {
        activeShop = isOpen ? shopManager : null;   // if shop's open, pass in shopManager
        

    }

    /// <summary>
    /// When click on slot, 
    /// If left click, use item. Right click, drop item
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (quantity > 0)
        {
            if (eventData.button == PointerEventData.InputButton.Left)  
            {
                // Buy Item from shop
                if (activeShop != null)
                {
                    activeShop.SellItem(item);
                    quantity--;
                    UpdateSlotUI();
                }
                // Use Item
                else
                {
                    invManager.UseItem(this);
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)    // drop item
            { 
                invManager.DropItem(this);
            }
        }
    }
    public void UpdateSlotUI()
    {
        if (quantity <= 0)
            item = null;
        
        if (item != null)
        {
            itemImage.sprite = item.itemIcon;
            itemImage.gameObject.SetActive(true);
            quantityText.text = $"{quantity}";
        }
        else
        {
            itemImage.gameObject.SetActive(false);
            quantityText.text = "";
        }
    }
}
