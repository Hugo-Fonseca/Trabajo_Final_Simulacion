using UnityEngine;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public TMP_Text bestScoreText;    // Run Cat
    public TMP_Text shooterBestScoreText; // Shooter
    public TMP_Text scareCountText;

    void Start()
    {
        bestScoreText.text = "Mejor puntaje Run Cat: "
                             + GameManager.Instance.bestScore.ToString("F0");

        int shooterBest = PlayerPrefs.GetInt("HighScore", 0);
        shooterBestScoreText.text = "Mejor puntaje Shooter: " + shooterBest;

        if (SimulationManagerLifeCats.Instance != null)
            scareCountText.text = "Veces asustado: "
                                  + SimulationManagerLifeCats.Instance.scareCount;
    }
}
