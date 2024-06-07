using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FishHoverOverHint : MonoBehaviour
{
    [SerializeField] private float scaleLerpPow = 20f;
    [SerializeField] private float moveLerpPow = 15f;
    [SerializeField] private TextMeshPro upperText;
    [SerializeField] private TextMeshPro lowerText;
    public bool shown = false;

    private Vector3 desiredCoords;
    private void Start()
    {
        desiredCoords = transform.position;
    }
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, shown ? Vector3.one : Vector3.zero, scaleLerpPow * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, desiredCoords, moveLerpPow * Time.deltaTime);
    }
    public void UpdateTextAndCoords(Vector3 newCoords, string sUpperText, string sLowerText)
    {
        desiredCoords = newCoords;
        upperText.text = sUpperText;
        lowerText.text = sLowerText;
    }
}
