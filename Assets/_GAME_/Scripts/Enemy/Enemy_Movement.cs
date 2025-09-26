using System;
using UnityEngine;
using UnityEngine.XR;

public enum EnemyState
{
    Idle, Chasing, Attacking, Knockback
}
public class Enemy_Movement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float enemySpeed;
    private int facingDir; 
    
    [Header("PlayerDetect")]
    [SerializeField] float playerDetectRange; // range for enemy's Player detection
    [SerializeField] private GameObject detectPoint; 
    [SerializeField] LayerMask playerLayer;
    
    [Header("Combat")]
    [SerializeField] private float attackRange; // range for enemy's attack ATTEMPT 
    [SerializeField] float attackCD;
    private float attackCDtimer;
    
    private Rigidbody2D enemyRB;
    private GameObject player;
    private Animator enemyAnim;
    private EnemyState enemyState;
    
    private Vector2 playerPos, enemyPos; 
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();
        facingDir = (int) transform.localScale.x;
        ChangeState(EnemyState.Idle);
    }

    public void ChangeState(EnemyState newState)
    {
        enemyState = newState;
        // Update animation
        enemyAnim.SetBool("isIdle", enemyState == EnemyState.Idle);
        enemyAnim.SetBool("isChasing",  enemyState == EnemyState.Chasing);
        enemyAnim.SetBool("isAttacking", enemyState == EnemyState.Attacking);
    }

    void Update()
    {
        if (enemyState != EnemyState.Knockback)
        {
            CheckForPlayer();
            // Update CD
            if (attackCDtimer > 0)
            {
                attackCDtimer -= Time.deltaTime;
            }
            // Chase state 
            if (enemyState == EnemyState.Chasing)
            {
                ChasePlayer();
            }
            // Attack state
            else if (enemyState == EnemyState.Attacking)
            {
                enemyRB.linearVelocity = Vector2.zero;  // stop enemy to do attack
            }
        }
    }
    
    /// <summary>
    /// Detect Player in detectRange
    /// and Attempt to attack if in attackRange
    /// </summary>
    void CheckForPlayer() 
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectPoint.transform.position, playerDetectRange, playerLayer);
        // If Player in sight => Chase & Attack logic
        if (hits.Length > 0)
        {
            player = hits[0].gameObject;
            playerPos = player.transform.position;
            enemyPos = transform.position;
            // If Player gets into attack range & countdown over => attack
            if (Vector2.Distance(playerPos, enemyPos) <= attackRange &&
                attackCDtimer <= 0)
            {
                attackCDtimer = attackCD;
                ChangeState(EnemyState.Attacking);
            }
            // Elseif out of range & finish attack attempt => continue chase towards Player
            else if(Vector2.Distance(playerPos, enemyPos) > attackRange &&
                    enemyState != EnemyState.Attacking) // (attack's endframe: ChangeState->Idle)
                ChangeState(EnemyState.Chasing); 
        }
        // Else => Idle logic
        else
        {
            enemyRB.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }
    
    /// <summary>
    /// Flip Enemy according to Player 
    /// and Send Enemy towards Player
    /// </summary>
    void ChasePlayer()
    {
        // Flip logic
        if (playerPos.x < transform.position.x && facingDir == 1 ||
            playerPos.x > transform.position.x && facingDir == -1) // Player approach from behind
        {
            Flip();
        }
        // Chase logic
        Vector2 enemyDirection = (player.transform.position - transform.position).normalized;
        enemyRB.linearVelocity = enemyDirection * enemySpeed;
    }
    void Flip()
    {
        facingDir *= -1;
        transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectPoint.transform.position, playerDetectRange);
    }
}
