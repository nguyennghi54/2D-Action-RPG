using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Dialogue/Create new Dialogue")]
public class DialogueSO : ScriptableObject
{
    public DialogLine[] dialogLines;
    public DialogOption[] dialogOptions;
}

[System.Serializable]
public class DialogLine
{
    public ActorSO speaker;
    [TextArea(3,5)] public string text;
}

[System.Serializable]
public class DialogOption
{
    public string optionText;
    public DialogueSO nextDialog;
}