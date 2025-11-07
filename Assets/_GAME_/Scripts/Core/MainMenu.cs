using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public AudioManager audioManager;
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        audioManager.PlayMenuBGM();
        SceneManager.sceneLoaded += OnSceneLoaded;
        startButton.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        audioManager.audioSource.Stop();
        audioManager.PlayGameBGM();
        SceneManager.LoadScene("Playground");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(scene);
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        startButton.onClick.RemoveListener(StartGame);
    }
}
