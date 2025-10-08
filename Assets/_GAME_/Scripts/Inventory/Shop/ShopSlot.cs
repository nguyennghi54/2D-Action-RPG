using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public ItemSO item;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI priceText;
    public Image itemImage;
     
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private ShopInfo shopInfo;
    
    public int price;
    public void Init(ItemSO item, int price)
    {
        this.item = item;
        itemImage.sprite = item.itemIcon;
        itemNameText.text = item.itemName;
        this.price = price;
        priceText.text = $"{this.price}";
    }

    public void OnBuyButtonClicked()
    {
        shopManager.TryBuyItem(item, price);
    }
    
    /// <summary>
    /// When hover mouse on shopSlot, show info
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
            shopInfo.ShowItemInfo(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        shopInfo.HideItemInfo();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(item != null)
            shopInfo.FollowMouseHover();
    }
}
