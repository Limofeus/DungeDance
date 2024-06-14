using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishColPrefab : MonoBehaviour
{
    public enum FishVisib { Hidden, Unknown, Caught }
    [SerializeField] private SpriteRenderer fishRenderer;
    [SerializeField] private SpriteRenderer[] childSRs;
    [SerializeField] private GameObject questionMarkObj;
    [SerializeField] private float mouseOverScaleMult = 1.1f;
    [SerializeField] private float lerpPower = 20f;

    private bool mouseOver = false;
    [HideInInspector] public FishHoverOverHint hoverOverHint;

    public string upperText;
    public string lowerText;
    public string fishSizeString;

    private void OnMouseEnter()
    {
        mouseOver = true;
        hoverOverHint.shown = true;
        hoverOverHint.UpdateTextAndCoords(transform.position, upperText, lowerText, fishSizeString);
    }
    private void OnMouseExit()
    {
        mouseOver = false;
        hoverOverHint.shown = false;
    }
    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, (mouseOver ? mouseOverScaleMult : 1f) * Vector3.one, Time.deltaTime * lerpPower);
    }
    public void UpdateFishSprite(int fishSpriteId)
    {
        fishRenderer.sprite = FishDict.fishSprites[fishSpriteId];
        foreach(SpriteRenderer sr in childSRs)
        {
            sr.sprite = FishDict.fishSprites[fishSpriteId];
        }
    }
    public void UpdateFishVisibility(FishVisib fishVisib)
    {
        switch(fishVisib)
        {
            case FishVisib.Hidden:
                fishRenderer.gameObject.SetActive(false);
                questionMarkObj.SetActive(true);
                break;
            case FishVisib.Unknown:
                fishRenderer.color = Color.black;
                fishRenderer.gameObject.SetActive(true);
                questionMarkObj.SetActive(false);
                break;
            case FishVisib.Caught:
                fishRenderer.color = Color.white;
                fishRenderer.gameObject.SetActive(true);
                questionMarkObj.SetActive(false);
                break;
        }
    }
}
