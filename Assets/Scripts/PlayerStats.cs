using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private MainManager mainManager;
    [SerializeField] private TextMeshPro attractionText;
    [SerializeField] private Material attractionTextPink;
    [SerializeField] private Material attractionTextGreen;
    [SerializeField] private Material attractionTextBlue;
    [SerializeField] private Animator attractionAnimator;
    public int playerAttraction;
    public int baseAttraction;
    public int maxBaseAttraction;
    public int monsterBonusAttraction;
    public int maxMonsterBonusAttraction; // <- Welp, wont use this one (2024 Update, maybe I will 0_0?..)
    public int bonusAttraction;
    public int maxBonusAttraction;
    public bool overAttractive;
    private bool itemBonusAttr;

    public void TextUpdate()
    {
        attractionText.text = playerAttraction.ToString();
    }

    public void BlinkAttraction()
    {
        if (!overAttractive && !(playerAttraction <= maxBaseAttraction / 2f))
        {
            //Debug.Log("ShowingAttraction");
            attractionAnimator.SetTrigger("BeforeBlink");
        }
    }

    public void ChangeAttraction(int bywhat, int mode) //mode - 0) hit (first remove bonus then monster then base); 1) monster (added to monster bonus if attraction is over maximum); 2) monster loose (remove monster bonus attraction); 3) bonus (added to bonus if attraction is over maximum); 4) blink (thats it); 5) bonus loose (for whenewer the bonus ends, removes bonus attraction, just like monster loose does)
    {
        switch (mode)
        {
            case 0:
                if (bywhat > bonusAttraction)
                {
                    bywhat -= bonusAttraction;
                    bonusAttraction = 0;
                    if (bywhat > monsterBonusAttraction)
                    {
                        bywhat -= monsterBonusAttraction;
                        monsterBonusAttraction = 0;
                        baseAttraction = Mathf.Clamp(baseAttraction - bywhat, 0, maxBaseAttraction);
                    }
                    else
                    {
                        //Debug.Log("Should Go here");
                        monsterBonusAttraction -= bywhat;
                    }
                }
                else
                {
                    bonusAttraction -= bywhat;
                }
                break;
            case 1:
                if (baseAttraction + bywhat > maxBaseAttraction)
                {
                    bywhat -= (maxBaseAttraction - baseAttraction);
                    baseAttraction = maxBaseAttraction;
                    //Debug.Log(bywhat);
                    monsterBonusAttraction = Mathf.Clamp(monsterBonusAttraction + bywhat, 0, maxMonsterBonusAttraction);
                    //Debug.Log((baseAttraction + monsterBonusAttraction).ToString());
                }
                else
                    baseAttraction += bywhat;
                break;
            case 2:
                if (monsterBonusAttraction - bywhat > 0)
                {
                    monsterBonusAttraction -= bywhat;
                }
                else
                {
                    monsterBonusAttraction = 0;
                }
                break;
            case 3:
                if (baseAttraction + bywhat > maxBaseAttraction)
                {
                    bywhat -= (maxBaseAttraction - baseAttraction);
                    baseAttraction = maxBaseAttraction;
                    bonusAttraction = bonusAttraction + bywhat;

                }
                else
                    baseAttraction += bywhat;
                break;
            case 4:
                Debug.Log("Blinkin"); //okay, now what?
                break;
            case 5:
                if (bonusAttraction - bywhat > 0)
                {
                    bonusAttraction -= bywhat;
                }
                else
                {
                    bonusAttraction = 0;
                }
                break;
            default:
                Debug.LogError("ERROR Mode ERROR selected ERROR the ERROR humanity ERROR is ERROR fucERRORked");
                break;
        }
        playerAttraction = baseAttraction + monsterBonusAttraction + bonusAttraction;
        attractionText.text = playerAttraction.ToString();
        if (playerAttraction > maxBaseAttraction)
            overAttractive = true;
        else
            overAttractive = false;
        if (bonusAttraction > 0)
            itemBonusAttr = true;
        else
            itemBonusAttr = false;
        if (playerAttraction <= maxBaseAttraction / 2f)
        {
            attractionAnimator.SetBool("LowAttraction", true);
            attractionAnimator.SetBool("OverAttraction", false);
            attractionAnimator.SetBool("BonusAttraction", false);
            attractionAnimator.SetBool("KeepShown", true);
            //attractionText.material = attractionTextPink;
            attractionText.fontMaterial = attractionTextPink;
            //attractionText.fontSharedMaterial = attractionTextPink;
        }
        else if (itemBonusAttr) //CHANGE THAT!!!!
        {
            attractionAnimator.SetBool("LowAttraction", false);
            attractionAnimator.SetBool("OverAttraction", false);
            attractionAnimator.SetBool("BonusAttraction", true);
            attractionAnimator.SetBool("KeepShown", true);
            attractionText.fontMaterial = attractionTextBlue;
        }
        else if (overAttractive)
        {
            attractionAnimator.SetBool("LowAttraction", false);
            attractionAnimator.SetBool("OverAttraction", true);
            attractionAnimator.SetBool("BonusAttraction", false);
            attractionAnimator.SetBool("KeepShown", true);
            attractionText.fontMaterial = attractionTextGreen;
        }
        else
        {
            attractionAnimator.SetBool("LowAttraction", false);
            attractionAnimator.SetBool("OverAttraction", false);
            attractionAnimator.SetBool("BonusAttraction", false);
            attractionAnimator.SetBool("KeepShown", false);
            attractionText.fontMaterial = attractionTextPink;
        }
        //attractionText.UpdateFontAsset();
        attractionAnimator.SetTrigger("Blink");
    }
}
