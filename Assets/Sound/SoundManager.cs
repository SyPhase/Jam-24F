using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip audioClip, float volumeScale = 1.0f)
    {
        float pitch = Random.Range(0.8f, 1.2f);
        audioSource.pitch = pitch;

        audioSource.PlayOneShot(audioClip, volumeScale);
    }
}
