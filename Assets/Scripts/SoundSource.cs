using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSource : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource hitAudioSource;
    public AudioClip[] curseSounds;
    public AudioClip[] hitSounds;
    public AudioClip[] otherSounds;
    public float soundVolume;
    public void PlayCurseSound(int id)
    {
        audioSource.PlayOneShot(curseSounds[id], soundVolume);
    }

    public void PlayOtherSound(int id)
    {
        audioSource.PlayOneShot(otherSounds[id], soundVolume);
    }

    public void PlayHitSound(int id)
    {
        PlayHitSound(id, 1f);
    }
    public void PlayHitSound(int id, float volume)
    {
        hitAudioSource.pitch = Random.Range(0.8f, 1.4f);
        hitAudioSource.PlayOneShot(hitSounds[id], 0.6f * volume * soundVolume);
    }
}
