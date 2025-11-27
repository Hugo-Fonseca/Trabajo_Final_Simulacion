using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;

    public GameObject menuUI;
    public AudioSource musicSource;

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

        if (musicSource)
            musicSource.Pause();
    }

    public void ResumeGame()
    {
        paused = false;
        menuUI.SetActive(false);
        Time.timeScale = 1;

        if (musicSource)
            musicSource.UnPause();
    }

    public void OnExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        Debug.Log("Cierre simulado en editor");
#endif
    }
}
