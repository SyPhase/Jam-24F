using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePitch : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] float volume = 1.0f;

    void OnEnable()
    {
        SoundManager.Instance.PlayOneShot(audioClip, volume);
    }
}