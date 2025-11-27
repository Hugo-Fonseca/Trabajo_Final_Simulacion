using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementVertical : MonoBehaviour
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

        InputManager.Instance.OnMove += HandleMove;
    }

    void HandleMove(Vector2 input)
    {
        playerMoveInput = new Vector2(0, input.y);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = playerMoveInput * moveSpeed;

        if (animator != null)
        {
            float speed = rb.linearVelocity.magnitude;
            animator.SetFloat("Movement", speed * speed);
            animator.SetFloat("MoveY", rb.linearVelocity.y);
        }
    }

    private void OnDestroy()
    {
        if (InputManager.Instance != null)
            InputManager.Instance.OnMove -= HandleMove;
    }
}
