using UnityEngine;

public class CogSoundScript : MonoBehaviour
{
    private AudioSource myAudioSource;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.Play();
        Destroy(gameObject, myAudioSource.clip.length);
    }
}
