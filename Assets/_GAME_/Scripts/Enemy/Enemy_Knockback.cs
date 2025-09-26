using System.Collections;
using UnityEngine;

public class Enemy_Knockback : MonoBehaviour
{
    private Rigidbody2D enemyRB;
    private Enemy_Movement enemyMovement;
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<Enemy_Movement>();
    }

    public void Knockback(Transform player, float knockForce, float knockTime, float stunTime)
    {
        enemyMovement.ChangeState(EnemyState.Knockback);
        StartCoroutine(StunCounter(knockTime, stunTime));
        Vector2 direction = (transform.position - player.position).normalized;
        enemyRB.linearVelocity = direction * knockForce;
    }

    IEnumerator StunCounter(float knockTime, float stunTime)
    {
        yield return new WaitForSeconds(knockTime);
        enemyRB.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemyMovement.ChangeState(EnemyState.Idle);
    }
}
