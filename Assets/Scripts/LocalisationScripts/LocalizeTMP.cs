using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizeTMP : MonoBehaviour
{
    public string key;
    public TextMeshPro textMeshPro;
    public bool setManualy;
    private void Start()
    {
        if (!setManualy)
            textMeshPro = GetComponent<TextMeshPro>();
        textMeshPro.text = LocalisationSystem.GetLocalizedValue(key);
    }
}
