using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectCameraEffects : MonoBehaviour
{
    public float circleshakePow;
    public float circleshakeSpeed;
    public float lerpPow;
    public float lerpDist;
    private Vector3 initialPosition;
    private Vector3 mouseVecLerped;
    void Start()
    {
        initialPosition = transform.position;
        //Debug.Log(Screen.width.ToString() + " " + Screen.height.ToString());
    }

    void Update()
    {
        //Debug.Log(Input.mousePosition.x.ToString() + " " + Input.mousePosition.y.ToString());
        Vector3 circleShakeVec = new Vector3(Mathf.Sin(Time.time * circleshakeSpeed), Mathf.Cos(Time.time * circleshakeSpeed), 0f) * circleshakePow;
        mouseVecLerped = Vector3.Lerp(mouseVecLerped, new Vector3((((Input.mousePosition.x/Screen.width) * 2f) - 1f) * (Screen.width / Screen.height), ((Input.mousePosition.y / Screen.height) * 2f) - 1f, 0f) * lerpDist, lerpPow * Time.deltaTime);
        transform.position = initialPosition + mouseVecLerped + circleShakeVec;
    }
}
