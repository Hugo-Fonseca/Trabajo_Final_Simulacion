using UnityEngine;
using TMPro;

public class Proyectil : MonoBehaviour
{
    public TMP_Text textoDistancia;
    public TMP_Text textoVelocidad;
    public Vector2 origen;

    public AudioSource audioDisparo;   // NUEVO

    private bool contado = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // reproducir sonido cuando se instancia / lanza
        if (audioDisparo != null)
            audioDisparo.Play();
    }

    void Update()
    {
        if (rb != null && textoVelocidad != null)
        {
            float velocidad = rb.linearVelocity.magnitude;
            textoVelocidad.text = "Vel: " + velocidad.ToString("F2") + " m/s";
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (contado) return;
        contado = true;

        float distancia = Vector2.Distance(origen, transform.position);

        if (textoDistancia != null)
            textoDistancia.text = "Distancia: " + distancia.ToString("F2") + " m";

        BallCamera2D cam = Camera.main.GetComponent<BallCamera2D>();
        if (cam != null)
            cam.SetTarget(transform);

        CatNPC2D gato = FindObjectOfType<CatNPC2D>();
        if (gato != null)
            gato.SetBall(transform);
    }
}
