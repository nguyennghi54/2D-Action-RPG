using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill", menuName = "SkillTree/Create new Skill")]
public class Skill : ScriptableObject
{
    public string skillName;
    public  int maxLevel;
    public  Sprite skillIcon;
    
}
