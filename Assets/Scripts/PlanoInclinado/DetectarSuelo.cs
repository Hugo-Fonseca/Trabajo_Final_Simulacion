using UnityEngine;

public class DetectarSuelo2D : MonoBehaviour
{
    public PlanoInclinado2DManager manager;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Objeto"))
        {
            // Destruir el objeto al tocar el suelo
            Destroy(collision.gameObject);

            // Reiniciar simulación
            if (manager != null)
                manager.DetectarSuelo();
        }
    }
}
