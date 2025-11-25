using UnityEngine;
using System.Collections;

public class ScareManager : MonoBehaviour
{
    public bool randomScares = false;
    public float minInterval = 8f;
    public float maxInterval = 18f;

    public AudioSource scareSound;
    public Animal targetAnimal;

    [Header("Scare Visual")]
    public GameObject scareVisualPrefab;
    public Vector2 minSpawn = new Vector2(-5, -3);
    public Vector2 maxSpawn = new Vector2(5, 3);
    public float scareVisualDuration = 1.5f;

    [Header("Cooldown entre sustos manuales")]
    public float manualScareCooldown = 12f;   
    private bool canScare = true;

    void Start()
    {
        if (randomScares) StartCoroutine(RandomScaresLoop());
    }

    public void TriggerScareFromUI()
    {
        if (!canScare) return;  

        StartCoroutine(ScareCooldown());
        PlayScare();
    }

    IEnumerator ScareCooldown()
    {
        canScare = false;
        yield return new WaitForSeconds(manualScareCooldown);
        canScare = true;
    }

    void PlayScare()
    {
        if (scareSound != null) scareSound.Play();
        SpawnScareVisual();
        if (targetAnimal != null) targetAnimal.TriggerScare();
    }

    IEnumerator RandomScaresLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            PlayScare();
        }
    }

    void SpawnScareVisual()
    {
        if (scareVisualPrefab == null) return;

        Vector3 pos = new Vector3(
            Random.Range(minSpawn.x, maxSpawn.x),
            Random.Range(minSpawn.y, maxSpawn.y),
            0
        );

        GameObject visual = Instantiate(scareVisualPrefab, pos, Quaternion.identity);

        Destroy(visual, scareVisualDuration);
    }
}
