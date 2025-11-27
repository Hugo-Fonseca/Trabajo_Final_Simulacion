using UnityEngine;

public class BallCamera2D : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10f);
    public float suavizado = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 objetivo = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, objetivo, Time.deltaTime * suavizado);
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

}
