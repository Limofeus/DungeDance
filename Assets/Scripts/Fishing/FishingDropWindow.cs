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
    public void StartFishDropAnim(bool isItem, int fishId, float fishSize)
    {
        if (isItem)
        {
            itemCaughtSR.sprite = ItemSpriteDictionary.itemSprites[fishId];
            fishNameText.text = LocalisationSystem.GetLocalizedValue($"item_name_id{fishId}");
            fishSizeText.text = LocalisationSystem.GetLocalizedValue("fishing_item_caught");
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
