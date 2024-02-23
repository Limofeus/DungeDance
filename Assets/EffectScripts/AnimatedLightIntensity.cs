using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class AnimatedLightIntensity : MonoBehaviour
{
    public float animatedValue;
    public float animationLerp;
    public enum mode { Simple, Range, Random };
    public mode Mode;
    public bool Smoothing;
    public float Smoothness;
    public int FreqMax;
    public int Freq;
    public float Intensity;
    private Light2D light2D;
    private float Avarage;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        if (Smoothness != 0)
            Smoothness = 10 / Smoothness;
        else
            Smoothing = false;
        if (Mode == mode.Random)
            Freq = Random.Range(Freq, FreqMax + 1);
    }
    void Update()
    {
        if (Mode != mode.Range)
            if (!Smoothing)
                light2D.intensity = Mathf.Lerp(SpectrumManager.SpectrumData[Freq] * 10 * Intensity * SpectrumManager.volumeDemultiplier, animatedValue, animationLerp);
            else
                light2D.intensity = Mathf.Lerp(Mathf.Lerp(light2D.intensity, SpectrumManager.SpectrumData[Freq] * 10 * Intensity * SpectrumManager.volumeDemultiplier, Smoothness * Time.deltaTime), animatedValue, animationLerp);
        else
        {
            Avarage = 0f;
            for (int i = Freq; i < FreqMax; i++)
            {
                Avarage += SpectrumManager.SpectrumData[i] * SpectrumManager.volumeDemultiplier * 10 * Intensity;
            }
            Avarage /= FreqMax - Freq;
            if (!Smoothing)
                light2D.intensity = Mathf.Lerp(Avarage, animatedValue, animationLerp);
            else
                light2D.intensity = Mathf.Lerp(Mathf.Lerp(light2D.intensity, Avarage, Smoothness * Time.deltaTime), animatedValue, animationLerp);
        }
    }
}
