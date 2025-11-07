using System;
using _GAME_.Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{
    private int currentExp;
    private int expToNextLevel;
    [SerializeField] private float expGrowthMultiplier = 1.2f; // add 20% more EXP needed to each level 
    [SerializeField] private PlayerPrefab player;
    [Header("UI")]
    [SerializeField] private Slider expSlider;
    [SerializeField] private TMP_Text expText;
    private int initialExpToNextLevel;
    public static event Action<int> OnLevelUp;
    public void Start()
    {
        expToNextLevel = 10;
        initialExpToNextLevel = expToNextLevel;
        UpdateUI();
    }
    
    /// <summary>
    /// Subscribe event: When enemy killed,
    /// Player gain EXP
    /// </summary>
    void OnEnable()
    {
        Enemy_Health.OnEnemyDefeated += GainExperience;
    }

    void OnDisable()
    {
        Enemy_Health.OnEnemyDefeated -= GainExperience;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GainExperience(2);
        }
    }

    public void ResetLevel()
    {
        expSlider.maxValue = initialExpToNextLevel;
        expSlider.value = 0;
        expText.text = $"LEVEL: {player.level}";
    }
    public void GainExperience(int amount)
    {
        currentExp += amount;
        while (currentExp >= expToNextLevel)
        {
            LevelUp();
        }

        UpdateUI();
    }

    private void LevelUp()
    {
        player.audioManager.audioSource.PlayOneShot(player.audioManager.levelupSFX);
        player.level++;
        currentExp -= expToNextLevel;
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * expGrowthMultiplier);
        OnLevelUp?.Invoke(1);   // give 1 point every level up
    }

    public void UpdateUI()
    {
        expSlider.maxValue = expToNextLevel;
        expSlider.value = currentExp;
        expText.text = $"LEVEL: {player.level}";
    }
}
