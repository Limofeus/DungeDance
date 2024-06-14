using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLineManager : MonoBehaviour
{
    [SerializeField] private Transform linePointRod;
    [SerializeField] private Transform linePointFloat;
    [SerializeField] private LineRenderer lineRenderer;

    private void Update()
    {
        if (linePointFloat.gameObject.activeSelf)
        {
            lineRenderer.gameObject.SetActive(true);
            lineRenderer.SetPosition(0, linePointRod.position);
            lineRenderer.SetPosition(1, linePointFloat.position);
        }
        else
        {
            lineRenderer.gameObject.SetActive(false);
        }
    }
}
