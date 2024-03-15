using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHoldIndicator : MonoBehaviour
{
    [SerializeField] private float length;

    [SerializeField] private Transform backgroundHolder;
    [SerializeField] private Transform gradientHolder;
    [SerializeField] private Transform endCircleHolder;

    [SerializeField] private SpriteRenderer backgroundSR;
    [SerializeField] private LineRenderer gradientRenderer;
    [SerializeField] private SpriteRenderer circleSR;

    [SerializeField] private Color holdStartColor;
    [SerializeField] private Color holdEndColor;

    private GradientAlphaKey[] gradAlphaKeys = new GradientAlphaKey[2] { new GradientAlphaKey(1.0f, 0.0f) , new GradientAlphaKey(1.0f, 1.0f) }; //В идеале эта херь константа, но этот класс не может быть константой :(

    public void InitializeIndicator(float tlength)
    {
        length = tlength;

        backgroundHolder.localScale = new Vector3(length, backgroundHolder.localScale.y, backgroundHolder.localScale.z);
        gradientHolder.localScale = new Vector3(0f, gradientHolder.localScale.y, gradientHolder.localScale.z);
        endCircleHolder.localPosition = new Vector3(length, 0f, 0f);

        circleSR.color = backgroundSR.color;
    }
    public void SetGradientStartColor(Color stColor)
    {
        holdStartColor = stColor;
    }
    public void SetGradientEbdColor(Color stColor)
    {
        holdEndColor = stColor;
    }
    public void CalculateEndColor(float offset)
    {
        float maxOffset = 0.1f;

        if (offset < 0.1f)
        {
            float MakeYellow;
            MakeYellow = (0.5f - Mathf.Abs((offset / maxOffset) - 0.5f));
            Color NeededColor = new Color((offset / maxOffset) + MakeYellow, (1f - (offset / maxOffset)) + MakeYellow, 0f);
            holdEndColor = NeededColor;
            //Auto upd progress if offset is low
            UpdateHoldProgress(1f);
        }
        else
        {
            holdEndColor = Color.red;
        }
    }
    public void UpdateHoldProgress(float progress)
    {
        progress = Mathf.Clamp01(progress);

        gradientHolder.localScale = new Vector3(progress * length, gradientHolder.localScale.y, gradientHolder.localScale.z);

        // Blend color from red at 0% to blue at 100%
        var gradColors = new GradientColorKey[2];
        gradColors[0] = new GradientColorKey(holdStartColor, 0.2f);
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
