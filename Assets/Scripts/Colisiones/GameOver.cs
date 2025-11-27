using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel;
    private bool saved = false;

    void Update()
    {
        if (!saved && GameObject.FindGameObjectWithTag("Player") == null)
        {
            saved = true;
            gameOverPanel.SetActive(true);

            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();

            if (scoreManager != null)
            {
                int finalScore = (int)scoreManager.CurrentScore;

                GameManager.Instance.SaveBestScore(finalScore);

                Debug.Log("Puntaje guardado: " + finalScore);
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

