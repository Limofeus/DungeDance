using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator animator;
    public float scaleMultiplier = 1f;
    public float scaleMultiplierMultiplier = 1f;
    public bool lerpScaling; //uses normal scaleMultiplier as target scale multiplier AND uses scaleMultiplierMultiplier as lerp power!! <-----
    private Vector3 startScale;
    public bool mouseOver;
    public bool sendMessageOnClick;
    public GameObject messageRessiver;
    public string messageItem;
    [SerializeField] private float uiLockValueCheck = 0f;
    private void Awake()
    {
        startScale = transform.localScale;
    }
    private void Update()
    {
        if (!lerpScaling)
        {
            //So here is an example of what I call "really thought through code"
            transform.localScale = startScale * (1f + ((scaleMultiplier - 1f) * scaleMultiplierMultiplier));
        }
        else
        {
            if (mouseOver)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, startScale * scaleMultiplier, Time.deltaTime * scaleMultiplierMultiplier);
            }
            else
            {
                transform.localScale = Vector3.Lerp(transform.localScale, startScale, Time.deltaTime * scaleMultiplierMultiplier);
            }
        }
        if ((Input.GetMouseButtonDown(0) /*|| (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began)*/) && mouseOver/* && MenuDataManager.uiLockValue <= uiLockValueCheck*/) //Comment the mobile thing off, the other one is in the level selector script
        {
            if (sendMessageOnClick)
                messageRessiver.SendMessage(messageItem);
        }
    }
    private void OnMouseEnter()
    {
        if (MenuDataManager.uiLockValue > uiLockValueCheck) return;
        //Debug.Log($"MDMuiLock: {MenuDataManager.uiLockValue}, CurrBTN: {gameObject.name}, ButnLock: {uiLockValueCheck}");
        mouseOver = true;
        if(!lerpScaling)
            animator.SetBool("Over",true);
    }
    private void OnMouseExit()
    {
        if (MenuDataManager.uiLockValue > uiLockValueCheck) return;
        mouseOver = false;
        if(!lerpScaling)
            animator.SetBool("Over", false);
    }
}
