using _GAME_.Scripts.Player;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] PlayerPrefab player;
    void OnEnable()
    {
        SkillSlot.OnSkillPointSpent += HandleSkillPointSpent;
    }

    void OnDisable()
    {
        SkillSlot.OnSkillPointSpent -= HandleSkillPointSpent;
    }
    
/// <summary>
/// When SkillSlot.OnSkillPointSpent, 
/// update Player's HP
/// </summary>
/// <param name="slot"></param>
    private void HandleSkillPointSpent(SkillSlot slot)
    {
        string skillName = slot.skill.skillName;
        switch (skillName)
        {
            case "Max HP Boost":
                player.UpdateMaxHP(1);
                break;
            default:
                Debug.LogWarning($"Unknown skill: {skillName}");
                break;
        }
    }
}
