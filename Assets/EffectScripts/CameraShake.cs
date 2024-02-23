using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool moveIt;
    public float ShakeSpeed;
    public float ShakePower;
    public float Threshold;
    public bool rotateIt;
    public float rotateSpeed;
    public float rotatePowerDeg;
    public float rotateThesholdDeg;
    public int Freq;
    private float PowerMult;
    private Vector3 StartPosition;
    private Vector3 NextPosition;
    private float startRotation;
    private float nextRotation;
    public void Start()
    {
        StartPosition = transform.position;
        startRotation = transform.rotation.eulerAngles.z;
        NextPosition = StartPosition;
        nextRotation = startRotation;
    }
    private void Update()
    {
        PowerMult = SpectrumManager.SpectrumData[Freq] * SpectrumManager.volumeDemultiplier * 10;
        if (moveIt)
        {
            transform.position = Vector3.Lerp(transform.position, NextPosition, ShakeSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, NextPosition) < Threshold)
                PickNextPosition();
        }
        if (rotateIt)
        {
            transform.rotation = Quaternion.Lerp(Quaternion.Euler(0,0, transform.rotation.eulerAngles.z), Quaternion.Euler(0, 0, nextRotation), rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z), Quaternion.Euler(0, 0, nextRotation)) < rotateThesholdDeg)
                PickNextRotation();
        }
    }
    void PickNextPosition()
    {
        NextPosition = StartPosition + new Vector3(Random.Range(-ShakePower, ShakePower) * PowerMult, Random.Range(-ShakePower, ShakePower) * PowerMult, 0f);
    }
    void PickNextRotation()
    {
        nextRotation = startRotation + (Random.Range(-rotatePowerDeg, rotatePowerDeg) * PowerMult);
    }
}
