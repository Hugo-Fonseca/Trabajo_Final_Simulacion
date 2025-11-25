using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnimalUI : MonoBehaviour
{
    public Animal animal;
    public TMP_Text stateText;
    public TMP_Text energyText;     

    void Start()
    {
        if (animal != null)
        {
            animal.OnStateChanged += UpdateStateUI;
            animal.OnEnergyChanged += UpdateEnergyUI;
        }

        // Mostrar valores iniciales
        UpdateStateUI(animal.currentState);
        UpdateEnergyUI(animal.energia);
    }

    void UpdateStateUI(AnimalState newState)
    {
        if (stateText != null)
            stateText.text = "Estado: " + newState;
    }

    void UpdateEnergyUI(float energiaActual)
    {
        if (energyText != null)
            energyText.text = "Energía: " + Mathf.RoundToInt(energiaActual);
    }
}
