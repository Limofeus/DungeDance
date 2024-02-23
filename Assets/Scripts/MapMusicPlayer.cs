using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public static float menuTrackMoment;
    public static MapMusicPlayer mapMusicPlayer;
    public float volumeMax;
    public float volumeMin;
    private float musicVolume;
    private bool mouseOverTrack;
    private bool fadeOut;
    private float timerr;

    void Start()
    {
        audioSource.time = menuTrackMoment;
        musicVolume = SaveSystem.Load().settingsData.musicVolume;
        mapMusicPlayer = this;
    }
    void Update()
    {
        if (timerr > 0.5f)
        {
            if (fadeOut)
            {
                audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, 2f * Time.deltaTime);
            }
            else
            {
                if (mouseOverTrack)
                {
                    audioSource.volume = Mathf.Lerp(audioSource.volume, volumeMax * musicVolume, 6f * Time.deltaTime);
                }
                else
                {
                    audioSource.volume = Mathf.Lerp(audioSource.volume, volumeMin * musicVolume, 4f * Time.deltaTime);
                }
            }
        }
        else
        {
            timerr += Time.deltaTime;
        }
    }
    public void MouseOverTrack(bool mouseOver, AudioClip musicClip, float audioTime)
    {
        if (mouseOver)
        {
            mouseOverTrack = true;
            if (audioSource.clip != musicClip)
            {
                audioSource.clip = musicClip;
                audioSource.Play();
                audioSource.time = audioTime;
            }
        }
        else
        {
            mouseOverTrack = false;
        }
    }

    public void FadeOut()
    {
        fadeOut = true;
    }

}
