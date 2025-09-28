using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ChangeHealth(float amount)
    {
        //Debug.Log("Enemy hit!");
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
