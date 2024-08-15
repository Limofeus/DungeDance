using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangSelectButton : MonoBehaviour
{
    public float scaleMultiplier = 1f;
    public float lerpPow = 1f;
    private Vector3 startScale;
    public bool mouseOver;
    [SerializeField] private LangSelection langSelection;
    [SerializeField] private int langId;
    private void Awake()
    {
        startScale = transform.localScale;
    }
    private void Update()
    {
        if (mouseOver)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, startScale * scaleMultiplier, Time.deltaTime * lerpPow);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, startScale, Time.deltaTime * lerpPow);
        }
        if ((Input.GetMouseButtonDown(0) /*|| (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began)*/) && mouseOver/* && MenuDataManager.uiLockValue <= uiLockValueCheck*/) //Comment the mobile thing off, the other one is in the level selector script
        {
            langSelection.SetLang(langId);
        }
    }
    private void OnMouseEnter()
    {
        //Debug.Log($"MDMuiLock: {MenuDataManager.uiLockValue}, CurrBTN: {gameObject.name}, ButnLock: {uiLockValueCheck}");
        mouseOver = true;
    }
    private void OnMouseExit()
    {
        mouseOver = false;
    }
}
