using TMPro;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    [SerializeField] private SkillSlot[] slotList;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private int availPoints;
    
    void OnEnable()
    {
        SkillSlot.OnSkillPointSpent += HandleSkillPointSpent;
        SkillSlot.OnSkillMaxed += HandleSkillMaxed;
        ExpManager.OnLevelUp += UpdateSkillPoint;
    }
    void OnDisable()
    {
        SkillSlot.OnSkillPointSpent -= HandleSkillPointSpent;
        SkillSlot.OnSkillMaxed -= HandleSkillMaxed;
        ExpManager.OnLevelUp -= UpdateSkillPoint;
    }
    
    void Start()
    {
        foreach (var slot in slotList)
        {
            slot.skillButton.onClick.AddListener(() => CheckAvailPoints(slot));
        }
        UpdateSkillPoint(0);
    }
    
    /// <summary>
    /// If skill points still available,
    /// upgrade skill
    /// </summary>
    /// <param name="slot"></param>
    void CheckAvailPoints(SkillSlot slot)
    {
        if (availPoints > 0)
        {
            slot.TryUpgradeSkill();
        }
    }
    public void UpdateSkillPoint(int amount)
    {
        availPoints += amount;
        pointText.text = $"Point: {availPoints}";
    }
    
    /// <summary>
    /// If subscriber SkillSlot.TryUpgradeSkill() called,
    /// subtract available skill points
    /// </summary>
    /// <param name="slot"></param>
    private void HandleSkillPointSpent(SkillSlot skillSlot)
    {
        if (availPoints > 0)
        {
            UpdateSkillPoint(-1);
        }
    }

    private void HandleSkillMaxed(SkillSlot skillSlot)
    {
        foreach (SkillSlot slot in slotList)
        {
            if (!slot.isUnlocked && slot.CanUnlockSkill())
            {
                slot.Unlock();
            }
            
        }
    }
}
