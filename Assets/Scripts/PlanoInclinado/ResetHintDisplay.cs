using UnityEngine;
using TMPro;

public class ResetHintDisplay : MonoBehaviour
{
    public TMP_Text hintText;

    void Update()
    {
        if (hintText == null || InputManager.Instance == null)
            return;

        string name = InputManager.Instance.GetKeyName("Player/Reset");

        var icon = InputManager.Instance.GetKeyIcon("Player/Reset");

        if (icon != null)
        {
            hintText.text = $"<sprite name=\"{icon.name}\"> Reiniciar";
        }
        else
        {
            hintText.text = $"Presiona [{name}] para reiniciar";
        }
    }
}
