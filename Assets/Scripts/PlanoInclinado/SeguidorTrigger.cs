using UnityEngine;

public class SeguidorTrigger : MonoBehaviour
{
    public PlanoInclinado2DManager manager;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Objeto"))
        {
            // Reiniciar simulación
            if (manager != null)
                manager.DetectarSuelo();

            // Destruir el objeto
            Destroy(collision.gameObject);
        }
    }
}
