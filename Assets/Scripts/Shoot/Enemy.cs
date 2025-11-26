using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    public float rotationSpeed = 5f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!target)
            GetTarget();
    }

    private void FixedUpdate()
    {
        if (!target) return;

        SimulateLookDirection();
        SimulateMovement();
    }

    private void SimulateMovement()
    {
        rb.linearVelocity = transform.up * speed;
    }

    private void SimulateLookDirection()
    {
        Vector2 targetDirection = target.position - transform.position;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, rotationSpeed * Time.fixedDeltaTime);

        
        if (targetDirection.x > 0.1f)
            sr.flipX = false;  // mira derecha
        else if (targetDirection.x < -0.1f)
            sr.flipX = true;   // mira izquierda
    }

    private void GetTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player)
            target = player.transform;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
