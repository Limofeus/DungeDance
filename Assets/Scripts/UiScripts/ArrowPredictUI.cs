using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPredictUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer arrowOutlineSR;
    [SerializeField] private float lerpPow;
    private Transform arrowOutlineTransform;
    private GameObject currClosestArrow = null;

    private Color targetArrowSrColor = new Color(1f, 1f, 1f, 0f);

    private void Start()
    {
        arrowOutlineTransform = arrowOutlineSR.transform;
    }

    private void Update()
    {
        if(currClosestArrow != null)
            arrowOutlineTransform.localRotation = currClosestArrow.transform.localRotation * currClosestArrow.transform.GetChild(0).localRotation;
        arrowOutlineSR.color = Color.Lerp(arrowOutlineSR.color, targetArrowSrColor, lerpPow * Time.deltaTime);
        GameObject closestArrow = MainManager.CalculateNextArrow(2f);
        if (closestArrow != currClosestArrow)
        {
            UpdateClosestArrow(closestArrow);
        }
    }

    private void UpdateClosestArrow(GameObject newArrow)
    {
        currClosestArrow = newArrow;
        if(currClosestArrow == null)
        {
            targetArrowSrColor = new Color(1f, 1f, 1f, 0f);
        }
        else
        {
            targetArrowSrColor = new Color(1f, 1f, 1f, 1f);
        }
    }
}
