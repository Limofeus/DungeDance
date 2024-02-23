using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    public enum mode { Simple, Range, Random };
    public mode Mode;
    public bool Smoothing = true;
    public float Smoothness = 1;
    public int FreqMax;
    public int Freq;
    public Vector3 EffectMultiplier;
    public float limitScale;
    private Vector3 StartScale;
    private Vector3 Avarage;

    private void Start()
    {
        StartScale = transform.localScale;
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
                transform.localScale = StartScale + Vector3.ClampMagnitude(SpectrumManager.SpectrumData[Freq] * SpectrumManager.volumeDemultiplier * 10 * EffectMultiplier, limitScale);
            else
                transform.localScale = Vector3.Lerp(transform.localScale, StartScale + Vector3.ClampMagnitude(SpectrumManager.SpectrumData[Freq] * SpectrumManager.volumeDemultiplier * 10 * EffectMultiplier, limitScale), Smoothness * Time.deltaTime);
        else
        {
            Avarage = Vector3.zero;
            for (int i = Freq; i < FreqMax; i++)
            {
                Avarage += StartScale + Vector3.ClampMagnitude(SpectrumManager.SpectrumData[Freq] * SpectrumManager.volumeDemultiplier * 10 * EffectMultiplier, limitScale);
            }
            Avarage /= FreqMax - Freq;
            if (!Smoothing)
                transform.localScale = Avarage;
            else
                transform.localScale = Vector3.Lerp(transform.localScale, Avarage, Smoothness * Time.deltaTime);
        }
    }
}
