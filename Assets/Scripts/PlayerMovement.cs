using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public Animator animator;

    private Vector2 playerMoveInput;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        InputManager.Instance.OnMove += HandleMove;
    }

    void HandleMove(Vector2 input)
    {
        playerMoveInput = input;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = playerMoveInput * moveSpeed;

        float speed = rb.linearVelocity.magnitude;    
        animator.SetFloat("Movement", speed*speed);

        animator.SetFloat("MoveX", rb.linearVelocity.x);
        animator.SetFloat("MoveY", rb.linearVelocity.y);

        // Girar el sprite según dirección
        if (rb.linearVelocity.x > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);  // mirando derecha
        }
        else if (rb.linearVelocity.x < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1); // mirando izquierda
        }

    }
}
