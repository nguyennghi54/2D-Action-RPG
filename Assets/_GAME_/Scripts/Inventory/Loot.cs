using System;
using _GAME_.Scripts.Player;
using Unity.Cinemachine;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private int quantity;
    private bool pickable = true;
    public static event Action<ItemSO, int, PlayerPrefab> OnItemLooted;
    private PlayerPrefab player;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void InitLoot(ItemSO item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
        pickable = false;
        UpdateUI();
    }
    public void UpdateUI()
    {
        sr.sprite = item.itemIcon;
        this.name = item.itemName;
    }
    void OnValidate()
    {
        if (item == null)
            return;
        UpdateUI();
    }
    
    /// <summary>
    /// When Player pick item up
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && pickable)
        {
            player = other.gameObject.GetComponent<PlayerPrefab>();
            anim.Play("LootPickup");
            OnItemLooted?.Invoke(item, quantity, player); // pass loot's information
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Destroy(gameObject, .5f);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pickable = true; 
            //gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
        }
        
    }
    
}
