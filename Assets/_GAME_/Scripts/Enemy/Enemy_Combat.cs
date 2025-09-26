
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    [SerializeField] private int enemyDamage;
    [SerializeField] private float weaponRange; // range for enemy's ACTUAL weapon touch & damage
    [SerializeField] GameObject attackPoint;    // centerpoint for weaponRange's circle radius
    [SerializeField] private LayerMask playerLayer;
    [Header("KnockBack")]
    [SerializeField] private float knockbackForce;
    [SerializeField] private float stunTime;
    public void Attack()
    {
        // If Player still in weaponRange after entered  => attack
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.transform.position, weaponRange, playerLayer);
        if (hits.Length > 0)
        {
            hits[0].GetComponent<PlayerHealth>().ChangeHealth(-enemyDamage);
            hits[0].GetComponent<Player_Movement>().Knockback(transform, knockbackForce, stunTime);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position,weaponRange);
    }
}
