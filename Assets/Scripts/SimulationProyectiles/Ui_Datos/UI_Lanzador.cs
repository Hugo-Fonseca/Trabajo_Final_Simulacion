using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Lanzador : MonoBehaviour
{
    public Slider sliderAngulo;
    public Slider sliderFuerza;

    public TextMeshProUGUI textoAngulo;
    public TextMeshProUGUI textoFuerza;
    public TextMeshProUGUI textoDistancia;

    void Update()
    {
        textoAngulo.text = "Ángulo: " + sliderAngulo.value.ToString("0") + "°";
        textoFuerza.text = "Fuerza: " + sliderFuerza.value.ToString("0");
    }

    public void ActualizarDistancia(float d)
    {
        textoDistancia.text = "Distancia: " + d.ToString("0.0") + " m";
    }
}
