using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PostProcessWeight : MonoBehaviour
{
    public enum mode { Simple, Range, Random };
    public mode Mode;
    public bool Smoothing = true;
    public float Smoothness = 1f;
    public int FreqMax;
    public int Freq;
    public float Intensity = 1f;
    private Volume volume;
    private float Avarage;

    private void Start()
    {
        volume = GetComponent<Volume>();
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
                volume.weight = SpectrumManager.SpectrumData[Freq] * SpectrumManager.volumeDemultiplier * 10 * Intensity;
            else
                volume.weight = Mathf.Lerp(volume.weight, SpectrumManager.SpectrumData[Freq] * SpectrumManager.volumeDemultiplier * 10 * Intensity, Smoothness * Time.deltaTime);
        else
        {
            Avarage = 0f;
            for (int i = Freq; i < FreqMax; i++)
            {
                Avarage += SpectrumManager.SpectrumData[i] * SpectrumManager.volumeDemultiplier * 10 * Intensity;
            }
            Avarage /= FreqMax - Freq;
            if (!Smoothing)
                volume.weight = Avarage;
            else
                volume.weight = Mathf.Lerp(volume.weight, Avarage, Smoothness * Time.deltaTime);
        }
    }
}
