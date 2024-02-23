using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoolText : MonoBehaviour
{
    public TextMeshPro TextMeshPro;
    public string[] WhatCanItSay;
    public float TimToRemove;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshPro.text = LocalisationSystem.GetLocalizedValue(WhatCanItSay[Random.Range(0, WhatCanItSay.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        TimToRemove -= Time.deltaTime;
        if (TimToRemove <= 0f)
            Destroy(gameObject);
    }
}
