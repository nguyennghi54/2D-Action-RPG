using System.Collections.Generic;
using _GAME_.Scripts.Player;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    private Animator anim;
    private PlayerPrefab player;
    private float attackDamage;
    private float attackCD;
    private float attackCDtimer;
    private Dictionary<UnitStat, float> statDict;
    
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
        statDict = player.statDict;
        Init();
    }

    void Init()
    {
        attackDamage = statDict.GetValueOrDefault(UnitStat.Attack);
        attackCD = statDict.GetValueOrDefault(UnitStat.AttackCD);
        weaponRange = statDict.GetValueOrDefault(UnitStat.WeaponRange);
        knockForce = statDict.GetValueOrDefault(UnitStat.KnockForce);
        knockTime = statDict.GetValueOrDefault(UnitStat.KnockTime);
        stunTime = statDict.GetValueOrDefault(UnitStat.StunTime);
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
