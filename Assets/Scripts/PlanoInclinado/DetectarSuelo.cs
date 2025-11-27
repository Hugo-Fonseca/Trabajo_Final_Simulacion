using UnityEngine;

public class DetectarSuelo2D : MonoBehaviour
{
    public PlanoInclinado2DManager manager;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Objeto"))
        {
            Destroy(collision.gameObject);

            if (manager != null)
                manager.DetectarSuelo();
        }
    }
}
