using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ActorSO", menuName = "Dialogue/Create new Actor")]
public class ActorSO : ScriptableObject
{
    public string actorName;
    public Sprite portrait;
}
