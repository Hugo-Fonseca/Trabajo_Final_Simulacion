using UnityEngine;

public class SeguirObjeto2D : MonoBehaviour
{
    public Transform objetivo;       // El objeto que queremos seguir
    public float suavizado = 5f;     // Velocidad de seguimiento
    public float alturaFija = -3f;   // Altura Y del seguidor (sobre el suelo)

    void LateUpdate()
    {
        if (objetivo != null)
        {
            // Solo sigue horizontalmente, altura fija
            Vector3 posicionDeseada = new Vector3(objetivo.position.x, alturaFija, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
        }
    }
}

