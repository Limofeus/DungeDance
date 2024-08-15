using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HelpPopupVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI upperText;
    [SerializeField] private TextMeshProUGUI lowerTextLong;
    [SerializeField] private TextMeshProUGUI lowerTextShort;
    [SerializeField] private Image popupImage;
    [SerializeField] private float closeLerpPow = 20f;
    private bool closeOnAction = false;
    private bool closing = false;
    private SceneHintPopupManager sHPM;
    private float uiLockValueToSet = 2f;
    private bool uiLockfired = false;
    private void Update()
    {
        if(closeOnAction && Input.anyKeyDown)
        {
            closing = true;
            if (!uiLockfired)
            {
                MenuDataManager.uiLockValue = 0f;
                uiLockfired = true;
            }
            sHPM?.HintClosed();
        }
        if(closeOnAction)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, closing ? Vector3.zero : Vector3.one, Time.deltaTime * closeLerpPow);
        }
    }
    public void InitializeHint(string hintPopupTag, Dictionary<string, HintPopupPerTagInfo> hintPopupDict, bool closeOnActionV = false, SceneHintPopupManager sceneHintPopupManager = null)
    {
        HintPopupPerTagInfo thisHintInfo = hintPopupDict[hintPopupTag];
        sHPM = sceneHintPopupManager;
        if (thisHintInfo.sprite != null) 
        {
            SetPopupTextImage(thisHintInfo.upperText, thisHintInfo.lowerText, thisHintInfo.sprite);
        }
        else
        {
            SetPopupTextLong(thisHintInfo.upperText, thisHintInfo.lowerText);
        }
        if (closeOnActionV)
        {
            closeOnAction = closeOnActionV;
            transform.localScale = Vector3.zero;
            MenuDataManager.uiLockValue = uiLockValueToSet;
        }
    }
    private void SetPopupTextLong(string upperString, string mainString)
    {
        popupImage.gameObject.SetActive(false);
        lowerTextShort.gameObject.SetActive(false);
        lowerTextLong.gameObject.SetActive(true);

        upperText.text = LocalisationSystem.GetLocalizedValue(upperString);
        lowerTextLong.text = LocalisationSystem.GetLocalizedValue(mainString);
    }

    private void SetPopupTextImage(string upperString, string mainString, Sprite sprite)
    {
        popupImage.gameObject.SetActive(true);
        lowerTextShort.gameObject.SetActive(true);
        lowerTextLong.gameObject.SetActive(false);

        upperText.text = LocalisationSystem.GetLocalizedValue(upperString);
        lowerTextShort.text = LocalisationSystem.GetLocalizedValue(mainString);
        popupImage.sprite = sprite;
    }
}
