using UnityEngine;
using TMPro;

public class CatNPC2D : MonoBehaviour
{
    [Header("Velocidades")]
    public float speedToBall = 3f;
    public float speedReturn = 6f;

    [Header("Referencias")]
    public Transform launcherPosition;
    public Transform cameraStartPoint;
    public Camera mainCamera;

    public Animator animator;
    private Transform ball;
    private bool returningToLauncher = false;

    public bool IsBusy()
    {
        return ball != null || returningToLauncher;
    }

    void Update()
    {
        if (ball != null && !returningToLauncher)
        {
            MoveTowardsX(ball.position.x, speedToBall);
        }
        else if (returningToLauncher)
        {
            MoveTowardsX(launcherPosition.position.x, speedReturn);

            float d = Mathf.Abs(transform.position.x - launcherPosition.position.x);
            if (d < 0.1f)
                returningToLauncher = false;
        }
        else
        {
            animator.SetFloat("Movements", 0f);
        }
    }

    void MoveTowardsX(float targetX, float speed)
    {
        float oldX = transform.position.x;

        Vector2 p = transform.position;
        p.x = Mathf.MoveTowards(p.x, targetX, speed * Time.deltaTime);
        transform.position = p;

        float movement = Mathf.Abs(p.x - oldX); 
        animator.SetFloat("Movements", movement);

        if (p.x > oldX)
            transform.localScale = new Vector3(1, 1, 1);  // mirando derecha
        else if (p.x < oldX)
            transform.localScale = new Vector3(-1, 1, 1); // mirando izquierda
    }

    public void SetBall(Transform ballTransform)
    {
        ball = ballTransform;
        returningToLauncher = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ball == null) return;
        if (collision.transform != ball) return;

        Debug.Log("El gato recogió la pelota!");

        // Detener cámara
        BallCamera2D cam = mainCamera.GetComponent<BallCamera2D>();
        if (cam != null) cam.SetTarget(null);

        // Destruir pelota
        Destroy(ball.gameObject);
        ball = null;

        // Reset de velocidad en UI
        Lanzador lanzador = FindObjectOfType<Lanzador>();
        if (lanzador != null && lanzador.textoVelocidad != null)
            lanzador.textoVelocidad.text = "Vel: 0 m/s";

        // Empezar retorno
        returningToLauncher = true;

        // Reset cámara a inicio
        if (cameraStartPoint != null)
        {
            mainCamera.transform.position = new Vector3(
                cameraStartPoint.position.x,
                cameraStartPoint.position.y,
                mainCamera.transform.position.z
            );
        }
    }
}
