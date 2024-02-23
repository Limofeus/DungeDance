using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartStopPS : MonoBehaviour
{
    public ParticleSystem PS;
    public float[] time;
    private int Counter = 0;
    public float CurretTimeToWatch;

    // Update is called once per frame
    void Update()
    {
        CurretTimeToWatch = Time.time;
        if(Counter < time.Length && Time.time > time[Counter])
        {
            if (PS.isPlaying)
                PS.Stop();
            else
                PS.Play();
            Counter++;
        }
    }
}
