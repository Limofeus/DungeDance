using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sliderd : MonoBehaviour
{
    public float mapMin;
    public float mapMax;
    public float localSliderScale;
    public bool lerpIt;
    public Transform dragger;
    public Transform backgroundFilled;
    public SpriteRenderer draggerSpriteRenderer;
    public bool showValue;
    public TextMeshPro valueGauge;
    public Color notDraggingCol;
    public Color whileDraggingCol;
    private float sliderScale;
    public float sliderValue;
    public GameObject messageResiver;
    public string messageString;
    private bool overCollider;
    private bool dragging;
    private Vector3 startDragPosition;
    private Vector3 dragOffset;
    private float mappedValue;
    void Start()
    {

    }
    void Update()
    {
        sliderScale = localSliderScale * transform.localScale.x;
        if(Input.GetMouseButtonDown(0) && overCollider)
        {
            dragging = true;
            startDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragOffset = startDragPosition - dragger.position;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (dragging) UpdateDesiredValue();
            dragging = false;
        }
        if (overCollider) 
        {
            dragger.localScale = Vector3.Lerp(dragger.localScale, 1.5f * Vector3.one, 15f * Time.deltaTime);
        }
        else
        {
            dragger.localScale = Vector3.Lerp(dragger.localScale, 1f * Vector3.one, 15f * Time.deltaTime);
        }
        if (dragging)
        {
            draggerSpriteRenderer.color = Color.Lerp(draggerSpriteRenderer.color, whileDraggingCol, 15f * Time.deltaTime);
            sliderValue = Mathf.Clamp01(((Camera.main.ScreenToWorldPoint(Input.mousePosition) - dragOffset).x - (transform.position.x - (sliderScale / 2f))) / sliderScale);
            if (!lerpIt)
            {
                dragger.localPosition = Vector3.right * (sliderValue * 2f - 1f) * (localSliderScale / 2f);
                backgroundFilled.localScale = new Vector3(sliderValue, 1f, 1f);
            }
            else
            {
                // Oh god how I love .Lerp
                dragger.localPosition = Vector3.Lerp(dragger.localPosition, Vector3.right * (sliderValue * 2f - 1f) * (localSliderScale / 2f), 15f * Time.deltaTime);
                backgroundFilled.localScale = Vector3.Lerp(backgroundFilled.localScale, new Vector3(sliderValue, 1f, 1f), 15f * Time.deltaTime);
            }
            mappedValue = (sliderValue * (mapMax - mapMin)) + mapMin;
            if (showValue)
            {
                valueGauge.text = Mathf.RoundToInt(mappedValue * 100f).ToString();
            }
        }
        else
        {
            draggerSpriteRenderer.color = Color.Lerp(draggerSpriteRenderer.color, notDraggingCol, 15f * Time.deltaTime);
        }
    }
    //Not sure if two functions below will work
    public void SetValue(float value)
    {
        SetActualValue((value - mapMin) / (mapMax - mapMin));
    }
    public void SetActualValue(float actualValue)
    {
        dragger.localPosition = Vector3.right * (actualValue * 2f - 1f) * (localSliderScale / 2f);
        backgroundFilled.localScale = new Vector3(actualValue, 1f, 1f);
    }
    private void UpdateDesiredValue()
    {
        Debug.Log("Updating this slider's value (no)");
        //put code in here | -Thanks Limofeus from the past, now I know where to put the slider code \(> w < )/
        messageResiver.SendMessage(messageString, mappedValue);
    }
    public void UnmapAndUpdate(float remapValue)
    {
        sliderValue = (remapValue - mapMin) / (mapMax - mapMin);
        dragger.localPosition = Vector3.right * (sliderValue * 2f - 1f) * (localSliderScale / 2f);
        backgroundFilled.localScale = new Vector3(sliderValue, 1f, 1f);
        mappedValue = (sliderValue * (mapMax - mapMin)) + mapMin;
        if (showValue)
        {
            valueGauge.text = Mathf.RoundToInt(mappedValue * 100f).ToString();
        }
    }
    public void MouseEntered()
    {
        overCollider = true;
    }
    public void MouseExited()
    {
        overCollider = false;
    }
}
