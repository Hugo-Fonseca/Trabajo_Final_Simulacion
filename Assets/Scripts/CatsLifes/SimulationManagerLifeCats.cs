using UnityEngine;

public class SimulationManagerLifeCats : MonoBehaviour
{
    public static SimulationManagerLifeCats Instance;

    public int scareCount = 0;   // Número de veces asustado

    private void Awake()
    {
        Instance = this;
        LoadData();
    }

    public void AddScare()
    {
        scareCount++;
        SaveData();

        // Actualizar HUD si existe
        if (InteractionUIManager.Instance != null)
            InteractionUIManager.Instance.UpdateScareCounter(scareCount);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Cats_ScareCount", scareCount);
    }

    public void LoadData()
    {
        scareCount = PlayerPrefs.GetInt("Cats_ScareCount", 0);
    }
}
