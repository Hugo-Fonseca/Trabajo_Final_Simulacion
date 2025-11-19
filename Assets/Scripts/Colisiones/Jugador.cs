using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    public float PlayerSpeed = 5f;
    private Rigidbody2D rb;
    private float directionY; // Almacena el valor de entrada del nuevo sistema

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Buenas prácticas para movimiento 2D
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    // Este callback recibe el input.
    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        directionY = inputVector.y; // Solo la componente vertical
    }

    void FixedUpdate()
    {
        float velocityY = directionY * PlayerSpeed;
        // ¡CORRECCIÓN AQUÍ! -> Usar 'rb.velocity' para Rigidbody2D
        rb.linearVelocity = new Vector2(0, velocityY);
    }

    // Recomendación: Limpieza de la entrada
    // Si tu InputManager usa eventos, desuscribe aquí si es el patrón usado:
    /*
    private void OnDestroy()
    {
        // Esto solo es necesario si te suscribes directamente a un evento del InputManager.Instance
        // Si usas el componente PlayerInput, no es necesario.
        // InputManager.Instance.OnMove -= HandleMove; 
    }
    */
}