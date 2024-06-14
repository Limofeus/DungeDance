using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingArrowVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer arrowSpriteRenderer;
    [SerializeField] private float defaultArrowScale = 0.5f;
    [SerializeField] private float deleteArrowScale = 1.0f;

    [SerializeField] private float arrowLerpPower = 15f;
    [SerializeField] private float arrowDeleteTime = 0.25f;

    [SerializeField] private Color correctHitColor;
    [SerializeField] private Color wrongHitColor;

    private float targetArrowScale;
    private Color targetArrowColor;

    private bool deletingArrow = false;
    private Color endColor;

    void Start()
    {
        targetArrowScale = defaultArrowScale;
        targetArrowColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (!deletingArrow)
        {
            arrowSpriteRenderer.transform.localScale = Vector3.Lerp(arrowSpriteRenderer.transform.localScale, targetArrowScale * Vector3.one, Time.deltaTime * arrowLerpPower);
            arrowSpriteRenderer.color = Color.Lerp(arrowSpriteRenderer.color, targetArrowColor, Time.deltaTime * arrowLerpPower);
        }
    }

    public void SetArrowRotation(int rotation) // 0 - Up, 1 - Right, 2 - Down, 3 - Left
    {
        transform.Rotate(Vector3.back, rotation * 90f);
    }

    public void DeleteArrow(bool correctHit)
    {
        deletingArrow = true;
        arrowSpriteRenderer.color = correctHit ? correctHitColor : wrongHitColor;
        endColor = correctHit ? correctHitColor : wrongHitColor;
        endColor.a = 0f;
        StartCoroutine(DeleteArrow());
    }

    private IEnumerator DeleteArrow()
    {
        float deleteArrowTimer = 0f;
        float deleteProgress = 0f;
        Color startColor = arrowSpriteRenderer.color;
        while (deleteArrowTimer <= arrowDeleteTime)
        {
            deleteProgress = Mathf.Clamp01(deleteArrowTimer / arrowDeleteTime);
            deleteArrowTimer += Time.deltaTime;
            arrowSpriteRenderer.color = Color.Lerp(startColor, endColor, deleteProgress);
            arrowSpriteRenderer.transform.localScale = Vector3.Lerp(defaultArrowScale * Vector3.one, deleteArrowScale * Vector3.one, deleteProgress);
            yield return null;
        }
        Destroy(gameObject);
    }
}
