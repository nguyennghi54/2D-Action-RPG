using System;
using _GAME_.Scripts.Player;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private int quantity;
    
    public static event Action<ItemSO, int, PlayerPrefab> OnItemLooted;
    private PlayerPrefab player;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnValidate()
    {
        if (item == null)
            return;
        sr.sprite = item.itemIcon;
        this.name = item.itemName;
    }

    /// <summary>
    /// When Player pick item up
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<PlayerPrefab>();
            anim.Play("LootPickup");
            OnItemLooted?.Invoke(item, quantity, player); // pass loot's information
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, .5f);
        }
    }
}
