using System.Collections.Generic;
using _GAME_.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameObject Instance;
    [Header("Gameover")]
    public CanvasGroup gameoverUI;
    public Button gameoverButton;
    [Header("Game clear")]
    public CanvasGroup gameclearUI;
    public Button gameclearButton;
    
    public GameObject[] persistentList;
    [Header("Reset")] [SerializeField] private PlayerPrefab player;
    [SerializeField] private InventoryManager invManager;
    [SerializeField] private SkillTreeManager skillTreeManager;
    public AudioManager audioManager;

    public Enemy[] enemyList;
    private int enemyCount;
    
    void Awake()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        Screen.fullScreenMode = FullScreenMode.Windowed;
        gameoverButton.onClick.AddListener(ToMainMenu);
        gameclearButton.onClick.AddListener(ToMainMenu);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        if(Instance!= null)
        {
            CleanUpAndDestroy();
            return;
        }
        else
        {
            Instance = this.gameObject;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObject();
        }
        
    }

    void OnEnable()
    {
        Enemy_Health.OnEnemyDefeated += CountEnemyDefeated;
    }

    void OnDisable()
    {
        Enemy_Health.OnEnemyDefeated -= CountEnemyDefeated;
    }
    public void ResetScene()
    {
        player.ResetStat();
        player.playerHealth.UpdateHealthUI();
        invManager.ClearInventory();
        player.UpdateCurrentHP((int) player.statDict.GetValueOrDefault(UnitStat.MaxHP));
        player.transform.position = player.spawnPos;
        player.level = 0;
        player.expManager.ResetLevel();
        enemyCount = 0;
        skillTreeManager.UpdateSkillPoint(skillTreeManager.initialPoints);
    }
    
    public void EnableGameOverUI(bool enable)
    {
        audioManager.audioSource.Stop();
        audioManager.audioSource.PlayOneShot(audioManager.gameoverSFX);
        AudioSource.PlayClipAtPoint(audioManager.gameoverSFX, transform.position);
        Time.timeScale = enable ? 0 : 1;
        gameoverUI.alpha = enable ? 1 : 0;
        gameoverUI.blocksRaycasts = enable ? true : false;
        gameoverUI.interactable = enable ? true : false;
    }

    public void CountEnemyDefeated(int expReward)
    {
        enemyCount++;
        if (enemyCount == enemyList.Length)
        {
            EnableGameClearUI(true);
        }
    }
    
    public void EnableGameClearUI(bool enable)
    {
        audioManager.audioSource.Stop();
        audioManager.audioSource.PlayOneShot(audioManager.winSFX);
        AudioSource.PlayClipAtPoint(audioManager.winSFX, transform.position);
        Time.timeScale = enable ? 0 : 1;
        gameclearUI.alpha = enable ? 1 : 0;
        gameclearUI.blocksRaycasts = enable ? true : false;
        gameclearUI.interactable = enable ? true : false;
    }
    
    public void ToMainMenu()
    {
        EnableGameOverUI(false);
        EnableGameClearUI(false);
        SceneManager.LoadScene("MainMenu");
        ResetScene();
        audioManager.audioSource.Stop();
    }
    void MarkPersistentObject()
    {
        foreach (GameObject obj in persistentList)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
        audioManager.audioSource.Play();
    }
    //destroy previous's scene's objects
    void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentList)
        {
            Destroy(obj);
        }
        Destroy(gameObject);
    }
}
