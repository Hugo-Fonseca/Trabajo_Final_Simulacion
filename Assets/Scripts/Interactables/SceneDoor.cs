using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoor : MonoBehaviour, IInteractable
{
    [Header("Nombre de la escena a cargar")]
    public string sceneName;

    public string GetInteractText()
    {
        return "Entrar a la simulación";
    }

    public void Interact(GameObject player)
    {
        SceneManager.LoadScene(sceneName);
    }
}
