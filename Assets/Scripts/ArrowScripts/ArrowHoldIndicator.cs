using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHoldIndicator : MonoBehaviour
{
    [SerializeField] private float length;
    [SerializeField] private float progres;

    [SerializeField] private Transform backgroundHolder;
    [SerializeField] private Transform gradientHolder;
    [SerializeField] private Transform endCircleHolder;

    [SerializeField] private SpriteRenderer backgroundSR;
    [SerializeField] private LineRenderer gradientRenderer;
    [SerializeField] private SpriteRenderer circleSR;

    [SerializeField] private Color holdStartColor;
    [SerializeField] private Color holdEndColor;

    private GradientAlphaKey[] gradAlphaKeys = new GradientAlphaKey[2] { new GradientAlphaKey(1.0f, 0.0f) , new GradientAlphaKey(1.0f, 1.0f) }; //В идеале эта херь константа, но этот класс не может быть константой :(

    private void Start()
    {
        backgroundHolder.localScale = new Vector3(length, backgroundHolder.localScale.y, backgroundHolder.localScale.z);
        gradientHolder.localScale = new Vector3(0f, gradientHolder.localScale.y, gradientHolder.localScale.z);
        endCircleHolder.localPosition = new Vector3(length, 0f, 0f);

        circleSR.color = backgroundSR.color;
    }

    private void Update()
    {
        UpdateHoldProgress(progres);
    }

    public void UpdateHoldProgress(float progress)
    {
        gradientHolder.localScale = new Vector3(progress * length, gradientHolder.localScale.y, gradientHolder.localScale.z);

        // Blend color from red at 0% to blue at 100%
        var gradColors = new GradientColorKey[2];
        gradColors[0] = new GradientColorKey(holdStartColor, 0.0f);
        gradColors[1] = new GradientColorKey(Color.Lerp(holdStartColor, holdEndColor, progress), 1.0f);

        Gradient gradient = new Gradient();
        gradient.SetKeys(gradColors, gradAlphaKeys);
        gradientRenderer.colorGradient = gradient; //А по другому (gradientRenderer.colorGradient.SetKeys...) не работает

        if (progress >= 1f)
        {
            circleSR.color = holdEndColor;
        }
        else
        {
            circleSR.color = backgroundSR.color;
        }
    }

}
