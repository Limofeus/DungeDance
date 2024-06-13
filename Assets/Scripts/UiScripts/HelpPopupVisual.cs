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

    private void Update()
    {
        if(closeOnAction && Input.anyKeyDown)
        {
            closing = true;
        }
        if(closeOnAction)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, closing ? Vector3.zero : Vector3.one, Time.deltaTime * closeLerpPow);
        }
    }
    public void InitializeHint(string hintPopupTag, Dictionary<string, HintPopupPerTagInfo> hintPopupDict, bool closeOnActionV = false)
    {
        HintPopupPerTagInfo thisHintInfo = hintPopupDict[hintPopupTag];
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
        }
    }
    private void SetPopupTextLong(string upperString, string mainString)
    {
        popupImage.gameObject.SetActive(false);
        lowerTextShort.gameObject.SetActive(false);
        lowerTextLong.gameObject.SetActive(true);

        upperText.text = upperString;
        lowerTextLong.text = mainString;
    }

    private void SetPopupTextImage(string upperString, string mainString, Sprite sprite)
    {
        popupImage.gameObject.SetActive(true);
        lowerTextShort.gameObject.SetActive(true);
        lowerTextLong.gameObject.SetActive(false);

        upperText.text = upperString;
        lowerTextShort.text = mainString;
        popupImage.sprite = sprite;
    }
}
