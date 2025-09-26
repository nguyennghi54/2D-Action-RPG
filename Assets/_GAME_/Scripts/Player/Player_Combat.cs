using _GAME_.Scripts.Player;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    private Animator anim;
    private PlayerPrefab player;
    private int attackDamage;
    private float attackCD;
    private float attackCDtimer;
    [Header("EnemyDetect")]
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private LayerMask enemyLayer;
    private float weaponRange;
    [Header("Knockback")]
    private float knockForce;
    private float knockTime;
    private float stunTime;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<PlayerPrefab>();
        Init();
    }

    void Init()
    {
        attackDamage = player.attack;
        attackCD = player.attackCD;
        weaponRange = player.weaponRange;
        knockForce = player.knockForce;
        knockTime = player.knockTime;
        stunTime = player.stunTime;
    }
    void Update()
    {
        if (attackCDtimer > 0)
        {
            attackCDtimer -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (attackCDtimer <= 0)
        {
            anim.SetBool("isAttacking", true);
            attackCDtimer = attackCD;
        }
    }

    public void DealDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, weaponRange, enemyLayer);
        if (enemies.Length > 0)
        {
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<Enemy_Health>().ChangeHealth(-attackDamage);
                enemy.GetComponent<Enemy_Knockback>().Knockback(transform,knockForce,knockTime,stunTime);
            }
        }
    }
    public void FinishAttack()
    {
        anim.SetBool("isAttacking", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.transform.position, weaponRange);
    }
}
