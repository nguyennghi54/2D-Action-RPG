using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [Header("UI")] 
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogText;
    public bool isDialogActive;
    public Button[] choiceButtons;
    
    private DialogueSO currentDialog;
    private int dialogIndex;

    void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        foreach (Button button in choiceButtons)
            button.gameObject.SetActive(false);
    }
    
    void Start()
    {
        ShowDialog();
    }

    public void StartDialog(DialogueSO dialogSO)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        currentDialog = dialogSO;
        isDialogActive = true;
        dialogIndex = 0;
        ShowDialog();
    }
    
    public void AdvanceDialog()
    {
        if (dialogIndex < currentDialog.dialogLines.Length)
        {
            ShowDialog();
        }
        else
        {
            ShowChoices();
        }
    }

    public void ShowChoices()
    {
        ClearChoices();
        if (currentDialog.dialogOptions.Length > 0)
        {
            for (int i = 0; i < currentDialog.dialogOptions.Length; i++)
            {
                var option = currentDialog.dialogOptions[i];
                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextDialog)); // (): onClick()
            }
        }
        else
        {
            choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "Bye.";
            choiceButtons[0].onClick.AddListener(EndDialog);
            choiceButtons[0].gameObject.SetActive(true);
        }
    }

    void ChooseOption(DialogueSO dialogSO)
    {
        if (dialogSO == null)
            EndDialog();
        else
        {
            ClearChoices();
            StartDialog(dialogSO);
        }
    }
    public void EndDialog()
    {
        dialogIndex = 0;
        isDialogActive = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        ClearChoices();
    }
    /// <summary>
    /// Show dialog on dialogUI, line by line
    /// </summary>
    void ShowDialog()
    {
        DialogLine line = currentDialog.dialogLines[dialogIndex];
        portrait.sprite = line.speaker.portrait;
        nameText.text = line.speaker.name;
        dialogText.text = line.text;
        dialogIndex++;

    }

    void ClearChoices()
    {
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }
}
