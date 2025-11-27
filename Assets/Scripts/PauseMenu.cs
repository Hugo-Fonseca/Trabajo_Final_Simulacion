using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuUI;

    private bool paused = false;
    private bool inputLocked = false;

    void OnEnable()
    {
        InputManager.Instance.OnPause += HandlePauseInput;
    }

    void OnDisable()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnPause -= HandlePauseInput;
    }

    private void HandlePauseInput()
    {
        if (inputLocked) return;
        StartCoroutine(InputCooldown());

        if (!paused)
            PauseGame();
        else
            ResumeGame();
    }

    void PauseGame()
    {
        paused = true;
        menuUI.SetActive(true);
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        paused = false;
        menuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnContinue()
    {
        ResumeGame(); 
    }

    public void OnExit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    private System.Collections.IEnumerator InputCooldown()
    {
        inputLocked = true;
        yield return new WaitForSecondsRealtime(0.25f);
        inputLocked = false;
    }
}
