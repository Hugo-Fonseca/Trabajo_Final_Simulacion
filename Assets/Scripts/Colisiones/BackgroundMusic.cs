using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    void Start()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
            audio.Play();
    }
}
