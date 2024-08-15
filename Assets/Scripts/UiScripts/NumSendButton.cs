using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface NumSelectable {

    public void OnNumSelected(int numSelected);

}

public class NumSendButton : MonoBehaviour
{
    public float scaleMultiplier = 1f;
    public float lerpPow = 1f;
    private Vector3 startScale;
    public bool mouseOver;
    [SerializeField] private GameObject numSelectable;
    private NumSelectable numSel;
    [SerializeField] private int selectionNum;
    private void Awake()
    {
        numSel = numSelectable.GetComponent<NumSelectable>();
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
        if ((Input.GetMouseButtonDown(0) /*|| (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began)*/) && mouseOver/* && MenuDataManager.uiLockValue <= uiLockValueCheck*/) //Comment the mobile thing off, the other one is in the level selector script (Well, this comment was made a long time ago, now there are once in... a lot of different scripts)
        {
            numSel.OnNumSelected(selectionNum);
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
