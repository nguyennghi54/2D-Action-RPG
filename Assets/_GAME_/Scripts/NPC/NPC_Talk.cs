using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private Animator speechAnim;
    [SerializeField] private DialogueSO dialogSO;
    private bool playerInRange;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }
    
    /// <summary>
    /// Press E to advance dialog
    /// </summary>
    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                // If a dialog already in playing
                if (DialogueManager.Instance.isDialogActive)
                {
                    DialogueManager.Instance.AdvanceDialog();
                }
                // If no dialog yet, play this NPC's designated dialogue
                else
                {
                    DialogueManager.Instance.StartDialog(dialogSO);
                }
            }
        }
    }
    void OnEnable()
    {
        rb.linearVelocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;  // prevent being pushed
        anim.Play("Idle");
        speechAnim.Play("Open");
    }

    void OnDisable()
    {
        rb.bodyType = RigidbodyType2D.Dynamic; 
        speechAnim.Play("Close");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
