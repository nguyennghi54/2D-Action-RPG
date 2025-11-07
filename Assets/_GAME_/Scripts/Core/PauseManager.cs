using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class PauseManager : MonoBehaviour
{
    public GameManager gameManager;
    public CanvasGroup pauseUI;
    private bool pauseOpen;
    public Button resumeButton;
    public Button resetButton;
    public Button quitButton;
    public Button menuButton;
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        resumeButton.onClick.AddListener(ResumeGame);
        resetButton.onClick.AddListener(ResetGame);
        quitButton.onClick.AddListener(QuitGame);
        menuButton.onClick.AddListener(ReturnMenu);
    }
    

    void PauseGame(bool pauseOpen)
    {
        Time.timeScale = pauseOpen ? 1 : 0;
        pauseUI.alpha = pauseOpen ? 0 : 1;
        pauseUI.interactable = pauseOpen ? false : true;
        pauseUI.blocksRaycasts = pauseOpen ? false : true;
    }
    void Update()
    {
        if (Input.GetButtonDown("TogglePauseUI"))
        {
            if (pauseOpen)
            {
                PauseGame(false);
                pauseOpen = false;
            }
            else
            {
                PauseGame(true);
                pauseOpen = true;
            }
        }
    }
    public void ResumeGame()
    {
        PauseGame(true);
        pauseOpen = false;
    }

    public void ResetGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        gameManager.ResetScene();
        ResumeGame();
        
    }
    public void ReturnMenu()
    {
        gameManager.ToMainMenu();
        PauseGame(true);
    }
    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

   
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        resumeButton.onClick.RemoveListener(ResumeGame);
        resetButton.onClick.RemoveListener(ResetGame);
        quitButton.onClick.RemoveListener(QuitGame);
    }
}
