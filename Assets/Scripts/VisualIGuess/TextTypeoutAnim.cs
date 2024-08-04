using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextTypeoutAnim : MonoBehaviour
{
    [SerializeField] private float textTypeoutLerpPower = 30f;
    [SerializeField] private TextMeshPro textMesh;
    private string textToTypeout = "";
    private float currTextProgress;
    void Update()
    {
        if(textToTypeout != "")
        {
            currTextProgress = Mathf.Lerp(currTextProgress, 1f, Time.deltaTime * textTypeoutLerpPower);
            textMesh.text = textToTypeout.Substring(0, Mathf.RoundToInt(currTextProgress * textToTypeout.Length));
        }
    }

    public void UpdateText(string newText)
    {
        textMesh.text = "";
        currTextProgress = 0f;
        textToTypeout = newText;
    }
}
