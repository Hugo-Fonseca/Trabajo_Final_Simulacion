using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    public GameObject menuUI;
    private bool paused = false;

    private void Awake()
    {
        Instance = this;
        ResumeGame();
    }

    void OnEnable()
    {
        InputManager.Instance.OnPause += TogglePause;
    }

    void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnPause -= TogglePause;
    }

    public void TogglePause()
    {
        if (paused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        paused = true;
        menuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        paused = false;
        menuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
