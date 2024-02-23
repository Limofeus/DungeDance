using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LightIntensity : MonoBehaviour
{
    public bool RandomFreq;
    public bool Lerp;
    public float LerpHardness;
    public bool ToMaxFreq;
    public int FreqMax;
    public UnityEngine.Rendering.Universal.Light2D light2D;
    public int Freq;
    public float Intensity;
    private List<float> FrameValue = new List<float>();
    private float Avarage;

    private void Start()
    {
        if (RandomFreq)
            Freq = Random.Range(Freq, FreqMax + 1);
    }
    void Update()
    {
        if (!ToMaxFreq)
            if (!Lerp)
                light2D.intensity = SpectrumManager.SpectrumData[Freq] * SpectrumManager.volumeDemultiplier * 10 * Intensity;
            else
                light2D.intensity = Mathf.Lerp(light2D.intensity, SpectrumManager.SpectrumData[Freq] * SpectrumManager.volumeDemultiplier * 10 * Intensity, LerpHardness * Time.deltaTime);
        else
        {
            Avarage = 0f;
            for (int i = Freq; i < FreqMax; i++)
            {
                Avarage += SpectrumManager.SpectrumData[i] * SpectrumManager.volumeDemultiplier * 10 * Intensity;
            }
            Avarage /= FreqMax - Freq;
            light2D.intensity = Avarage;
        }
    }
}
