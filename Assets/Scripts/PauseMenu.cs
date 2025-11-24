using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuUI;

    private bool paused = false;

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
        paused = !paused;

        menuUI.SetActive(paused);

        Time.timeScale = paused ? 0 : 1;
    }

    public void OnContinue()
    {
        TogglePause();
    }

    public void OnExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
