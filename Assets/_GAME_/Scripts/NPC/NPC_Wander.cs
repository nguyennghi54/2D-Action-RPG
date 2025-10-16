using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

public class NPC_Wander : MonoBehaviour
{
    [Header("Wander Area")] 
    [SerializeField] private float wanderWidth;
    [SerializeField] private float wanderHeight;
    private Vector2 startPos;

    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float pauseDuration;
    private Animator anim;
    private Vector2 target;
    private bool isPaused;

    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        startPos = transform.position;
    }

    void OnEnable()
    {
        target = GetRandomTarget();
    }
    
    // Generate random target point
    private Vector2 GetRandomTarget()
    {
        float halfWidth =  wanderWidth/2;
        float halfHeight =  wanderHeight/2;
        int edge = Random.Range(0, 2);
        return edge switch
        {
            // get edgemost points of rectangle area
            0 => new Vector2(startPos.x - halfWidth,
                Random.Range(startPos.y + halfHeight, startPos.y - halfHeight)), // left
            1 => new Vector2(startPos.x + halfWidth,
                Random.Range(startPos.y + halfHeight, startPos.y - halfHeight)), // right
            2 => new Vector2(Random.Range(startPos.x + halfWidth, startPos.x - halfWidth),
                startPos.y - halfWidth), // bottom
            3 => new Vector2(Random.Range(startPos.x + halfWidth, startPos.x - halfWidth),
                startPos.y + halfWidth) // top
        };

    }

    IEnumerator PauseToPickTarget()
    {
        isPaused = true;
        anim.Play("NPC_Idle");
        yield return new WaitForSeconds(pauseDuration);
        target =  GetRandomTarget();
        isPaused = false;
        anim.Play("NPC_Run");
        
    }
    void Update()
    {
        if (isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        // Pick new target when approach old one
        if (Vector2.Distance(transform.position, target) < .1f)
        {
            StartCoroutine(PauseToPickTarget());
        }

        Move();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(MoveIfPausedTooLong());
    }

    IEnumerator MoveIfPausedTooLong()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(PauseToPickTarget());
    }
    void Move()
    {
        Vector2 dir = (target - (Vector2) transform.position).normalized;
        rb.velocity = dir * speed;
        //Flip
        if (dir.x < 0 && transform.localScale.x > 0 || dir.x > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x*-1,  transform.localScale.y, transform.localScale.z);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startPos, new Vector3(wanderWidth, wanderHeight, 0));
    }
}
