using UnityEngine;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    public TMP_Text bestScoreText;

    void Start()
    {
        bestScoreText.text = "Mejor puntaje Run Cat: " + GameManager.Instance.bestScore.ToString("F0");
    }
}
