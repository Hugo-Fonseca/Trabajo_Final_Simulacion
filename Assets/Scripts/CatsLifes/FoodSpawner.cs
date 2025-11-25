using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public Transform spawnPoint;

    // 🔊 sonido al aparecer la comida
    public AudioClip spawnSound;
    private AudioSource audioSource;

    private GameObject comidaActual;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void SpawnFood()
    {
        // si ya había una comida, eliminarla
        if (comidaActual != null)
            Destroy(comidaActual);

        // generar nueva comida en el punto de spawn
        comidaActual = Instantiate(foodPrefab, spawnPoint.position, Quaternion.identity);

        // 🔊 reproducir sonido de spawn
        if (spawnSound != null)
            audioSource.PlayOneShot(spawnSound);
    }

    public void DestruirComida()
    {
        if (comidaActual != null)
        {
            Destroy(comidaActual);
            comidaActual = null;
        }
    }

    public GameObject GetComida()
    {
        return comidaActual;
    }

    // 👇 NUEVO: útil para saber si hay comida
    public bool HayComida()
    {
        return comidaActual != null;
    }

    // 👇 NUEVO: devuelve la posición de la comida
    public Vector3 GetPosicionComida()
    {
        if (comidaActual == null) return Vector3.zero;
        return comidaActual.transform.position;
    }
}
