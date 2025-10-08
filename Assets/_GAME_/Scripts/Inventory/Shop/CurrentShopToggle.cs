using UnityEngine;

public class CurrentShopToggle : MonoBehaviour
{
    public void OpenItemShop()
    {
        if (Shopkeeper.currentShopkeeper != null)
            Shopkeeper.currentShopkeeper.OpenItemShop();
    }
    public void OpenWeaponShop()
    {
        if (Shopkeeper.currentShopkeeper != null)
            Shopkeeper.currentShopkeeper.OpenWeaponShop();
    }
    public void OpenAmorShop()
    {
        if (Shopkeeper.currentShopkeeper != null)
            Shopkeeper.currentShopkeeper.OpenArmorShop();
    }
    
}
