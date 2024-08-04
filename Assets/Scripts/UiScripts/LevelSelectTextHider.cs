using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelSelectTextHider : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private float timeTillFullMap = 1f;
    [SerializeField] private float fullMapPosLimit = 7.75f;
    [SerializeField] private float lerpPow = 20f;
    private float currentMapPosLimit = 0f;
    private Color startColor;
    private Color transparentColor;
    private float animStartTime;
    void Start()
    {
        startColor = textMesh.color;
        transparentColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
        textMesh.color = transparentColor;
        animStartTime = Time.time;
    }
    void Update()
    {
        currentMapPosLimit = Mathf.Clamp01((Time.time - animStartTime) / timeTillFullMap) * fullMapPosLimit;
        if(Mathf.Abs(transform.position.x) < currentMapPosLimit)
        {
            textMesh.color = Color.Lerp(textMesh.color, startColor, lerpPow * Time.deltaTime);
        }
        else
        {
            textMesh.color = Color.Lerp(textMesh.color, transparentColor, lerpPow * Time.deltaTime);
        }
    }
}
