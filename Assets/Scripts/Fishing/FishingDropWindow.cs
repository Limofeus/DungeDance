using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishingDropWindow : MonoBehaviour
{
    [SerializeField] private SpriteRenderer itemCaughtSR;
    [SerializeField] private TextMeshPro fishNameText;
    [SerializeField] private TextMeshPro fishSizeText;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void StartFishDropAnim(bool isItem, int fishId, float fishSize, bool hasInvSpace)
    {
        if (isItem)
        {
            itemCaughtSR.sprite = ItemSpriteDictionary.itemSprites[fishId];
            fishNameText.text = LocalisationSystem.GetLocalizedValue($"item_name_id{fishId}");
            if(hasInvSpace)
                fishSizeText.text = LocalisationSystem.GetLocalizedValue("fishing_item_caught_to_inventory");
            else
                fishSizeText.text = LocalisationSystem.GetLocalizedValue("fishing_item_caught_to_storage");
        }
        else
        {
            itemCaughtSR.sprite = FishDict.fishSprites[fishId];
            fishNameText.text = LocalisationSystem.GetLocalizedValue($"fish_name_id{fishId}");
            fishSizeText.text = LocalisationSystem.GetLocalizedValue("fishing_caught_size") + fishSize.ToString("G3");
        }
        animator.SetTrigger("FishDrop");
    }
}
