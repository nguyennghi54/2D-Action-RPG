using System.Collections.Generic;
using _GAME_.Scripts.Player;
using TMPro;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject[] statSlotList;
    [SerializeField] private PlayerPrefab player;
    [SerializeField] private CanvasGroup statUI;
    private Dictionary<UnitStat, float> statDict;
    private bool statOpen;
    void Start()
    {
        statDict = player.statDict;
        UpdateAllStatsUI();
        statUI.alpha = 0;
    }

    void Update()
    {
        UpdateAllStatsUI();
        if (Input.GetButtonDown("ToggleStatUI"))
        {
            if (statOpen)
            {
                Time.timeScale = 1;
                statUI.alpha = 0;
                statOpen = false;
            }
            else
            {
                Time.timeScale = 0; // pause game
                statUI.alpha = 1;
                statOpen = true;
            }
        }
    }
    public void UpdateHPUI()
    {
        statSlotList[0].GetComponentInChildren<TMP_Text>().text =
            $"MaxHP: {statDict.GetValueOrDefault(UnitStat.MaxHP)}";

    }

    public void UpdateAttackUI()
    {
        statSlotList[1].GetComponentInChildren<TMP_Text>().text = 
            $"Attack: {statDict.GetValueOrDefault(UnitStat.Attack)}";
    }

    public void UpdateSpeedUI()
    {
        statSlotList[2].GetComponentInChildren<TMP_Text>().text = 
            $"Speed: {statDict.GetValueOrDefault(UnitStat.MoveSpeed)}";
    }

    public void UpdateAllStatsUI()
    {
        UpdateHPUI();
        UpdateAttackUI();
        UpdateSpeedUI();
    }
}
