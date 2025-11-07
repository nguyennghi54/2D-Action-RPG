using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static GameObject Instance;
    [Header("BGM")]
    public AudioClip menuBGM;
    public AudioClip gameBGM;
    [Header("SFX")]
    [SerializeField] public AudioClip buttonSFX;
    [HideInInspector] public AudioSource audioSource;
    [SerializeField] public AudioClip slashSFX;
    [SerializeField] public AudioClip clubSFX;
    [SerializeField] public AudioClip buySFX;
    [SerializeField] public AudioClip interactSFX;
    [SerializeField] public AudioClip eatSFX;
    [SerializeField] public AudioClip lootSFX;
    [SerializeField] public AudioClip levelupSFX;
    [SerializeField] public AudioClip deathSFX;
    [SerializeField] public AudioClip gameoverSFX;
    [SerializeField] public AudioClip winSFX;

    void Awake()
    {
        if(Instance!= null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        Button[] buttonList = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (Button button in buttonList)
        {
            button.onClick.AddListener(PlayButtonSFX);
        }
    }

    public void PlayMenuBGM()
    {
        audioSource.clip = menuBGM;
        audioSource.Play();
    }

    public void PlayGameBGM()
    {
        audioSource.clip = gameBGM;
        audioSource.Play();
    }
    void PlayButtonSFX()
    {
        audioSource.PlayOneShot(buttonSFX);       
    }
}
