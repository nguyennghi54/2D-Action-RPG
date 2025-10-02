using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private SpriteRenderer enemySprite;
    [SerializeField] private int expReward; // EXP Player gain when enemy defeated
    public delegate void EnemyDefeated(int exp);
    public static event EnemyDefeated OnEnemyDefeated;
    void Start()
    {
        currentHealth = maxHealth;
        enemySprite = GetComponent<SpriteRenderer>();
    }
    
    /// <summary>
    /// Manage Enemy's health
    /// </summary>
    /// <param name="damage amount"></param>
    public void ChangeHealth(float amount)
    {
        StartCoroutine(HurtAnimation());
        //Debug.Log("Enemy hit!");
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            OnEnemyDefeated(expReward);
            Destroy(gameObject);
        }
    }

    IEnumerator HurtAnimation()
    {
        enemySprite.color = Color.crimson;
        yield return new WaitForSeconds(0.2f);
        enemySprite.color =  Color.white;
    }
}
