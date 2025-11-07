using System.Collections;
using System.Collections.Generic;
using _GAME_.Scripts.Player;
using UnityEngine;

    public class Player_Movement : MonoBehaviour
{
    [Header("Speed")]
    private float moveSpeed;
    private float currentSpeed;
    public float sprintSpeed;
    public float dashDuration;
    public float dashCD;
    private float dashTimer;
    private bool isDashing = false;
    private bool canDash = true;
    [SerializeField] private TrailRenderer trailRenderer;
    
    private float horizontalInput;
    private float verticalInput;
    
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movingDirection;
    private SpriteRenderer spriteRenderer;
    private Player_Combat playerCombat;
    
    private int facingDir;
    private bool isKnockBacked;

    private PlayerPrefab player;
    private Dictionary<UnitStat, float> statDict;
    void Start()
    {
        player = GetComponent<PlayerPrefab>();
        statDict = player.statDict;
        moveSpeed = statDict.GetValueOrDefault(UnitStat.MoveSpeed);
        currentSpeed = moveSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCombat = GetComponent<Player_Combat>();
        facingDir = (int) transform.localScale.x;
    }
    #region Update
    /// <summary>
    /// Detect movement input
    /// </summary>
    void FixedUpdate()
    {
        if (isDashing)
            return;
        if (!isKnockBacked)
        {
            horizontalInput =  Input.GetAxis("Horizontal");
            verticalInput =  Input.GetAxis("Vertical");
            movingDirection = new Vector2(horizontalInput, verticalInput).normalized;
            anim.SetFloat("horizontal", Mathf.Abs(horizontalInput));
            anim.SetFloat("vertical", Mathf.Abs(verticalInput));
            // Flip facing direction
            if (horizontalInput > 0 && transform.localScale.x < 0
                || horizontalInput < 0 && transform.localScale.x > 0)
                Flip();
            rb.linearVelocity = new Vector2(horizontalInput, verticalInput).normalized * moveSpeed;
        }
    }
    
    /// <summary>
    /// Detect attack input
    /// </summary>
    void Update()
    {
        if (Input.GetButtonDown("Slash"))
        {
            playerCombat.Attack();
        }

        if (Input.GetButtonDown("Sprint") && canDash)
        {
            StartCoroutine(Sprint());
        }
    }
    #endregion Update

    IEnumerator Sprint()
    {
        canDash = false;
        isDashing = true;
        rb.linearVelocity = new Vector2(movingDirection.x * sprintSpeed, movingDirection.y * sprintSpeed);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        trailRenderer.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }
    
    void Flip()
    {
        facingDir *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, 
            transform.localScale.y, transform.localScale.z);
    }
    
    #region KnockBack
    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockBacked = true;
        Vector2 direction = (transform.position - enemy.position).normalized;
        rb.linearVelocity = direction * force;
        StartCoroutine(KnockbackTimer(stunTime));
    }
    IEnumerator KnockbackTimer(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        isKnockBacked = false;
    }
    #endregion KnockBack
}


