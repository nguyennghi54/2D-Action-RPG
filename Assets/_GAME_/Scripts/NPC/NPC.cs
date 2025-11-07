using Unity.VisualScripting;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public enum NPCState
    {
        Default, Idle, Patrol, Wander, Talk
    }

    public NPCState currentState = NPCState.Patrol;
    private NPCState defaultState;
    
    public NPC_Talk talk;
    public NPC_Patrol patrol;
    public NPC_Wander wander;
    [SerializeField] public AudioManager audioManager;
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        defaultState = currentState;
        SwitchState(currentState);
    }

    public void SwitchState(NPCState newState)
    {
        currentState = newState;
        if(patrol!=null)
            patrol.enabled = newState == NPCState.Patrol;
        if(wander!=null)
            wander.enabled = newState == NPCState.Wander;
        if(talk!=null)
            talk.enabled = newState == NPCState.Talk;
    }
    
    /// <summary>
    /// If Player comes in range, state = Talk
    /// </summary>
    /// <param name="collider"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SwitchState(NPCState.Talk);
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SwitchState(defaultState);
        }
    }
}
