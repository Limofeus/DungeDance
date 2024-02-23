using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVolumeMultiply : MonoBehaviour
{
    public float volumeA;
    public float volumeB;
    public AudioSource audioSource;
    void Update()
    {
        audioSource.volume = volumeA * volumeB;
    }
}
