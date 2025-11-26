using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private float fireRate = 0.5f; 
    private bool canShoot = true;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Vector2 mousePos;

    private Camera cam;
    private Animator anim;
    private SpriteRenderer sr;

    private bool shootRequested = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        rb.freezeRotation = true;

        InputManager.Instance.OnMove += HandleMove;
    }

    private void HandleMove(Vector2 input)
    {
        moveInput = input;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            shootRequested = true;
    }

    private void FixedUpdate()
    {
        SimulateMovement();
        SimulateLookDirection();

        if (shootRequested && canShoot)
        {
            Shoot();
            shootRequested = false;
        }
        else
        {
            shootRequested = false;
        }
    }

    private void SimulateMovement()
    {
        if (moveInput.sqrMagnitude > 0.01f)
            rb.linearVelocity = moveInput.normalized * speed;
        else
            rb.linearVelocity = Vector2.zero;

        anim.SetFloat("Movement", moveInput.sqrMagnitude);
    }

    private void SimulateLookDirection()
    {
        mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 dir = mousePos - (Vector2)transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (moveInput.x > 0.1f)
        {
            sr.flipX = false;
            firePoint.localPosition = new Vector3(0.5f, 0f, 0f);
        }
        else if (moveInput.x < -0.1f)
        {
            sr.flipX = true;
            firePoint.localPosition = new Vector3(-0.5f, 0f, 0f);
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        canShoot = false;
        Invoke(nameof(ResetShoot), fireRate);
    }

    private void ResetShoot()
    {
        canShoot = true;
    }
}
