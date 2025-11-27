using UnityEngine;

public class Launcher2D : MonoBehaviour
{
    [Header("References")]
    public Transform launchPoint;
    public GameObject ballPrefab;

    [Header("Launch Settings")]
    public float minForce = 3f;
    public float maxForce = 15f;
    public float force = 8f;

    public float rotationSpeed = 90f;

    void Update()
    {
        HandleAim();
        HandleForce();
        HandleShoot();
    }

    void HandleAim()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        float angle = transform.eulerAngles.z;
        angle -= mouseY * rotationSpeed * Time.deltaTime;
        angle = Mathf.Clamp(angle, -10, 80);

        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    void HandleForce()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            force += 10 * Time.deltaTime;

        if (Input.GetKey(KeyCode.DownArrow))
            force -= 10 * Time.deltaTime;

        force = Mathf.Clamp(force, minForce, maxForce);
    }

    void HandleShoot()
    {
        
        {
            Debug.Log("Espera a que el gato recoja la pelota y Regrese.");
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject ball = Instantiate(ballPrefab, launchPoint.position, launchPoint.rotation);
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

            Vector2 dir = launchPoint.right;
            rb.AddForce(dir * force, ForceMode2D.Impulse);

            // Cámara sigue la pelota
            BallCamera2D cam = Camera.main.GetComponent<BallCamera2D>();
            if (cam != null)
                cam.SetTarget(ball.transform);

           
        }
    }
}
