using UnityEngine;

public class SeguirObjeto2D : MonoBehaviour
{
    public Transform objetivo;       
    public float suavizado = 5f;     
    public float alturaFija = -3f;   

    void LateUpdate()
    {
        if (objetivo != null)
        {
            Vector3 posicionDeseada = new Vector3(objetivo.position.x, alturaFija, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, posicionDeseada, suavizado * Time.deltaTime);
        }
    }
}

