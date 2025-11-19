using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEscape : MonoBehaviour
{
    [Header("Nombre de la escena principal del mapa")]
    public string mainSceneName = "SampleScene"; // <-- tu escena

    void Start()
    {
        // Nos suscribimos al evento Escape del InputManager
        InputManager.Instance.OnEscape += HandleEscape;
    }

    void HandleEscape()
    {
        Debug.Log("ESCAPE DETECTADO - REGRESANDO A " + mainSceneName);
        SceneManager.LoadScene(mainSceneName);
    }
}
