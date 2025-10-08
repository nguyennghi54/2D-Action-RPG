using System;
using System.Collections.Generic;
using UnityEngine;

public class Shopkeeper : MonoBehaviour
{
    public static Shopkeeper currentShopkeeper;

    [SerializeField] private Animator anim;
    [SerializeField] private CanvasGroup shopCanvas;
    [SerializeField] private ShopManager shopManager;
    
    [Header("Item lists")] [SerializeField]
    private List<ShopManager.ShopItems> shopItems;
    [SerializeField] private List<ShopManager.ShopItems> shopWeapon;
    [SerializeField] private List<ShopManager.ShopItems> shopArmor;
    
    [Header("Portrait")]
    [SerializeField] private Camera portraitCamera;
    [SerializeField] private Vector3 camOffset;
    public static event Action<ShopManager, bool> OnShopStateChanged;
    private Vector3 facingDir;

    private bool playerInRange;

    void Start()
    {
        facingDir = transform.localScale;
    }
    /// <summary>
    /// Press E to open/close shop
    /// </summary>
    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                currentShopkeeper = this;
                EnableShopCanvas(true);
                OnShopStateChanged?.Invoke(shopManager, true);
                OpenItemShop(); //default
                portraitCamera.transform.position = transform.position + camOffset;
                portraitCamera.gameObject.SetActive(true);
            }
            if (Input.GetButtonDown("Cancel"))
            {
                currentShopkeeper = null;
                EnableShopCanvas(false);
                OnShopStateChanged?.Invoke(shopManager, false);
                portraitCamera.gameObject.SetActive(false);
            }
        }
    }


    void EnableShopCanvas(bool enable)
    {
        Time.timeScale = enable ? 0 : 1;
        shopCanvas.alpha = enable ? 1 : 0;
        shopCanvas.blocksRaycasts = enable;
        shopCanvas.interactable = enable;
    }

    public void OpenItemShop()
    {
        shopManager.PopulateShopSlots(shopItems);
    }

    public void OpenWeaponShop()
    {
        shopManager.PopulateShopSlots(shopWeapon);
    }

    public void OpenArmorShop()
    {
        shopManager.PopulateShopSlots(shopArmor);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", true);
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("playerInRange", false);
            playerInRange = false;
        }
    }
}