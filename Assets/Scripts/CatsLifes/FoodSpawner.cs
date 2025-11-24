using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject foodPrefab;
    public Transform spawnPoint;

    // 🔊 sonido al aparecer la comida
    public AudioClip spawnSound;
    private AudioSource audioSource;

    GameObject comidaActual;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void SpawnFood()
    {
        if (comidaActual != null) Destroy(comidaActual);

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
}
