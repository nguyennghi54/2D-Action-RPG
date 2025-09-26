using System.Collections;
using _GAME_.Scripts.Player;
using UnityEngine;

    public class Player_Movement : MonoBehaviour
{
    private float moveSpeed;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movingDirection;
    private SpriteRenderer spriteRenderer;
    private Player_Combat playerCombat;
    
    private int facingDir;
    private bool isKnockBacked;

    private PlayerPrefab player;
    void Start()
    {
        player = GetComponent<PlayerPrefab>();
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
        if (!isKnockBacked)
        {
            var horizontalInput =  Input.GetAxis("Horizontal");
            var verticalInput =  Input.GetAxis("Vertical");
            anim.SetFloat("horizontal", Mathf.Abs(horizontalInput));
            anim.SetFloat("vertical", Mathf.Abs(verticalInput));
            // Flip facing direction
            if (horizontalInput > 0 && transform.localScale.x < 0
                || horizontalInput < 0 && transform.localScale.x > 0)
                Flip();
            rb.linearVelocity = new Vector2(horizontalInput, verticalInput).normalized * player.moveSpeed;
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
    }
    #endregion Update
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


