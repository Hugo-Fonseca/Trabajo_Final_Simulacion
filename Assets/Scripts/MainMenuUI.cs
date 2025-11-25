using UnityEngine;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public TMP_Text bestScoreText;
    public TMP_Text scareCountText;

    void Start()
    {
        // Puntaje del juego Run Cat
        bestScoreText.text = "Mejor puntaje Run Cat: "
                             + GameManager.Instance.bestScore.ToString("F0");

        // Contador de sustos del simulador de gatos
        if (SimulationManagerLifeCats.Instance != null)
            scareCountText.text = "Veces asustado: "
                                  + SimulationManagerLifeCats.Instance.scareCount;
    }
}
