using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] public Skill skill;
    [SerializeField] public bool isUnlocked;
    [SerializeField] private TMP_Text skillText;
    [SerializeField] private Image skillIcon;
    public Button skillButton;
    private float fontSize;
    
    private int currentLevel = 0;
    [SerializeField] private List<SkillSlot> prequisiteSkillList;

    public static event Action<SkillSlot> OnSkillPointSpent;
    public static event Action<SkillSlot> OnSkillMaxed;

    void OnValidate()   
    {
        if(skill!=null && skillText.text != null)
        {
            UpdateUI();
        }
    }

    /// <summary>
    /// Invoke SkillTreeManager.UpdateSkillPoint() to subtract point
    /// when upgrade a skill
    /// </summary>
    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < skill.maxLevel)
        {
            currentLevel++;
            OnSkillPointSpent?.Invoke(this);    // decrease skill point
            if (currentLevel >= skill.maxLevel)
            {
                OnSkillMaxed?.Invoke(this); // unlock more skills
            }
            UpdateUI();
        }
    }
    /// <summary>
    /// If in prequisiteList,
    /// ignore if still locked / isn't maxed out (not met requirement). 
    /// Else unlock all
    /// </summary>
    /// <returns></returns>
    public bool CanUnlockSkill()
    {
        foreach (SkillSlot slot in prequisiteSkillList)
        {
            if (!slot.isUnlocked || slot.currentLevel < slot.skill.maxLevel)
            {
                return false;
            }
        }
        return true;
    }
    public void Unlock()
    {
        isUnlocked = true;
        UpdateUI();
    }
    private void UpdateUI()
    {
        skillIcon.sprite = skill.skillIcon;
        if (isUnlocked)
        {
            skillButton.interactable = true;
            skillIcon.color = Color.white;
            skillText.text = $"{currentLevel}/{skill.maxLevel}";
            skillText.fontSize = 37;
        }
        else
        {
            skillButton.interactable = false;
            skillIcon.color = Color.grey;
            skillText.text = "Locked";
            skillText.fontSize = 30;
        }
    }
}
