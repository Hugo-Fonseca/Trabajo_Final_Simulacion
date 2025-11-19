using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lanzador : MonoBehaviour
{
    [Header("Referencias")]
    public Transform puntoDisparo;
    public GameObject proyectilPrefab;

    [Header("UI")]
    public Slider sliderAngulo;
    public Slider sliderFuerza;
    public TMP_Text textoAngulo;
    public TMP_Text textoFuerza;
    public TMP_Text textoDistancia;
    public TMP_Text textoVelocidad;

    private float angulo;
    private CatNPC2D gato;

    void Start()
    {
        gato = FindObjectOfType<CatNPC2D>();
    }

    void Update()
    {
        // Actualizar ángulo
        angulo = sliderAngulo.value;
        puntoDisparo.rotation = Quaternion.Euler(0, 0, angulo);

        if (textoAngulo)
            textoAngulo.text = "Ángulo: " + angulo.ToString("F0") + "°";

        if (textoFuerza)
            textoFuerza.text = "Fuerza: " + sliderFuerza.value.ToString("F1");
    }

    public void Lanzar()
    {
        // No permitir disparo si el gato aún está ocupado
        if (gato != null && gato.IsBusy())
        {
            Debug.Log("⛔ Espera a que el gato regrese.");
            return;
        }

        // Crear pelota
        GameObject bala = Instantiate(
            proyectilPrefab,
            puntoDisparo.position,
            puntoDisparo.rotation
        );

        Rigidbody2D rb = bala.GetComponent<Rigidbody2D>();
        rb.linearVelocity = puntoDisparo.right * sliderFuerza.value;

        // Agregar script Proyectil
        Proyectil p = bala.AddComponent<Proyectil>();
        p.textoDistancia = textoDistancia;
        p.textoVelocidad = textoVelocidad;
        p.origen = puntoDisparo.position;

        // Cámara sigue la pelota
        BallCamera2D cam = Camera.main.GetComponent<BallCamera2D>();
        if (cam != null)
            cam.SetTarget(bala.transform);

        // Gato persigue pelota
        gato.SetBall(bala.transform);
    }
}
