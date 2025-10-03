using UnityEngine;

public class ToggleSkillUI : MonoBehaviour
{
    private CanvasGroup skillUI;
    private bool skillOpen;
    void Start()
    {
        skillUI = GetComponent<CanvasGroup>();
        skillUI.alpha = 0;
    }

    void Update()
    {
        if (Input.GetButtonDown("ToggleSkillUI"))
        {
            if (skillOpen)
            {
                Time.timeScale = 1;
                skillUI.alpha = 0;
                skillUI.blocksRaycasts = false;
                skillOpen = false;
            }
            else
            {
                Time.timeScale = 0; // pause game
                skillUI.alpha = 1;
                skillUI.blocksRaycasts = true;
                skillOpen = true;
            }
        }
    }
    
}
