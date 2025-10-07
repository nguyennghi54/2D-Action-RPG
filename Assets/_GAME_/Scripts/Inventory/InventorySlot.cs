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

    private InventoryManager manager;

    void Start()
    {
        manager = transform.parent.GetComponentInParent<InventoryManager>();
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
            if (eventData.button == PointerEventData.InputButton.Left)  // use item
            {
                manager.UseItem(this);
            }
            else if (eventData.button == PointerEventData.InputButton.Right)    // drop item
            { 
                manager.DropItem(this);
            }
        }
    }
    public void UpdateSlotUI()
    {
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
