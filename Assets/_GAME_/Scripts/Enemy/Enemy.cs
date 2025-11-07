using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public AudioManager audioManager;
    public AudioSource audioSource;

    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
    }
}
