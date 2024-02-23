using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectrumManager : MonoBehaviour
{
    public AudioSource AudioSource;
    public static float volumeDemultiplier = 1;
    public static float[] SpectrumData;
    void Start()
    {
        SpectrumData = new float[256];
    }

    private void Update()
    {
        AudioSource.GetSpectrumData(SpectrumData, 0, FFTWindow.Hamming);
    }
}
