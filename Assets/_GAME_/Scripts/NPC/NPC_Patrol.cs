using System.Collections;
using UnityEngine;

public class NPC_Patrol : MonoBehaviour
{
    public Vector2[] patrolPoints;
    private int currentPatrolIndex;
    
    [SerializeField] private float speed;
    [SerializeField] private float pauseDuration;
    private bool isPaused;

    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 targetPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        StartCoroutine(SetPatrolPoint());
    }

    void Update()
    {
        if (isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 dir = ((Vector3) targetPos - transform.position).normalized;
        if (dir.x < 0 && transform.localScale.x > 0 || dir.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x*-1, transform.localScale.y,  transform.localScale.z);
        }
        rb.linearVelocity = dir * speed;
        // if get close to target, set new target
        if (Vector2.Distance(transform.position, targetPos) < .1f)
        {
            StartCoroutine(SetPatrolPoint());
        }
    }

    IEnumerator SetPatrolPoint()
    {
        isPaused = true;
        anim.Play("NPC_Idle");
        yield return new  WaitForSeconds(pauseDuration);
        isPaused = false;
        anim.Play("NPC_Run");
        currentPatrolIndex = (currentPatrolIndex+ 1) % patrolPoints.Length;  // if done traverse, loop back to begin
        targetPos = patrolPoints[currentPatrolIndex];
    }
}
