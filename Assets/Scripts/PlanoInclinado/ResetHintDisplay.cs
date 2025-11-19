using UnityEngine;
using TMPro;

public class ResetHintDisplay : MonoBehaviour
{
    public TMP_Text hintText;

    void Update()
    {
        if (hintText == null || InputManager.Instance == null)
            return;

        // Intentamos obtener el nombre del botón asignado
        string name = InputManager.Instance.GetKeyName("Player/Reset");

        // Intentamos obtener un icono adecuado (si existe)
        var icon = InputManager.Instance.GetKeyIcon("Player/Reset");

        // --- Mostrar icono si existe ---
        if (icon != null)
        {
            // Asumiendo que en tu UI usas <sprite> para mostrar iconos
            hintText.text = $"<sprite name=\"{icon.name}\"> Reiniciar";
        }
        else
        {
            // Sino, mostramos solo el nombre del botón
            hintText.text = $"Presiona [{name}] para reiniciar";
        }
    }
}
