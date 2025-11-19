using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlanoInclinado2DManager : MonoBehaviour
{
    [Header("Prefabs y Objetos")]
    public GameObject prefabObjeto;
    public Transform rampa;
    public Transform seguidor;
    public Transform spawnPoint;

    [Header("UI")]
    public TMP_Text anguloText;
    public TMP_Text tiempoText;
    public TMP_Text velocidadText;

    [Header("Configuración Física")]
    public PhysicsMaterial2D materialObjeto;
    [Range(-45f, 45f)] public float angulo = 0f;
    [Range(0f, 1f)] public float friccion = 0f;

    [Header("Seguimiento")]
    public float alturaSeguidor = -3f;
    public float suavizadoSeguidor = 5f;

    [Header("Sprite del Seguidor")]
    public Transform seguidorVisual;   // 👈 Aquí irá tu personaje animado

    private Rigidbody2D objetoActual;
    private float tiempo;
    private Vector3 startPosition;
    public TMP_Text resetHintText;


    void Start()
    {
        startPosition = spawnPoint.position;
        CrearNuevoObjeto();
    }

    void Update()
    {
        if (objetoActual == null)
            return;

        // --- UI ---
        anguloText.text = $"Ángulo: {angulo:F1}°";
        tiempoText.text = $"Tiempo: {tiempo:F2} s";
        velocidadText.text = $"Velocidad: {objetoActual.linearVelocity.magnitude:F2} m/s";

        // Rampa
        rampa.rotation = Quaternion.Euler(0, 0, angulo);

        // Fricción
        if (materialObjeto != null)
            materialObjeto.friction = friccion;

        // Timer
        if (objetoActual.linearVelocity.magnitude > 0.01f)
            tiempo += Time.deltaTime;

        // --- Movimiento del seguidor ---
        if (seguidor != null)
        {
            Vector3 destino = new Vector3(
                objetoActual.position.x,
                alturaSeguidor,
                seguidor.position.z
            );

            seguidor.position = Vector3.Lerp(
                seguidor.position,
                destino,
                suavizadoSeguidor * Time.deltaTime
            );
        }

        // --- Voltear sprite del seguidor ---
        if (seguidorVisual != null)
        {
            if (objetoActual.position.x > seguidor.position.x)
                seguidorVisual.localScale = new Vector3(1, 1, 1);
            else
                seguidorVisual.localScale = new Vector3(-1, 1, 1);
        }

    }

    public void CrearNuevoObjeto()
    {
        if (objetoActual != null)
            Destroy(objetoActual.gameObject);

        GameObject nuevo = Instantiate(prefabObjeto, startPosition, Quaternion.identity);
        objetoActual = nuevo.GetComponent<Rigidbody2D>();

        Collider2D col = objetoActual.GetComponent<Collider2D>();
        if (col != null && materialObjeto != null)
            col.sharedMaterial = materialObjeto;

        tiempo = 0;
    }

    public void ResetSimulation() => CrearNuevoObjeto();

    public void SetAngulo(float valor) => angulo = valor;
    public void SetFriccion(float valor) => friccion = valor;

    public void DetectarSuelo() => CrearNuevoObjeto();
}
